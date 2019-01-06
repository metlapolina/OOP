using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_OOP_11
{
    public partial class Program
    {
        static void Main(string[] args)
        {
            //1.Задайте массив типа string, содержащий 12 месяцев (June, July, May,December, January ….). 
            //Используя LINQ to Object напишите:
            //запрос выбирающий последовательность месяцев с длиной строки равной n, 
            //запрос возвращающий только летние и зимние месяцы, 
            //запрос вывода месяцев в алфавитном порядке,
            //запрос считающий месяцы содержащие букву «u» и длиной имени не менее 4 - х..
            string[] months = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
            int n;
            Console.Write("Enter string length n:");
            n = int.Parse(Console.ReadLine());
            Console.WriteLine("1. Months with a string length of " +n+":");
            IEnumerable<string> length = months
                .Where(p => (p.Length == n))
                .Select(p=>p);
            foreach(string month in length)
            {
                Console.WriteLine(month);
            }
            Console.WriteLine();

            Console.WriteLine("2. Summer and winter months:");
            IEnumerable<string> sumwin = months
                .Where(p => (p.Equals("December")|| p.Equals("January") || p.Equals("February") ||p.Equals("June") || p.Equals("July") || p.Equals("August")))
                .Select(p => p);
            foreach (string month in sumwin)
            {
                Console.WriteLine(month);
            }
            Console.WriteLine();

            Console.WriteLine("3. Months in alphabetical order:");
            IEnumerable<string> alph = months
                .OrderBy(p => p)
                .Select(p => p);
            foreach (string month in alph)
            {
                Console.WriteLine(month);
            }
            Console.WriteLine();

            Console.WriteLine("4. Months containing a letter 'u' and a length >= 4:");
            IEnumerable<string> contU = months
                .Where(p => (p.Contains('u')&&p.Length>=4))
                .Select(p => p);
            foreach (string month in contU)
            {
                Console.WriteLine(month);
            }
            Console.WriteLine("\n---------------------------------------------------\n");


            //2.Создайте коллекцию List<T> и параметризируйте ее типом (классом)
            //из лабораторной №3(при необходимости реализуйте нужные интерфейсы).
            Rectangle[] arr = new Rectangle[7];
            arr[0] = new Rectangle(10, 10, 14.14, 14.14);
            arr[1] = new Rectangle(20, 20, 28.28, 28.28);
            arr[2] = new Rectangle(25, 13, 28.18, 28.18);
            arr[3] = new Rectangle(17, 17, 24.04, 24.04);
            arr[4] = new Rectangle(14, 14);
            arr[5] = new Rectangle();
            arr[6] = new Rectangle(7, 12, 13.9, 13.9);

            List<Rectangle> list = new List<Rectangle>
            {
                arr[0],arr[1],arr[2],arr[3],arr[4],arr[5],arr[6]
            };

            //3.На основе LINQ сформируйте следующие запросы по вариантам. При необходимости добавьте в класс T(тип параметра) свойства.
            //определить для каждой группы наибольший и наименьший по площади(периметру) объект
            //Массив квадратов со стронной не более x
            //Упорядоченный по периметру массив прямоугольников
            #region Определение типов четырёхугольников
            int rect = 0;
            int sq = 0;
            int romb = 0;
            for (int j = 0; j < arr.Length; j++)
            {
                if (arr[j].Height == arr[j].Width && arr[j].Diagonal == arr[j].Diagonal2)
                {
                    sq++;
                    arr[j].type = "square";
                }
                else if (arr[j].Diagonal == arr[j].Diagonal2)
                {
                    rect++;
                    arr[j].type = "rect";
                }
                else
                {
                    romb++;
                    arr[j].type = "romb";
                }
            }
            #endregion
            for (int j = 0; j < arr.Length; j++)
            {
                arr[j].perim = Calculations.Perimeter(arr[j].Height, arr[j].Width);
            }
            IEnumerable<Rectangle> rectan = list
                .Where(p => p.type.Equals("rect"))
                .OrderBy(p => p.perim)
                .Select(p => p);
            Console.WriteLine("Min rectangle - {0}, max rectangle - {1}", rectan.First(), rectan.Last());
            IEnumerable<Rectangle> squ = list
                .Where(p => p.type.Equals("square"))
                .OrderBy(p => p.perim)
                .Select(p => p);
            Console.WriteLine("Min square - {0}, max square - {1}", squ.First(), squ.Last());
            IEnumerable<Rectangle> rom = list
                .Where(p => p.type.Equals("romb"))
                .OrderBy(p => p.perim)
                .Select(p => p);
            Console.WriteLine("Min romb - {0}, max romb - {1}\n", rom.First(), rom.Last());

            Console.Write("Enter the side of the square x=");
            int x = int.Parse(Console.ReadLine());
            Console.WriteLine("Squares with side <= "+x+":");
            IEnumerable<Rectangle> square = list
                .Where(p => p.type.Equals("square") && p.Width <= x)
                .Select(p => p);
            foreach (var s in square)
            {
                Console.WriteLine(s);
            }
            Console.WriteLine();

            Console.WriteLine("Rectangles ordered by perimetr:");
            IEnumerable<Rectangle> rec = list
                .Where(p => p.type.Equals("rect"))
                .OrderBy(p=>p.perim)
                .Select(p => p);
            foreach (var r in rec)
            {
                Console.WriteLine(r);
            }
            Console.WriteLine();

            //4.Придумайте и напишите свой собственный запрос, в котором было бы не менее 5 операторов из разных категорий: 
            //условия, проекций, упорядочивания, группировки, агрегирования, кванторов и разиения.
            Console.WriteLine("Own request:");
            IEnumerable<Rectangle> own = list
                .Skip(1)
                .Where(p => p.Height>10)
                .OrderByDescending(p => p.perim)
                .Where(p=>p.type.Equals("square"))
                .Take(2);
            foreach (var o in own)
            {
                Console.WriteLine(o);
            }
            Console.WriteLine();

            //5.Придумайте запрос с оператором Join
            //выполняет внутреннее соединение по эквивалентности
            //двух последовательностей на основе ключей
            Console.WriteLine("Request with Join:");
            int[] key= new int[7];
            for (int i = 0; i < list.Count; i++) {
                key[i] = list[i].Height; }
            var sometype = list //внутренняя последовательность
            .Join(
            key,                //внешняя последовательность
            w => w.Width,       //внутренний ключ выбора
            h => h,             //внешний ключ выбора
            (w, h) => new       //результат
            {
                type = string.Format("{0}",w.type),
                hw = h,
            });
            foreach (var item in sometype)  //получили объекты с равными сторонами
                Console.WriteLine(item);
        }
    }
}
