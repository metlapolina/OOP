using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_OOP_4
{
    public class Owner
    {
        public int id;
        public string name;
        public string organization;

        public Owner(int id, string name, string organization) {
            this.id = id;
            this.name = name;
            this.organization = organization;
        }
    }

    class List
    {
        //Добавьте в свой класс вложенный объект Owner, который содержит Id, имя и организацию создателя.
        public Owner owner;

        //Добавьте в свой класс вложенный класс Date (дата создания). 
        public class Date
        {
            public int day;
            public int month;
            public int year;

            public Date(int day, int month, int year)
            {
                this.day = day;
                this.month = month;
                this.year = year;
            }
        }

        public List()
        {
            this.MyList = new List<int>();
            this.owner = new Owner( 1, "Metla Polina", "BSTU");
        }

        public List<int> MyList { get; set; }
        public int Count => this.MyList.Count;  //количество
        public void Push(int element) => this.MyList.Add(element);  //добавление элемента
        public int Pop()    //удаление элемента
        {
            int lastElementIndex = this.MyList.Count - 1;
            int lastElement = this.MyList[lastElementIndex];
            this.MyList.RemoveAt(lastElementIndex);
            return lastElement;
        }

        public void Remove(int pos) //удаление элемента в заданной позиции
        {
            this.MyList.RemoveAt(pos);
        }

        //индексатор
        public int this[int index]
        {
            get => this.MyList[index];

            set => this.MyList[index] = value;
        }

        //операции сравнения перегружаются парами: == и != 
        //если перегружаются операторы == и !=, то для этого требуется переопределить методы Object.Equals() и Object.GetHashCode().
        public override bool Equals(object list) //переопределение
        {
            if (list == null)
            {
                return false;
            }
            if (list.GetType() != this.GetType())
            {
                return false;
            }
            List temp = (List)list;
            return this.Count == temp.Count && this.MyList == temp.MyList;
        }
        public override int GetHashCode()
        {
            int hash;
            string wd = Convert.ToString(Count);
            hash = string.IsNullOrEmpty(wd) ? 0 : Count.GetHashCode();
            hash = (hash * 47) + Count.GetHashCode();
            return hash;
        }

        //перегрузка операторов
        // + - добавить элемент
        public static List operator +(List list, int element)
        {
            list.Push(element);
            return list;
        }

        // >> - удалить элемент в заданной позиции
        public static List operator >>(List list, int position)
        {
            list.Remove(position);
            return list;
        }

        public static bool operator ==(List list1, List list2)
        {
            return list1.Equals(list2);
        }
        //проверка на неравенство множеств
        public static bool operator !=(List list1, List list2)
        {
            return !list1.Equals(list2);
        }
    }

    //Создайте статический класс MathOperation, содержащий 3 метода для работы с вашим классом: 
    //поиск максимального, минимального, подсчет количества элементов.
    static class MathOperation
    {
        public static int GetMaxElement(List list)  //поиск мах элемента
        {
            int[] temp = new int[list.Count];
            for (int i = temp.Length - 1; i >= 0; i--)
            {
                temp[i] = list.Pop();
            }
            return temp.Max();
        }

        public static int GetMinElement(List list)  //поиск мин элемента
        {
            int[] temp = new int[list.Count];
            for (int i = temp.Length - 1; i >= 0; i--)
            {
                temp[i] = list.Pop();
            }
            return temp.Min();
        }

        public static int GetCount(List list)   //подсчет количества элементов
        {
            return list.Count;
        }

        //Добавьте к классу MathOperation методы расширения для типа string и  вашего типа
        public static int MaxLength(this string[] str)  //поиск самого длинного слова
        {
            int max = str[0].Length;
            int k = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (max <= str[i].Length)
                {
                    max = str[i].Length;
                    k = i;
                }
            }
            return k;
        }

        public static List Del(this List list)  //удаление последнего элемента
        {
            list.Pop();
            return list;
        }
    }

    //Написать программу тестирования, в которой проверяется использование перегруженных операций.
    class Program
    {
        static void Main(string[] args)
        {
            List myList1 = new List();
            List myList2 = new List();
            List myList3 = new List();
            
            myList1 = myList1 + 1;  //добавление элемента
            myList1 = myList1 + 23;
            myList1 = myList1 + (-2);
            myList1 = myList1 + 456;
            myList1 = myList1 + 78; 
            Console.WriteLine("Count of List1: " + myList1.Count);
            Console.WriteLine("List1: ");
            for (int i = 0;i< myList1.Count;i++)
            {
                Console.WriteLine(myList1[i]);
            }

            myList2 = myList2 + 0;
            myList2 = myList2 + 5;
            Console.WriteLine("\nCount of List2: " + myList2.Count);
            Console.WriteLine("List2: ");
            for (int i = 0; i < myList2.Count; i++)
            {
                Console.WriteLine(myList2[i]);
            }

            myList3 = myList1 >> 1; //удаление элемента в заданной позиции
            Console.WriteLine("\nCount of List3: " + myList3.Count);
            Console.WriteLine("List3: ");
            for (int i = 0; i < myList3.Count; i++)
            {
                Console.WriteLine(myList3[i]);
            }

            Console.WriteLine("\nList1 != List2 : " + (myList1 != myList2));    //проверка на неравенство множеств
            Console.WriteLine("List1 = List3 : " + (myList1 == myList3));   //проверка на равенство множеств

            //удаление последнего элемента и возврат количества элементов списка
            Console.WriteLine("Delete last element of List3 and count of List3: " + MathOperation.GetCount(MathOperation.Del(myList3)));
            Console.WriteLine("List3: ");
            for (int i = 0; i < myList3.Count; i++)
            {
                Console.WriteLine(myList3[i]);
            }
            //нахождение мах и мин элементов
            Console.WriteLine("\nMin element of List1: " + MathOperation.GetMinElement(myList1));
            Console.WriteLine("Max element of List2: " + MathOperation.GetMaxElement(myList2));

            Console.WriteLine("---------------------------");
            int k;
            string[] array = {"Hello","from","the","other","siiiiide" };
            //нахождение самого длинного слова
            k = MathOperation.MaxLength(array);
            Console.WriteLine("Element with max length of array: "+array[k]);
                         
            Console.WriteLine("---------------------------");
            List.Date date = new List.Date(6, 10, 2018);
            Console.WriteLine("Owner: {0} {1} {2}", myList1.owner.id, myList1.owner.name, myList1.owner.organization);
            Console.WriteLine("Creation date: {0}.{1}.{2}", date.day, date.month, date.year);

        }
    }
}
