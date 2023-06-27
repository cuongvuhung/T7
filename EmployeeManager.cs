using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
namespace T7
{
    public class EmployeeManager : BaseManager
    {
        //Varriable
        public Employee[] employees;
        private readonly string filePath = @"T7.dat";

        // Contructor
        public EmployeeManager()
        {
            // Create template file if not exist
            if (!File.Exists(filePath)) { File.WriteAllText(filePath, "EmpNo,EmpName,EmpEmail,true,EmpPassword,false"); }

            // Read file thisPath
            string[] content = File.ReadAllLines(this.filePath);

            // Create template file for first item 
            this.employees = new Employee[]
            {
                new Employee("EmpNo", "EmpName", "EmpEmail",true,"EmpPassword",false)
            };

            // Set data from file to cache data. 
            if (content.Length > 1)
            {
                for (int i = 1; i < content.Length; i++)
                {
                    string[] cell = content[i].Split(',');
                    employees = employees.Concat(new Employee[] { new Employee(cell[0], cell[1], cell[2], Convert.ToBoolean(cell[3]), cell[4], Convert.ToBoolean(cell[5])) }).ToArray();
                }
            }
            else
            {
                // Set admin user for first time or no base data
                Console.WriteLine("No base data!");
                Console.WriteLine("Creating base data!");
                employees = employees.Concat(new Employee[] { new Employee("admin", "admin", "admin", false, "admin", true) }).ToArray();
                PrintToFile(this.filePath);
            }
        }

        // Void        
        // Module Add new a employee
        public override string AddNew()
        {
            Console.Clear();
            Console.Write("Enter EmpNo:");
            string empNo = (Console.ReadLine() + "");
            Console.Write("Enter EmpName:");
            string empName = (Console.ReadLine() + "");
            Console.Write("Enter EmpEmail:");
            string empEmail = (Console.ReadLine() + "");
            Console.Write("Enter EmpPassword:");
            string empPassword = (Console.ReadLine() + "");
            Console.Write("Enter EmpIsManager (True/Any else):");
            bool empIsManager;
            if (Console.ReadLine() == "True")
            {
                empIsManager = true;
            }
            else
            {
                empIsManager = false;
            }

            // Check not duplicate Emp No
            if (!this.IsValid(empNo))
            {
                Console.WriteLine("Add new Employee: {0},{1},{2},{3},{4}", empNo,empName,empEmail,empPassword,empIsManager);
                Console.WriteLine("Are you sure?");
                Console.Write("Yes(Y) or No(any other):");
                if ((Console.ReadLine() + "").ToUpper() == "Y")
                {
                    employees = employees.Concat(new Employee[] { new Employee(empNo, empName, empEmail, false, empPassword, empIsManager) }).ToArray();
                    PrintToFile(this.filePath);
                    Console.Write("Employee added! ");
                    Console.ReadLine();
                    return (" add new " + empNo + "," + empName + "," + empEmail + ": Successful");
                }
                else
                {
                    Console.WriteLine("Nothing happen!");                    
                    Console.ReadLine();
                    return (" add new " + empNo + "," + empName + "," + empEmail + ": Stop by user");
                }                
            }
            else
            {
                Console.Write("Valid EmpNo! ");
                Console.ReadLine();
                return (" add new " + empNo + "," + empName + "," + empEmail + ": Unsuccessful");
            }
        }

