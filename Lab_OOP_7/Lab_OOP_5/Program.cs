using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

//Создать иерархию классов исключений (собственных) – 3 типа и более.
//Сделать наследование пользовательских типов исключений от стандартных классов.Net(например Exception).
//Сгенерировать и обработать как минимум пять различных исключительных ситуаций на основе своих и стандартных исключений.
namespace Lab_OOP_5
{
    #region
    struct Information 
    {
        public string book;
        public string person;
        public string name;
    }
    enum Book          
    {
        PrintEdition, Workbook, Magazine = 5, Book
    }
    interface IPublisher
    {                                       
        void Info();
    }
    interface IPublish
    {                                       
        void Same();                        
    }
    #endregion
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
    
    public static class Printer
    {
        public static void IAmPrinting(Author author)
        {
            Console.WriteLine(author.GetType());
            Console.WriteLine(author.ToString());
        }
    }
    
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
            if (printEd.Count == 0)
            {
                throw new EmptyException("No items of PrintEditions");          ////
            }
            for (int j = 0; j < workbooks.Count; j++)
            {
                Console.WriteLine("Workbooks[{0}] = {1}", j, workbooks[j].NameOfW);
            }
            if (workbooks.Count == 0)
            {
                throw new EmptyException("No items of Workbooks");          ////
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
            throw new Exception();                                          //exception
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
    
    public class LibraryController
    {
        public void Show(Library lib, int y)
        {
            Console.WriteLine("What is your name?");
            string answer = Console.ReadLine();
            if (answer == "Polina")
            {
                lib.ShowList(y);
            }
            else
            {
                throw new SecretException("You are a stranger. The entrance is closed.");       ////
            }
        }
        public int Count(Library lib) { int a = lib.CountOfBooks(); return a; }
        //Добавьте в код одной из функций макрос Assert. Объясните что он проверяет, как
        //будет выполняться программа в случае не выполнения условия.Объясните назначение Assert.
        //public int Cost(Library lib)
        //{
        //    Debug.Assert(DateTime.Now.DayOfWeek != DayOfWeek.Tuesday, "Hi! Today is Tuesday. All books are free.");     ////аварийное завершение
        //    int b = lib.CostOfBooks(); return b;                                                                    //если условие не выполняется        
        //}
    }
    //В конце поставить универсальный обработчик catch.
    //Обработку исключений вынести в main.При обработке выводить
    //специфическую информацию о месте, диагностику и причине исключения.
    //Последним должен быть блок, который отлавливает все исключения (finally).
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                int d = 0;              
                //int s = 4/d;                                                            //exception

                Library library = new Library();
                LibraryController libraryControl = new LibraryController();
                PrintEdition ed = new PrintEdition("fg", "Sun", "Morning", 2001, 20);     //exception
                PrintEdition ed1 = new PrintEdition("Alice", "Willson", "Sunday", 2005, 15);
                PrintEdition ed2 = new PrintEdition("Petr", "Komarov", "Good things", 2015, 40);
                Workbook wb = new Workbook("Reck", "Si", "Hello", 2012, 20);
                if (ed.FirstName.Length == 0)
                {
                    throw new NoNameException("The author isn't named");            ////
                }
                library.AddPrintEd(ed);
                library.AddPrintEd(ed1);
                library.AddPrintEd(ed2);
                library.AddWorkbook(wb);
                library.ToConsoleList();                                                    //exception
                Console.WriteLine("Count of books in the library: " + libraryControl.Count(library));     
                //Console.WriteLine("The cost of books in the library: " + libraryControl.Cost(library));   //assert
                Console.WriteLine("The list of books in the library: ");
                libraryControl.Show(library, 2005);                         //exception
                Console.WriteLine("Congratulations! Your code is error free");
            }
            catch (EmptyException exception)
            {
                exception.GetInfo();
            }
            catch (NoNameException exception)
            {
                exception.GetInfo();
            }
            catch (SecretException exception)
            {
                exception.GetInfo();
            }
            catch (DivideByZeroException exception)
            {
                Console.WriteLine(exception.Message);
                Console.WriteLine(exception.StackTrace);
                Console.WriteLine(exception.TargetSite);
            }
            catch
            {
                Console.WriteLine("Unknown error");
            }
            finally
            {
                Console.WriteLine("I'm a block FINALLY");
            }
        }
    }
}
