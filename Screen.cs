using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T7
{
    public class Screen : EmployeeManager
    {
        private static readonly string logPath = "T7log.txt";
        private bool loged = false;

        public Screen() : base() 
        {
            
            do 
            {
                string login = Login();
                switch (login)
                {
                case "manager":
                    this.ManagerScreen();
                    break;
                case "user":
                    this.UserScreen();
                    break;
                }
            } while (!loged);
        }
        
        private string empNoLogin = "";
        //Getter
        public string Login()
        {
            string empPassword;
            string role = " ";
            
            Console.Clear();
            Console.WriteLine("***EMPLOYEE MANAGER***");
            Console.WriteLine("***  LOGIN SCREEN  ***");
            do
            {
                Console.Write("EmpNo:");
                this.empNoLogin = (Console.ReadLine() + "");
                Console.Write("Password:");
                empPassword = (Console.ReadLine() + "");
                if (!this.IsValid(this.empNoLogin))
                {
                    Console.WriteLine("Invalid username");
                    Console.ReadLine();
                    continue;
                }
                if (!this.IsPassword(this.empNoLogin, empPassword))
                {
                    Console.WriteLine("Wrong password");
                    Console.ReadLine();
                    continue;
                }
                if (this.IsDeleted(this.empNoLogin))
                {
                    Console.WriteLine("Employee deleted!");
                    Console.ReadLine();
                    continue;
                }
                this.loged = true;                
                if (IsManager(this.empNoLogin)) role = "manager";
                if (!IsManager(this.empNoLogin)) role = "user";
                WriteLogFile(this.empNoLogin + ": log in as " + role);
                Console.WriteLine("Login succesful");
                Console.ReadLine();
                Console.Clear();                
            }
            while (!loged);
            return role;
        }

        // Module Manager Screen
        public void ManagerScreen()
        {
            int selected;
            do
            {   
                Console.Clear ();
                Console.WriteLine("***EMPLOYEE MANAGER***");
                Console.WriteLine("EmpNo: {0}", this.empNoLogin);
                Console.WriteLine("Role = Manager");
                Console.WriteLine("1. Search Employee by Name or EmpNo");
                Console.WriteLine("2. Add New Employee");
                Console.WriteLine("3. Update Employee");
                Console.WriteLine("4. Delete Employee");
                Console.WriteLine("5. Show all Employee");
                Console.WriteLine("6. Import a list of Employee");
                Console.WriteLine("7. Export a list of Employee");
                Console.WriteLine("8. Logout");
                Console.WriteLine("9. Exit");
                Console.Write("   Select (1-9): ");
                try { selected = Convert.ToInt16(Console.ReadLine()); }
                catch 
                { 
                    Console.WriteLine("Wrong select! Auto Logout");
                    selected = 8;
                }
                switch (selected)
                {
                    case 1:
                        this.Find();
                        break;
                    case 2:
                        WriteLogFile(empNoLogin +":" + this.AddNew());
                        break;
                    case 3:
                        WriteLogFile(empNoLogin + ":" + this.Update());
                        break;
                    case 4:
                        WriteLogFile(empNoLogin + ":" + this.Delete());
                        break;
                    case 5:
                        Console.Clear();
                        this.PrintList(employees);
                        Console.ReadLine();
                        break;
                    case 6:
                        WriteLogFile(empNoLogin + ":" + this.Import());
                        break;
                    case 7:
                        WriteLogFile(empNoLogin + ":" + this.Export());
                        break;
                    case 8:
                        loged = false;
                        Console.WriteLine("Logging out");
                        WriteLogFile(this.empNoLogin + ": log out");
                        break;
                    case 9:
                        WriteLogFile(this.empNoLogin + ": log out");
                        Console.WriteLine("-------- END ---------");
                        break;
                    default:
                        Console.WriteLine("Invalid");
                        break;
                }
            } while (selected != 9 && loged);
        }

        // Module User Screen
        public void UserScreen()
        {
            int selected;
            do
            {
                Console.Clear();
                Console.WriteLine("***EMPLOYEE MANAGER***");
                Console.WriteLine("EmpNo: {0}", this.empNoLogin);
                Console.WriteLine("Role = User");
                Console.WriteLine("1. Search Employee by Name or EmpNo");
                Console.WriteLine("2. Show all Employee");
                Console.WriteLine("3. Log out");
                Console.WriteLine("4. Exit");
                Console.Write("   Select (1-4): ");
                try { selected = Convert.ToInt16(Console.ReadLine()); }
                catch
                {
                    Console.WriteLine("Wrong select! Auto Logout");
                    selected = 3;
                }
                switch (selected)
                {
                    case 1:
                        this.Find();
                        break;
                    case 2:
                        Console.Clear();
                        this.PrintList(employees);
                        Console.ReadLine();
                        break;
                    case 3:
                        loged = false;
                        Console.WriteLine("Logging out");
                        WriteLogFile(this.empNoLogin + " log out");
                        break;
                    case 4:
                        Console.WriteLine("-------- END ---------");
                        WriteLogFile(this.empNoLogin + " log out");
                        break;
                    default:
                        Console.WriteLine("Invalid");
                        break;
                }
            } while (selected != 4 && loged);
        }
        public static void WriteLogFile(string content)
        {
            File.AppendAllTextAsync(logPath, DateTime.Now + " : " + content + "\n");            
        }

    }
}