        // Module Update a employee
        public override string Update()
        {
            Console.Clear();
            Console.Write("Enter EmpNo or EmpName for update: ");
            string searchKey = (Console.ReadLine() + "");
            string updateString = "";
            if (this.IsValid(searchKey))
            {
                foreach (Employee emp in employees)
                {
                    if (((emp.GetNo().Equals(searchKey) || emp.GetName().Equals(searchKey)) && emp.GetDeleted().Equals(false)))
                    {
                        Console.WriteLine("Found a Employee have EmpNo or Emp Name is:" + searchKey);
                        Console.WriteLine(emp.GetNo() + "," + emp.GetName() + "," + emp.GetEmail());
                        Console.WriteLine("Ready for update!");
                        Console.Write("Enter EmpNo: ");
                        emp.SetNo((Console.ReadLine() + ""));
                        Console.Write("Enter EmpName: ");
                        emp.SetName((Console.ReadLine() + ""));
                        Console.Write("Enter EmpEmail: ");
                        emp.SetEmail((Console.ReadLine() + ""));
                        Console.Write("Enter EmpPassword: ");
                        emp.SetPassword((Console.ReadLine() + ""));
                        updateString += (" update " + searchKey + ":" + emp.GetNo() + "," + emp.GetName() + "," + emp.GetEmail() + "\n");
                    }
                }
            } else 
            {
                Console.WriteLine("Not found:{0}",searchKey);
                Console.ReadLine();
                updateString = (" update " + searchKey + ": Invalid"); 
            }
            PrintToFile(this.filePath);            
            return updateString;
        }

        // Module Delete a employee
        public override string Delete()
        {
            Console.Clear();
            Console.Write("Enter EmpNo or EmpName for delete: ");
            string searchKey = (Console.ReadLine() + "");
            foreach (Employee emp in employees)
            {
                if ((emp.GetNo().Equals(searchKey) || emp.GetName().Equals(searchKey)) && emp.GetDeleted().Equals(false))
                {
                    Console.WriteLine("Found a Employee have EmpNo or Emp Name is:" + searchKey);
                    Console.WriteLine("Delete " + emp.GetNo() + "," + emp.GetName() + "," + emp.GetEmail());
                    Console.WriteLine("Are you sure");
                    Console.Write("Yes(Y) or No(any other):");
                    if ((Console.ReadLine() + "").ToUpper() == "Y")
                    {
                        emp.SetDeleted(true);
                        PrintToFile(this.filePath);
                        Console.WriteLine("Deleted");
                        Console.ReadLine();
                        return (" delete " + searchKey + " successful");
                    }
                    else
                    {
                        Console.WriteLine("Nothing happen!");
                        PrintToFile(this.filePath);
                        Console.ReadLine();
                        return (" delete " + searchKey + " unsuccessful");
                    }
                }
            }
            Console.WriteLine("Not found {0} in database", searchKey);
            Console.ReadLine();
            return (" delete " + searchKey + " unsuccessful");
        }

