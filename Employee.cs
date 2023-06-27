using System;
namespace T7
{
    public class Employee
    {
        // Variable
        private string no;
        private string name;
        private string email;
        private bool deleted;
        private string password;
        private bool isManager;

        // Contructor
        public Employee()
        {
            this.no = " ";
            this.name = " ";
            this.email = " ";
            this.deleted = true;
            this.password = " ";
            this.isManager = false;
        }
        public Employee(string no, string name, string email, bool deleted, string password, bool isManager)
        {
            this.no = no;
            this.name = name;
            this.email = email;
            this.deleted = deleted;
            this.password = password;
            this.isManager = isManager;
        }

        //Getter
        public string GetNo()
        {
            return this.no;
        }
        public string GetName()
        {
            return this.name;
        }
        public string GetEmail()
        {
            return this.name;
        }
        public bool GetDeleted()
        {
            return this.deleted;
        }
        public string GetPassword()
        {
            return password;
        }
        public bool GetIsManager()
        {
            return isManager;
        }

        //Setter
        public void SetName(string name)
        {
            this.name = name;
        }
        public void SetNo(string no)
        {
            this.no = no;
        }
        public void SetEmail(string email)
        {
            this.email = email;
        }
        public void SetDeleted(bool deleted)
        {
            this.deleted = deleted;
        }
        public void SetPassword(string password)
        {
            this.password = password;
        }
        public void SetIsManager(bool isManager)
        {
            this.isManager = isManager;
        }

        // ToString for print
        public override string? ToString()
        {
            return no + ", " + name + ", " + email;// + ", " + deleted ;
        }

        // String for print data for file
        public string? ToStringForFile()
        {
            return no + "," + name + "," + email + "," + deleted + "," + password + "," + isManager;
        }
    }
}

