using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Журнал, Книга, Печатное издание, Учебник, Журнал, Персона, Автор, Издательство. 

namespace Lab_OOP_5
{
    struct Information  //1)структура - совокупность логически связанных данных различного типа, объе под одним ид
    {
        public string book;
        public string person;
        public string name;
    }
    enum Book           //1)перечисление - набор логически связанных констант(по умолчанию int)
    {
        PrintEdition, Workbook, Magazine = 5, Book
    }
    interface IPublisher
    {                                       //интерфейс - функционал без реализации, нет объекта класса
        void Info();
    }
    interface IPublish
    {                                       //интерфейс
        void Same();                        //одноименный метод с абстрактным классом
    }

    public abstract class Author
    {                                       //абстрактный класс - нет объекта класса
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
        {                                       //переопределение метода(во всех классах)
            return "" + base.ToString() + " The author has written " + CountOfBooks + " books. He is " + Years + " years old.";
        }

        override public void ToPrint()          //реализация абстрактного метода
        {
            Console.WriteLine(this.ToString());
        }
        public override void Same()
        {                                       //реализация для абстрактного метода
            Console.WriteLine("I am the same method(abstract)!");
        }
        void IPublish.Same()
        {                                       //реализация для метода интерфейса
            Console.WriteLine("I am the same method TOO(interface)!");
        }
        public void Info()
        {
            Console.WriteLine(FirstName + " " + LastName + " " + Years + "years old, " + CountOfBooks + " books.");
        }
    }

    sealed public class PrintEdition : Author              //бесплодный класс - нельзя наследовать
    {
        protected string nameOfP;
        protected int yearOfP;
        protected int costOfP;

        public PrintEdition(string name, string sname, string nameOfP, int yearOfP, int costOfP)
        {
            this.firstName = name;
            this.lastName = sname;
            this.nameOfP = nameOfP;
            this.yearOfP = yearOfP;
            this.costOfP = costOfP;
        }

        public string NameOfP
        {
            get => nameOfP;
            set => nameOfP = value;
        }
        public int YearOfP
        {
            get => yearOfP;
            set => yearOfP = value;
        }
        public int CostOfP
        {
            get => costOfP;
            set => costOfP = value;
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
        {                                       //метод из абстрактного класса 
            Console.WriteLine("I am the same method(abstract)!");
        }
        public void Info()
        {         
            Console.WriteLine("The author of " + NameOfP + " is " + FirstName + " " + LastName+".");
        }
    }

    public partial class Workbook : Author //2)создать partial класс и разместить в разных файлах
    {
        protected string nameOfW;
        protected int yearOfW;
        protected int costOfW;

        public Workbook(string name, string sname, string nameOfW, int yearOfW, int costOfW)
        {
            this.firstName = name;
            this.lastName = sname;
            this.nameOfW = nameOfW;
            this.yearOfW = yearOfW;
            this.costOfW = costOfW;
        }

