using System;
namespace T7
{
    public abstract class BaseManager
    {
        public BaseManager() { }
        abstract public string AddNew();
        abstract public string Update();
        abstract public string Delete();
        abstract public string Import();
        abstract public string Export();
        abstract public void Find();
        abstract public void PrintList(Employee[] arr);
        abstract public void PrintToFile(string filePath);
    }
}

