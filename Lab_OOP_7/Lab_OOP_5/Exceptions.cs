using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Lab_OOP_5
{
    public class EmptyException : Exception
    {
        public EmptyException(string mess) : base(mess) { }
        public void GetInfo()
        {
            Console.WriteLine("EmptyException: " + this.Message);
            Console.WriteLine(this.StackTrace);
            Console.WriteLine(this.TargetSite);
        }
    }
    public class NoNameException : Exception
    {
        public NoNameException(string mess) : base(mess) { }
        public void GetInfo()
        {
            Console.WriteLine("NoNameException: " + this.Message);
            Console.WriteLine(this.StackTrace);
            Console.WriteLine(this.TargetSite);
        }
    }
    public class SecretException : Exception
    {
        public SecretException(string mess) : base(mess) { }
        public void GetInfo()
        {
            Console.WriteLine("SecretException: " + this.Message);
            Console.WriteLine(this.StackTrace);
            Console.WriteLine(this.TargetSite);
        }
    }
}
