using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_OOP_10
{
    class Student
    {
        public string name;
        public Student()
        {
            name = "Unknown";
        }
    }
    public class Author:IComparable<Author>,IComparer<Author>
    {                             
        protected string firstName;
        protected int years;
        public string FirstName
        {
            get => firstName;
            set => firstName = value;
        }
        public int Years
        {
            get => years;
            set => years = value;
        }
        public Author(string name,int yy)
        {
            firstName = name;
            years = yy;
        }
        public int CompareTo(Author obj)    //IComparable<Author>
        {
            if (this.Years > obj.Years)
                return 1;
            if (this.Years < obj.Years)
                return -1;
            else
                return 0;
        }
        public int Compare(Author first,Author second)  //IComparer<Author>
        {
            if (first.Years > second.Years)
                return 1;
            if (first.Years < second.Years)
                return -1;
            else
                return 0;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //1. Создать необобщенную коллекцию ArrayList.
            //a.Заполните ее 5 - ю случайными целыми числами
            //b.Добавьте к ней строку
            //c.Добавьте объект типа Student
            //d.Удалите заданный элемент
            //e.Выведите количество элементов и коллекцию на консоль.
            //f.Выполните поиск в коллекции значения

            ArrayList array = new ArrayList();  //динамический массив
                    //необобщенная коллекция оперирует данными типа object(не типизированны) - может включать в себя различный тип данных
            Random ran = new Random();
            for (int i = 0; i < 5; i++)
                array.Add(ran.Next(1, 20));

            array.Add("string");

            Student student = new Student();
            array.Add(student);

            array.RemoveAt(2);

            Console.Write("Count of elements: " + array.Count + "\narray ArrayList: ");
            foreach (var i in array)
            {
                Console.Write(i + " ");
            }

            Console.WriteLine("\nThe array contains a 'string': {0} in {1} position" , array.Contains("string"),array.IndexOf("string"));
            Console.WriteLine("The array contains a '25': {0} in {1} position", array.Contains(25), array.IndexOf(25));

            //2. Создать обобщенную коллекцию в соответствии с вариантом задания и заполнить ее данными, тип которых определяется вариантом задания(колонка – первый тип).
            //a.Вывести коллекцию на консоль
            //b.Удалите из коллекции n последовательных элементов
            //c.Добавьте другие элементы(используйте все возможные методы добавления для вашего типа коллекции).
            //d.Создайте вторую коллекцию(см.таблицу) и заполните ее данными из первой коллекции.
            //e.Выведите вторую коллекцию на консоль.В случае не совпадения количества параметров(например, LinkedList<T> и Dictionary<Tkey, TValue>), при нехватке -генерируйте ключи, в случае избыточности – оставляйте TValue.
            //f.Найдите во второй коллекции заданное значение.

            LinkedList<char> llist = new LinkedList<char>();    //двусторонний упорядоченный список
            llist.AddFirst('a');    //обобщенная коллекция (типизированна) содержит элементы типа T
            llist.AddLast('b');
            llist.AddLast('c');
            llist.AddLast('d');
            llist.AddLast('e');

            LinkedListNode<char> node = llist.First;
            node = node.Next;
            node = node.Previous;
            Console.WriteLine("\nLinkedList:");
            for (node = llist.First; node != null; node = node.Next)
            {
                Console.Write(node.Value + "\t");
            }

            Console.WriteLine("\nInput count for remove:");
            int x = int.Parse(Console.ReadLine());
            for (int i = 0; i < x; i++)
            {
                llist.RemoveFirst();
            }
            for (node = llist.First; node != null; node = node.Next)
            {
                Console.Write(node.Value + "\t");
            }

            node = llist.First;
            llist.AddFirst('f');
            llist.AddAfter(node,'g');
            llist.AddBefore(node, 'h');
            llist.AddLast('i');
            Console.WriteLine();
            for (node = llist.First; node != null; node = node.Next)
            {
                Console.Write(node.Value + "\t");
            }
            
            HashSet<char> hset = new HashSet<char>();   //неупорядоченный набор значений
            Console.WriteLine("\n\nHashSet:");
            for (node = llist.First; node != null; node = node.Next)
            {
                hset.Add(node.Value);
            }
            foreach(var a in hset)
            {
                Console.Write(a+" ");
            }
            Console.WriteLine("\nThe HashSet contains a 's': {0}", hset.Contains('s'));
            Console.WriteLine("The HashSet contains a 'f': {0}", hset.Contains('f'));


            //Повторите задание п.2 для пользовательского типа данных (в качестве типа T возьмите любой свой класс из лабораторной №5(Наследование…. ).
            //Не забывайте о необходимости реализации интерфейсов(IComparable, ICompare,….).При выводе коллекции используйте цикл foreach.
            Author author1 = new Author("Alex", 30);
            Author author2 = new Author("Nick", 45);
            Author author3 = new Author("John", 50);
            Author author4 = new Author("Stiv", 16);
            LinkedList<Author> llistA = new LinkedList<Author>();
            llistA.AddFirst(author1);
            llistA.AddLast(author2);
            llistA.AddLast(author3);
            llistA.AddLast(author4);

            LinkedListNode<Author> nodeA = llistA.First;
            nodeA = nodeA.Next;
            nodeA = nodeA.Previous;
            Console.WriteLine("\nLinkedList<Author>:");
            for (nodeA = llistA.First; nodeA != null; nodeA = nodeA.Next)
            {
                Console.Write(nodeA.Value.FirstName + "\t");
            }

            Console.WriteLine("\nInput number for remove:");
            int xA = int.Parse(Console.ReadLine());
            for (int i = 0; i < xA; i++)
            {
                llistA.RemoveFirst();
            }
            for (nodeA = llistA.First; nodeA != null; nodeA = nodeA.Next)
            {
                Console.Write(nodeA.Value.FirstName + "\t");
            }

            nodeA = llistA.First;
            llistA.AddFirst(author3);
            llistA.AddAfter(nodeA, author4);
            llistA.AddBefore(nodeA, author1);
            llistA.AddLast(author2);
            Console.WriteLine();
            for (nodeA = llistA.First; nodeA != null; nodeA = nodeA.Next)
            {
                Console.Write(nodeA.Value.FirstName + "\t");
            }

            HashSet<Author> authors = new HashSet<Author>
            {
                author1,
                author2,
                author3,
                author4
            };
            Console.WriteLine("\n\nHashSet<Author>:");
            foreach (var j in authors)
            {
                Console.Write(j.FirstName + " ");
            }
            Console.WriteLine("\nThe HashSet contains a author2: " + authors.Contains(author2));
            Author author5 = new Author("AA",3);
            Console.WriteLine("The HashSet contains a author5: "+authors.Contains(author5));
            Console.WriteLine();

            //Создайте объект наблюдаемой коллекции ObservableCollection<T>. 
            //Создайте произвольный метод и зарегистрируйте его на событие CollectionChange.
            //Напишите демонстрацию с добавлением и удалением элементов.В качестве типа T используйте свой класс из лабораторной №5 Наследование….
            ObservableCollection<Author> obs = new ObservableCollection<Author>();
            obs.CollectionChanged += CollectionChanged; //наблюдаемая коллекция - кол-ция динамич данных, обеспеч выдачу уведомлений при изм коллекции
            obs.Add(new Author("George", 32));
            obs.Add(new Author("Walt", 77));
            obs.RemoveAt(1);
            void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
            {
                switch (e.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        Author newA = e.NewItems[0] as Author;
                        Console.WriteLine("Object has been added: " + newA.FirstName);
                        break;
                    case NotifyCollectionChangedAction.Remove:
                        Author oldA = e.OldItems[0] as Author;
                        Console.WriteLine("Object has been removed: "+oldA.FirstName);
                        break;
                }
            }
            foreach(Author i in obs)
            {
                Console.WriteLine(i.FirstName+ " " + i.Years );
            }
        }
    }
}
