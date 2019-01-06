using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_OOP_2
{
    class Program
    {
        static void Main(string[] args)
        {
            //1.a)Определите переменные всех возможных примитивных типов С#  и проинициализируйте их. 
            sbyte sb = 1;
            short sh = 2;
            int i = 3;
            long l = 4;
            byte b = 5;
            ushort ush = 6;
            uint ui = 7;
            ulong ul = 8;
            char ch = 'a';
            bool bl = true;
            float fl = 1.5f;
            double db = 2.345f;
            decimal dec = 9;
            string str = "hello!";
            object obj = "object";

            //1.b)Выполните 5 операций явного и 5 неявного приведения. 
            short v1 = (short)b;
            int v2 = (int)sh;
            long v3 = (long)i;
            double v4 = (double)fl;
            decimal v5 = (decimal)db;
            //
            ushort v6 = b;
            uint v7 = ush;
            ulong v8 = ui;
            float v9 = l;
            decimal v10 = sb;

            //1.c)Выполните упаковку и распаковку значимых типов. 
            int i1 = 32;
            object o = i1;
            long l1 = (long)(int)o;

            //1.d)Продемонстрируйте работу с неявно типизированной переменной. 
            var vi = 12;
            var vstr = "hi";
            Console.WriteLine(vi+" - "+vi.GetType() + "\n"+vstr+" - " + vstr.GetType());
            var vf = 'a';
            var vm = vf + vi;
            Console.WriteLine(vf + "+" + vi + "=" + vm + " - " + vm.GetType());

            //1.e)Продемонстрируйте пример работы с Nullable переменной.
            int? x0 = null;
            int? x1 = null;
            float? x2 = 1.23f;
            Console.WriteLine(x1 == x2);
            int x = x0 ?? 1;    //null-объединение
            Console.WriteLine(x);

            //2.a)Объявите строковые литералы. Сравните их.
            //2.b)Создайте три строки на основе String. Выполните: сцепление, копирование, выделение подстроки, 
            //разделение строки на слова, вставки подстроки в заданную позицию, удаление заданной подстроки.
            string str1 = "ПОИТ";
            string str2 = "ИСиТ";
            string str3 = "ДЭиВИ";
            Console.WriteLine(str1 == str2);

            Console.WriteLine(string.Concat(str1,str2));
            string st = string.Copy(str3);
            Console.WriteLine(st);
            Console.WriteLine(str3.Substring(1,3));
            Console.WriteLine(str3.Split('и')[0]);
            Console.WriteLine(str2.Insert(2, str1));
            Console.WriteLine(str1.Remove(1, 2));

            //2.c)Создайте пустую и null строку. Продемонстрируйте что можно выполнить с такими строками 
            string empty = "";
            string nul = null;
            Console.WriteLine(string.Concat(empty,nul)+"\t"+(empty==nul)+"\t"+empty.Length+"\t"+string.Concat(nul,st));

            //2.d)Создайте строку на основе StringBuilder. Удалите определенные позиции и добавьте новые символы в начало и конец строки. 
            StringBuilder NewStr = new StringBuilder("Students ");
            Console.WriteLine(NewStr.Remove(1,2));
            Console.WriteLine(NewStr.Insert(0, "Petrov "));
            Console.WriteLine(NewStr.Insert(NewStr.Length, "Ivanov "));//or
            Console.WriteLine(NewStr.Append("Sidorov "));

            //3.a)Создайте целый двумерный массив и выведите его на консоль в отформатированном виде(матрица).
            int[,] array = new int[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
            for (int k = 0; k < 3; k++)
            {
                for(int j = 0; j < 3; j++)
                {
                    Console.Write(array[k, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();

            //3.b)Создайте одномерный массив строк. Выведите на консоль его содержимое, длину массива. 
            //Поменяйте произвольный элемент(пользователь определяет позицию и значение).
            string[] mas = { " first ", " second ", " third ", " fourth " };
            foreach (var m in mas)
                Console.Write(m);
            Console.WriteLine("\nmas.Length = "+mas.Length);
            Console.WriteLine("Write position(0-3)");
            int position = int.Parse(Console.ReadLine());
            Console.WriteLine("Write value");
            mas[position] = Console.ReadLine();
            for (int z = 0; z < mas.Length; z++)
                Console.Write(mas[z]);
            Console.WriteLine();

            //3.c)Создайте ступечатый(не выровненный) массив вещественных чисел с 3 - мя строками, в каждой 
            //из которых 2, 3 и 4 столбцов соответственно. Значения массива введите с консоли.
            float[][] arr = new float[3][];
            arr[0] = new float[2];
            arr[1] = new float[3];
            arr[2] = new float[4];
            Console.WriteLine("Write float numbers:");
            for (int h = 0; h < 3; h++)
            {
                for (int j = 0; j < arr[h].Length; j++)
                {
                    Console.Write("arr[{0}][{1}] ",h, j);
                    arr[h][j] = Convert.ToSingle(Console.ReadLine());
                }
            }

            for (int h = 0; h < 3; h++)
            {
                for (int j = 0; j < arr[h].Length; j++)
                {
                    Console.Write(arr[h][j] + " ");
                }
                Console.WriteLine();
            }

            //3.d)Создайте неявно типизированные переменные для хранения массива и строки.
            var VarArray = new[] { 1, 2, 3 };
            var VarString = "VarString";

            //4.a)Задайте кортеж из 5 элементов с типами int, string, char, string, ulong.
            //4.b)Сделайте именование его элементов.
            (int first, string second, char third, string fourth, ulong fifth) tuple = (18, "Polina", 's', "POIT", 4);
            //var tuple = (first:18, second:"Polina", third:'s', fourth:"POIT", fifth:4);

            //4.c)Выведите кортеж на консоль целиком и выборочно(1, 3, 4  элементы) 
            Console.WriteLine($"{tuple}");
            Console.WriteLine(tuple.first + " " + tuple.third + " " + tuple.fourth);

            //4.d)Выполните распаковку кортежа в переменные.
            var (one, two, three, four, five) = (tuple.first, tuple.second, tuple.third, tuple.fourth,tuple.fifth);

            //4.e)Сравните два кортежа.
            var type1 = (a: 2, b: 'A');
            var type2 = (c: 2, d: "Blabla");
            Console.WriteLine("Кортежи {0} и {1} - {2}", type1, type2, Equals(type1, type2) ? "равны" : "не равны");

            //5.Создайте локальную функцию в main и вызовите ее. Формальные параметры функции – массив целых и строка. 
            //Функция должна вернуть кортеж, содержащий: максимальный и минимальный элементы массива, сумму элементов массива и первую букву строки.
            int[] masiv = { 7, 45, 32, 78 };
            string aaa = "This is string";
            (int,int,int,string) Func(int[] massiv, string stroka)
            {
                return (massiv.Max(), massiv.Min(), massiv.Sum(), stroka.Substring(0, 1));
            }
            Console.WriteLine(Func(masiv, aaa));

            Console.ReadKey();
        }
    }
}
