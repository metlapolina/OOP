using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Журнал, Книга, Печатное издание, Учебник, Журнал, Персона, Автор, Издательство. 

namespace Lab_OOP_5
{
    interface IPublisher
    {                               //интерфейс - функционал без реализации, нет объекта класса
        void Info();
    }
    interface IPublish
    {                               //интерфейс
        void Same();                //одноименный метод с абстрактным классом
    }

    public abstract class Author
    {                               //абстрактный класс - нет объекта класса
        protected string firstName;
        protected string lastName;
        public string FirstName
        {
            get => firstName;
            set => firstName = value;
        }
        public string LastName
        {
            get => lastName;
            set => lastName = value;
        }
        public override string ToString()   //переопределение метода(во всех классах)
        {
            return "Type " + base.ToString() + ". The author - " + FirstName + " " + LastName+".";
        }
        virtual public void ToPrint() { }   //виртуальный метод дб реализован в поизводных классах
        public abstract void Same();        //одноименный метод с интерфейсом
    }

    public class Person : Author, IPublish      //наследуется от абстрактного класса и интерфейса
    {
        protected int countOfBooks;
        protected int years;

        public Person(string name, string sname, int count, int years)
        {
            this.firstName = name;
            this.lastName = sname;
            this.countOfBooks = count;
            this.years = years;
        }

        public int CountOfBooks
        {
            get => countOfBooks;
            set => countOfBooks = value;
        }
        public int Years
        {
            get => years;
            set => years = value;
        }
        public override string ToString()
        {                               //переопределение метода(во всех классах)
            return "" + base.ToString() + " The author has written " + CountOfBooks + " books. He is " + Years + " years old.";
        }

        override public void ToPrint()          //реализация абстрактного метода
        {
            Console.WriteLine(this.ToString());
        }
        public override void Same()
        {                               //реализация для абстрактного метода
            Console.WriteLine("I am the same method(abstract)!");
        }
        void IPublish.Same()
        {                               //реализация для метода интерфейса
            Console.WriteLine("I am the same method TOO(interface)!");
        }
        public void Info()
        {
            Console.WriteLine(FirstName + " " + LastName + " " + Years + "years old, " + CountOfBooks + " books.");
        }
    }

    sealed class PrintEdition : Author      //бесплодный класс - нельзя наследовать
    {
        protected string nameOfP;

        public PrintEdition(string name, string sname, string nameOfP)
        {
            this.firstName = name;
            this.lastName = sname;
            this.nameOfP = nameOfP;
        }

        public string NameOfP
        {
            get => nameOfP;
            set => nameOfP = value;
        }
        //переопределение всех методов, унаследованных от Object
        //переопределение ToString(во всех классах)
        public override string ToString()
        {
            return "" + base.ToString() + " The name of print edition is " + NameOfP + ".";
        }
        //переопределение Equals
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (obj.GetType() != this.GetType())
                return false;
            PrintEdition odin = (PrintEdition)obj;
            return (this.nameOfP == odin.nameOfP);
        }
        //переопределение GetHashCode
        public override int GetHashCode()
        {
            int hash = 47, d = 32;
            string a = Convert.ToString(nameOfP);
            hash = string.IsNullOrEmpty(a) ? 0 : nameOfP.GetHashCode();
            hash = (hash * 47) + d.GetHashCode();
            return hash;
        }
        //т.к. наследуется от абстрактного класса, то переопределяем методы
        override public void ToPrint()
        {
            Console.WriteLine(this.ToString());
        }
        public override void Same()
        {                                   //метод из абстрактного класса 
            Console.WriteLine("I am the same method(abstract)!");
        }
        public void Info()
        {         
            Console.WriteLine("The author of " + NameOfP + " is " + FirstName + " " + LastName+".");
        }
    }

    class Workbook : Author
    {
        protected string nameOfW;

        public Workbook(string name, string sname, string nameOfW)
        {
            this.firstName = name;
            this.lastName = sname;
            this.nameOfW = nameOfW;
        }

        public string NameOfW
        {
            get => nameOfW;
            set => nameOfW = value;
        }
        public override string ToString()
        {                               //переопределение метода(во всех классах)
            return "" + base.ToString() + " The name of workbook is " + NameOfW + ".";
        }
        //т.к. наследуется от абстрактного класса, то переопределяем методы
        override public void ToPrint()
        {                           
            Console.WriteLine(this.ToString());
        }
        public override void Same()
        {                               //метод из абстрактного класса 
            Console.WriteLine("I am the same method(abstract)!");
        }
        public void Info()
        {
            Console.WriteLine("The author of "+ NameOfW +" is " + FirstName + " " + LastName+".");
        }
    }
    
    //Создайте дополнительный класс Printer c методом
    //Формальным параметром метода должна быть ссылка на абстрактный класс. 
    //В методе определите тип объекта и вызовите ToString()
    public static class Printer
    {
        public static void IAmPrinting(Author author)
        {
            Console.WriteLine(author.GetType());
            Console.WriteLine(author.ToString());
        }
    }


    //Написать демонстрационную программу, в которой создаются объекты различных классов. 
    //Поработать с объектами через ссылки на абстрактные классы и интерфейсы. 
    //В этом случае для идентификации типов объектов использовать операторы is или as. 
    //В демонстрационной программе создайте массив, содержащий ссылки на разнотипные объекты ваших классов по иерархии, 
    //а также объект класса Printer и последовательно вызовите его метод iAmPrinting  со всеми ссылками в качестве аргументов. 
    class Program
    {
        static void Main(string[] args)
        {
            Person person = new Person("Jack", "London", 40, 40);
            Person person1 = new Person("Stephen", "King", 60, 71);
            Person person2 = new Person("Herbert", "Wells", 50, 79);
            PrintEdition ed = new PrintEdition("Sonya","Sun","Morning");
            ed.ToPrint();
            ed.Info();
            Workbook wbook = new Workbook("Arhimed","","Math");
            wbook.ToPrint();
            wbook.Info();

            IPublish pub = new Person("A","A",1,100);
            Author auth = person;
            Author auth1 = person2 as Author;
            Console.WriteLine(pub is Person);
            Console.WriteLine(auth is PrintEdition);
            Console.WriteLine();

            //вызов одноименных методов
            auth.Same();     
            ((IPublish)auth).Same();
            Console.WriteLine();

            //создание массива
            Author[] mas = { person1, ed, wbook, pub as Author, auth1 };
            for(int i = 0; i < mas.Length; i++)
            {
                Printer.IAmPrinting(mas[i]);
            }
        }
    }
}
