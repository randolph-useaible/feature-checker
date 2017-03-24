using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;

namespace RegistryChecker
{
    class Program
    {
        static void Main(string[] args)
        {
            PythonChecker python = new PythonChecker();
            python.Check();
            Console.WriteLine(python.ToString());
            Console.ReadLine();
        }
    }
}