        public string NameOfW
        {
            get => nameOfW;
            set => nameOfW = value;
        }
        public int YearOfW
        {
            get => yearOfW;
            set => yearOfW = value;
        }
        public int CostOfW
        {
            get => costOfW;
            set => costOfW = value;
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

    //3) Определить класс-Контейнер для хранения разных типов объектов (в пределах иерархии)
    //в виде списка или массива (использовать абстрактный тип данных). 
    //Класс-контейнер должен содержать методы get и set для управления списком/массивом, 
    //методы для добавления и удаления объектов в список/массив, метод для вывода списка на консоль. 
    //Создать Библиотеку с книгами, журналами и учебниками. Вывести наименование всех книг в библиотеке, 
    //вышедших не ранее указанного года; найти  суммарное количество учебников в библиотеке, подсчитать стоимость изданий, 
    //находящихся в библиотеке.
    public class Library
    {
        public List<PrintEdition> printEd = new List<PrintEdition>();
        public List<Workbook> workbooks = new List<Workbook>();
        public PrintEdition this[int index]
        {
            get
            {
                if (index > printEd.Count)
                {
                    Console.WriteLine("Превышен максимальный индекс списка печатных изданий");
                    return null;
                }
                return printEd[index];
            }
            set
            {
                if (index > printEd.Count)
                    Console.WriteLine("Элемента с таким индексом не существует");
                else
                    printEd[index] = value;
            }
        }
        public void AddPrintEd(PrintEdition a) { printEd.Add(a); }
        public void AddWorkbook(Workbook a) { workbooks.Add(a); }
        public void DelPrintEd(PrintEdition d) { printEd.Remove(d); }
        public void DelWorkbook(Workbook d) { workbooks.Remove(d); }
        public void ToConsoleList()
        {
            Console.WriteLine("List of books:");
            for (int i = 0; i < printEd.Count; i++)
            {
                Console.WriteLine("PrintEdition[{0}] = {1}", i, printEd[i].NameOfP);
            }
            for (int j = 0; j < workbooks.Count; j++)
            {
                Console.WriteLine("Workbooks[{0}] = {1}", j, workbooks[j].NameOfW);
            }
        }
        public void ShowList(int year)
        {
            Console.WriteLine("List of books the year of publication of which is more than {0}", year);
            for (int i = 0; i < workbooks.Count; i++)
            {
                if (workbooks[i].YearOfW >= year)
                {
                    Console.WriteLine("Workbook[{0}] = {1} {2}", i, workbooks[i].NameOfW, workbooks[i].YearOfW);
                }
            }
            for (int i = 0; i < printEd.Count; i++)
            {
                if (printEd[i].YearOfP >= year)
                {
                    Console.WriteLine("PrintEdition[{0}] = {1} {2}", i, printEd[i].NameOfP, printEd[i].YearOfP);
                }
            }
        }
        public int CountOfBooks()
        {
            int count = 0;
            count = workbooks.Count + printEd.Count;
            return count;
        }
        public int CostOfBooks()
        {
            int cost = 0, c1 = 0, c2 = 0;
            for (int i = 0; i < workbooks.Count; i++)
            {
                c1 += workbooks[i].CostOfW;
            }
            for (int j = 0; j < printEd.Count; j++)
            {
                c2 += printEd[j].CostOfP;
            }
            cost = c1 + c2;
            return cost;
        }
    }
    //4)Определить  управляющий класс-Контроллер, который управляет объектом-Контейнером и реализовать в нем запросы по варианту. 
    //При необходимости используйте стандартные интерфейсы (IComparable, ICloneable,….)
    public class LibraryController
    {
        public void Show(Library lib, int y) { lib.ShowList(y); }
        public int Count(Library lib) { int a = lib.CountOfBooks(); return a; }
        public int Cost(Library lib) { int b = lib.CostOfBooks(); return b; }
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
            //Person person = new Person("Jack", "London", 40, 40);
            //Person person1 = new Person("Stephen", "King", 60, 71);
            //Person person2 = new Person("Herbert", "Wells", 50, 79);
            Information inf;
            inf.name = "Harry Potter";
            Book mag = Book.Magazine;

            Library library = new Library();
            LibraryController libraryControl = new LibraryController();
            PrintEdition ed = new PrintEdition("Sonya","Sun","Morning", 2001, 20);
            PrintEdition ed1 = new PrintEdition("Alice", "Willson", "Sunday", 2005, 15);
            PrintEdition ed2 = new PrintEdition("Petr", "Komarov", "Good things", 2015, 40);
            PrintEdition ed3 = new PrintEdition("George", "Clane", "Hello", 2018, 48);
            PrintEdition ed4 = new PrintEdition("Eva", "Sable", "Every Day", 2003, 12);
            PrintEdition ed5 = new PrintEdition("Sonya", "Sun", "Morning", 2001, 20);
            Workbook wb = new Workbook("E.I.", "Lovenetskaya", "Math", 2014, 60);
            Workbook wb1 = new Workbook("P.M.", "Burak", "Philosophy", 2010, 33);
            Workbook wb2 = new Workbook("T.G.", "Kolennikova", "Psychology", 2009, 9);
            library.AddPrintEd(ed);
            library.AddPrintEd(ed1);
            library.AddPrintEd(ed2);
            library.AddPrintEd(ed3);
            library.AddPrintEd(ed4);
            library.AddPrintEd(ed5);
            library.DelPrintEd(ed5);
            library.AddWorkbook(wb);
            library.AddWorkbook(wb1);
            library.AddWorkbook(wb2);
            library.ToConsoleList();
            libraryControl.Show(library, 2014);
            Console.WriteLine("Count of books in the library: "+libraryControl.Count(library));
            Console.WriteLine("The cost of books in the library: "+libraryControl.Cost(library));
            
            //ed.ToPrint();
            //ed.Info();
            //wbook.ToPrint();
            //wbook.Info();

            //IPublish pub = new Person("A","A",1,100);
            //Author auth = person;
            //Author auth1 = person2 as Author;
            //Console.WriteLine(pub is Person);
            //Console.WriteLine(auth is PrintEdition);
            //Console.WriteLine();

            //вызов одноименных методов
            //auth.Same();     
            //((IPublish)auth).Same();
            //Console.WriteLine();

            //создание массива
            //Author[] mas = { person1, ed, wbook, pub as Author, auth1 };
            //for(int i = 0; i < mas.Length; i++)
            //{
            //    Printer.IAmPrinting(mas[i]);
            //}
        }
    }
}
