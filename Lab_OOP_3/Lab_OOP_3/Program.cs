using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_OOP_3
{
    public class Rectangle
    {
        private int height;
        private int width;
        private double diagonal;
        private double diagonal2;
        public readonly int ronly; //поле только для чтения
        private const string e = "Invalid value!";  //константа
        static int objct;   //статичекое поле
        public string type;
        public Rectangle()  //3 конструктора(с, без, по умолчанию)
        {
            height = 17;
            width = 17;
            diagonal = 9;
            diagonal2 = 19;
            type = "romb";
            ronly = GetHashCode();
            objct++;
        }
        public Rectangle(int height, int width, double diag = 8, double diag2 = 15)
        {
            this.height = height;
            this.width = width;
            diagonal = diag;
            diagonal2 = diag2;
            ronly = GetHashCode();
            objct++;
        }
        static Rectangle()  //статический
        {
            objct = 0;
        }
        public int Width    //свойства для полей
        {
            get { return width; }
            set
            {
                if (value <= 0)
                {
                    Console.WriteLine(e);
                }
                else
                {
                    width = value;
                }
            }
        }
        public int Height
        {
            get => height;
            set
            {
                if (value <= 0)
                {
                    Console.WriteLine(e);
                }
                else
                {
                    height = value;
                }
            }
        }
        public double Diagonal
        {
            get => diagonal;
            set
            {
                if (value <= 0)
                {
                    Console.WriteLine(e);
                }
                else
                {
                    diagonal = value;
                }
            }
        }
        public double Diagonal2
        {
            get => diagonal2;
            private set //ограничен доступ
            {
                if (value <= 0)
                {
                    Console.WriteLine(e);
                }
                else
                {
                    diagonal2 = value;
                }
            }
        }
        public override bool Equals(object obj) //переопределение
        {
            if (obj == null)
            {
                return false;
            }
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
            Rectangle temp = (Rectangle)obj;
            return this.height == temp.height && this.width == temp.width;
        }
        public override int GetHashCode()
        {
            int hash;
            string wd = Convert.ToString(width);
            hash = string.IsNullOrEmpty(wd) ? 0 : width.GetHashCode();
            hash = (hash * 47) + height.GetHashCode();
            return hash;
        }
        public override string ToString()
        {
            return "Type " + base.ToString() + " " + width + " " + height + " ";
        }
      
        public double Romb(ref double d1, out double d2)    //ref и out-параметры
        {
            double romb;
            d2 = d1 + 2;
            d1++;
            romb = 0.5 * d1 * d2;
            return romb;
        }
        public static void Info(Rectangle cl)   //статический метод
        {
            Console.WriteLine("ИНФОРМАЦИЯ О КЛАССЕ Rectangle:");
            Console.WriteLine("Количество объектов: " + objct);
            Console.WriteLine("Ширина сторон: " + cl.width);
            Console.WriteLine("Длина сторон: " + cl.height);
            Console.WriteLine("Длина первой диагонали: " + cl.diagonal);
            Console.WriteLine("Длина второй диагонали: " + cl.diagonal2);
            Console.WriteLine("Площадь: " + cl.Romb(ref cl.diagonal, out double d));
            Console.WriteLine("Длина первой диагонали:(1) " + cl.diagonal);
            Console.WriteLine("Длина второй диагонали(2): " + d);
        }
    }
    public partial class Calculations   //частичный класс
    {
        private Calculations()  //закрытый конструктор, нельзя создавать экземпляр
        { }
        public static int Perimeter(int width, int height)
        {
            int p;
            p = 2 * (width + height);
            return p;
        }
    }
    public partial class Calculations
    {
        public static int Area(int width, int height)
        {
            int a;
            a = width * height;
            return a;
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            int rect = 0;
            int sq = 0;
            int romb = 0;
            Rectangle[] arr = new Rectangle[7];
            arr[0] = new Rectangle(10, 10, 14.14, 14.14);
            arr[1] = new Rectangle(20, 20, 28.28, 28.28);
            arr[2] = new Rectangle(25, 13, 28.18, 28.18);
            arr[3] = new Rectangle(17, 17, 24.04, 24.04);
            arr[4] = new Rectangle(14, 14);
            arr[5] = new Rectangle();
            arr[6] = new Rectangle(7, 12, 13.9, 13.9);
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
            int[] mas = new int[sq];
            int i = 0;
            int[] mas1 = new int[rect];
            int k = 0;
            int[] mas2 = new int[romb];
            int z = 0;
            for (int j = 0; j < arr.Length; j++)
            {
                if(arr[j].type == "square")
                {
                    mas[i] = j;
                    i++;
                }
                if (arr[j].type == "rect")
                {
                    mas1[k] = j;
                    k++;
                }
                if (arr[j].type == "romb")
                {
                    mas2[z] = j;
                    z++;
                }
            }
            int max, min, t = 0, temp = 0;
            int max1, min1, t1 = 0, temp1 = 0;
            int max2, min2, t2 = 0, temp2 = 0;
            max = min = Calculations.Perimeter(arr[mas[0]].Width, arr[mas[0]].Height);
            max1 = min1 = Calculations.Perimeter(arr[mas1[0]].Width, arr[mas1[0]].Height);
            max2 = min2 = Calculations.Perimeter(arr[mas2[0]].Width, arr[mas2[0]].Height);
            for (int j = 0; j < mas.Length; j++)
            {
                int p = Calculations.Perimeter(arr[mas[j]].Width, arr[mas[j]].Height);
                if (max <= p)
                {
                    max = p;
                    t = mas[j];
                }
                if(min >= p)
                {
                    min = p;
                    temp = mas[j];
                }
            }
            for (int j = 0; j < mas1.Length; j++)
            {
                int p = Calculations.Perimeter(arr[mas1[j]].Width, arr[mas1[j]].Height);
                if (max1 <= p)
                {
                    max1 = p;
                    t1 = mas1[j];
                }
                if (min1 >= p)
                {
                    min1 = p;
                    temp1 = mas1[j];
                }
            }
            for (int j = 0; j < mas2.Length; j++)
            {
                int p = Calculations.Perimeter(arr[mas2[j]].Width, arr[mas2[j]].Height);
                if (max2 <= p)
                {
                    max2 = p;
                    t2 = mas2[j];
                }
                if (min2 >= p)
                {
                    min2 = p;
                    temp2 = mas2[j];
                }
            }

            Console.WriteLine("Количество квадратов: " + sq);
            Console.WriteLine("Количество прямоугольников: " + rect);
            Console.WriteLine("Количество ромбов: " + romb);
            Console.WriteLine("Наибольший по периметру из квадратов со сторонами: " + arr[t].Width+","+ arr[t].Height + "; наименьший: " + arr[temp].Width + "," + arr[temp].Height);
            Console.WriteLine("Наибольший по периметру из прямоугольников со сторонами: " + arr[t1].Width + "," + arr[t1].Height + "; наименьший: " + arr[temp1].Width + "," + arr[temp1].Height);
            Console.WriteLine("Наибольший по периметру из ромбов со сторонами: " + arr[t2].Width + "," + arr[t2].Height + "; наименьший: " + arr[temp2].Width + "," + arr[temp2].Height);

            bool result;
            result = arr[mas[0]].Equals(arr[mas[1]]);
            if (result == true)
            {
                Console.WriteLine("Два квадрата равны!");
            }
            else
            {
                Console.WriteLine("Два квадрата не равны!");
            }

            Console.WriteLine("arr[4].ToString : "+ arr[4].ToString());
            Console.WriteLine("Хэш-код : "+ arr[0].ronly);
            Console.WriteLine("Хэш-код : "+ arr[2].ronly);
            Console.WriteLine("Тип arr[0] - " + arr[0].GetType());
            Console.WriteLine("Площадь наибольшего по периметру прямоугольника - " + Calculations.Area(arr[t1].Width, arr[t1].Height));
            Rectangle.Info(arr[t2]);
                
            var anonObj = new { width = 10, height = 14, diag = 15, diag2 = 7 };    //анонимный тип
            Console.WriteLine(anonObj.GetType());
            Console.WriteLine("Ширина {0}, высота {1}, диагональ1 {2}, диагональ2 {3}",anonObj.width, anonObj.height, anonObj.diag, anonObj.diag2);
        }
    }
}
