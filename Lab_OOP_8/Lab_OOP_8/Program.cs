using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_OOP_8
{
    //Создайте обобщенный интерфейс с операциями добавить, удалить, просмотреть.
    interface IFunction <T> where T : struct
    {
        void Add(T element);
        void Remove(int pos);
        void Show();
    }
    //Создайте обобщенный класс на основе 4 л.р.
    //Наследуйте обобщенный интерфейс, реализуйте необходимые методы.
    //Наложите какое-либо ограничение на обобщение.
    public class CollectionType<T> : IFunction<T> where T : struct
    {
        public T element;
        public List<T> List { get; set; }
        public int Count => this.List.Count;
        public CollectionType()
        {
            this.List = new List<T>();
            this.element = default(T);
        }
        public CollectionType(T el)
        {
            this.List = new List<T>();
            this.element = el;
        }
        public T Pop()
        {
            int lastElementIndex = this.List.Count - 1;
            T lastElement = this.List[lastElementIndex];
            this.List.RemoveAt(lastElementIndex);
            return lastElement;
        }
        public void Add(T el) {
            if (el.Equals(0))
            {
                throw new Exception("***You cannot add element with a value of 0***");
            }
            List.Add(el);
        }
        public void Show()
        {
            if (List.Count == 0)
            {
                throw new Exception("***Empty List***");
            }
            for (int i = 0; i < List.Count; i++)
            {
                Console.WriteLine((i+1)+" element of list: "+List[i]);
            }
        }
        public void Remove(int pos)
        {
            this.List.RemoveAt(pos);
        }
        //Дополнительно
        //Добавьте методы сохранения объекта (объектов) обобщённого типа CollectionType<T> в файл и чтения из него.
        public void ToFile(CollectionType<T> type)
        {
            int index = type.Count;
            string[] text = new string[index];
            for (int i = 0; i < index; i++)
            {
                text[i] = Convert.ToString(type.Pop());
            }
            File.WriteAllLines(@"D:\l.txt", text);
        }
        public void FromFile()
        {
            Console.WriteLine(File.ReadAllText(@"D:\l.txt"));
        }
    }
    //Определить пользовательский класс, который будет использоваться в качестве параметра обобщения(из 5 л.р.).
    struct Workbook
    {
        public string firstName;
        public string lastName;
        public string nameOfW;
        public Workbook(string name, string sname, string nameOfW)
        {
            this.firstName = name;
            this.lastName = sname;
            this.nameOfW = nameOfW;
        }
        public void Info()
        {
            Console.WriteLine("The author of " + nameOfW + " is " + firstName + " " + lastName + ".");
        }
    }
    //Добавьте обработку исключений с finally.
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                CollectionType<int> list = new CollectionType<int>();
                CollectionType<short> list1 = new CollectionType<short>(2);

                //list.Show();              //exception
                list.Add(9);
                //list.Add(0);              //exception
                list.Add(78);
                list.Add(22);
                list.Add(54);
                list.Add(20);
                list.Add(1);
                list.Add(58);
                list.Add(77);
                list.Show();

                Workbook a = new Workbook("A","AA","AAA");
                Workbook b = new Workbook("B", "BB", "BBB");
                CollectionType <Workbook> wb1 = new CollectionType<Workbook>();
                CollectionType<Workbook> wb2 = new CollectionType<Workbook>();
                wb1.Add(a);
                wb1.Add(b);
                list.ToFile(list);
                list.FromFile();
                //wb1.ToFile(wb1);
                //wb1.FromFile();
                Console.WriteLine("Without exceptions!!!");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            finally
            {
                Console.WriteLine("The end.");
            }

        }
    }
}