        public override string Import()
        {
            Console.Clear();
            string filename;
            string[] content;
            string msg;
            Console.Write("Enter file name: ");
            filename = @"" + Console.ReadLine();
            if (File.Exists(filename))
            {
                content = File.ReadAllLines(filename);
                Employee[] emp = new Employee[]
                {
                    new Employee("EmpNo", "EmpName", "EmpEmail",true,"EmpPassword",false)
                };
                for (int i = 1; i < content.Length; i++)
                {
                    try
                    {
                        string[] cell = content[i].Split(',');
                        if (!this.IsValid(cell[0]))
                            emp = emp.Concat(new Employee[] { new Employee(cell[0], cell[1], cell[2], Convert.ToBoolean(cell[3]), cell[4], Convert.ToBoolean(cell[3])) }).ToArray();
                        else Console.WriteLine("Valid a Employee with EmpNo:" + cell[0]);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Bad File input! CODE:", e);
                        Console.ReadLine();
                        return (" try import: " + filename + ": Bad File input");
                    }
                }
                Console.WriteLine("Find more {0} Employee in imported file", emp.Length-1);
                Console.ReadLine();
                PrintList(emp);
                if (emp.Length > 1)
                {
                    for (int i = 0; i < emp.Length-1; i++)
                    {
                        emp[i] = emp[i+1];
                    }
                    Array.Resize(ref emp, emp.Length - 1);
                    Console.WriteLine("Ready for import");
                    Console.Write("Yes(Y) or No(any other):");
                    if ((Console.ReadLine() + "").ToUpper() == "Y")
                    {
                        employees = employees.Concat(emp).ToArray();
                        PrintToFile(this.filePath);
                        PrintList(employees);
                        msg = (" import: " + filename + " Successful import user: ");
                        foreach (Employee msgline in emp)
                        {
                            msg += msgline.GetNo() + ",";
                        }
                        Console.WriteLine("Import successful!");
                        Console.ReadLine();
                        return msg;
                    }
                    else
                    {
                        Console.WriteLine("Nothing happen!");
                        PrintToFile(this.filePath);
                        PrintList(employees);
                        return (" try import: " + filename + ": Stop by user");
                    }
                }
                else 
                {
                    Console.WriteLine("Nothing to import!");
                    Console.ReadLine();
                    return (" try import: " + filename + ": Nothing to import");
                };
                } 
                else 
                { 
                Console.WriteLine("Invalid file!");
                return (" try import: " + filename + ": invalid filename");                    
                }
                
        }
        public override string Export()
        {
            Console.Clear();
            string filename = @"";
            string[] content = new string[this.GetDataLength()];
            while (filename == "")
            {
                Console.Write("Enter file name: ");
                filename = @"" + Console.ReadLine();
            }
            for (int i = 0; i < this.GetDataLength(); i++)
            {
                content[i] = employees[i].ToStringForFile() + "";
            }
            if (!File.Exists(filename))
            {
                File.AppendAllLines(filename + ".csv", content);
                Console.WriteLine("Export successful!");
                Console.ReadLine();
                return (" export data to: " + filename + ":Successful");
            }
            else
            {
                Console.WriteLine("File exist!");
                Console.ReadLine();
                return (" export data to: " + filename + ": File exist");
            }
        }
        // Module Find employee
        public override void Find()
        {
            Console.Clear();
            Console.Write("Enter EmpNo or Name: ");
            string searchKey = (Console.ReadLine() + "");
            // search            
            Employee[] result = new Employee[this.GetDataLength()];
            int count = 0;
            foreach (Employee emp in employees)
            {
                if ((emp.GetNo().Equals(searchKey) || emp.GetName().Equals(searchKey)) && emp.GetDeleted().Equals(false))
                {
                    result[count++] = emp;
                }
            }
            // print
            if (count > 0)
            {
                PrintList(result);
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Not Found!");
                Console.ReadLine();
            }
        }

        // Module Show List of employee
        public override void PrintList(Employee[] arr)
        {
            foreach (Employee item in arr)
            {
                if (item != null && item.GetDeleted().Equals(false))
                {
                    Console.WriteLine(item);
                }
            }
            Console.WriteLine("---------------------------------");
        }

        // Module Write to file filePath all data 
        public override void PrintToFile(string filePath)
        {
            string[] content = new string[this.GetDataLength()];
            int i = 0;
            foreach (Employee item in employees)
            {
                content[i++] = (item.ToStringForFile() + "");
            }
            File.WriteAllLines(filePath, content);
            //Console.Write("Data is saved!");
            //Console.ReadLine();            
        }
        
        //Checker
        // Check how much item of data 
        private int GetDataLength()
        {
            int employeeLenght = 0;
            foreach (Employee emp in employees) employeeLenght++;
            return employeeLenght;
        }

        // Check empNo valid in data
        public bool IsValid(string empNo)
        {
            foreach (Employee emp in employees)
            {
                if (emp.GetNo().Equals(empNo))
                { return true; }
            }
            return false;
        }

        // Check correct passwword
        public bool IsPassword(string empNo, string empPassword)
        {
            foreach (Employee emp in employees)
            {
                if (emp.GetNo().Equals(empNo) && emp.GetPassword().Equals(empPassword))
                { return true; }
            }
            return false;
        }

        // Check user is manager
        public bool IsManager(string empNo)
        {
            foreach (Employee emp in employees)
            {
                if (emp.GetNo().Equals(empNo) && emp.GetIsManager().Equals(true))
                { return true; }
            }
            return false;
        }

        // Check empNo is deleted
        public bool IsDeleted(string empNo)
        {
            bool value = false;
            foreach (Employee emp in employees)
            {
                if (emp.GetNo().Equals(empNo) && emp.GetDeleted().Equals(true))
                { value = true; }
            }
            return value;
        }
    }
}

