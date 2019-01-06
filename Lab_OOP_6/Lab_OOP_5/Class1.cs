using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_OOP_5
{
    public partial class Workbook: Author
    {

        public override string ToString()
        {                                       //переопределение метода(во всех классах)
            return "" + base.ToString() + " The name of workbook is " + NameOfW + ".";
        }
        //т.к. наследуется от абстрактного класса, то переопределяем методы
        override public void ToPrint()
        {
            Console.WriteLine(this.ToString());
        }
        public override void Same()
        {                                       //метод из абстрактного класса 
            Console.WriteLine("I am the same method(abstract)!");
        }
        public void Info()
        {
            Console.WriteLine("The author of " + NameOfW + " is " + FirstName + " " + LastName + ".");
        }
    }
}
