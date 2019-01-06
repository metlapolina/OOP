using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;

namespace Lab_OOP_12
{
    interface ITest
    {
        int Sum(int x, int y);
    }
    interface IDo
    {
        void ToDo();
    }

    class Test : ITest, IDo
    {
        public int test1;
        public int test2;
        public string name;
        public Test()
        {
            test1 = 50;
            test2 = 50;
            name = "Brain";
        }
        public Test(int test1, int test2, string name)
        {
            this.test1 = test1;
            this.test2 = test2;
            this.name = name;
        }
        public int Test1{
            get => test1;
            set => test1 = value;
        }
        public int Test2
        {
            get => test2;
            set => test2 = value;
        }
        public string Name
        {
            get => name;
            set => name = value;
        }
        public int Sum(int x, int y)
        {
            return x + y;
        }
        public void ToDo()
        {
            Console.WriteLine("JUST DO IT");
        }
        public override string ToString()
        {
            return "Test";
        }
        public string ToConsole(string str)
        {
            return str;
        }
        private void Private()
        {
            Console.WriteLine("Private place");
        }
    }
    //Для изучения .NET Reflection API допишите класс Рефлектор, который будет содержать методы выполняющие следующие действия:
    //рефлексия - процесс выявления типов во время выполнения приложения
    class Reflector
    {
        public Type type;
        public Reflector(string type)
        {
            this.type = Type.GetType(type); //получаем объект Type
        }
        //a.выводит всё содержимое класса в текстовый файл;
        public void AboutClass()
        {
            using (FileStream fstream = new FileStream("class.txt", FileMode.Create))
            {
                StreamWriter sw = new StreamWriter(fstream);
                foreach (MemberInfo info in type.GetMembers())  //GetMembers возвр. члены(св-ва, методы, поля и т. д.) текущего объекта Type.
                {
                    sw.WriteLine(info.DeclaringType + " - " + info.MemberType + " - " + info.Name + "\n");
                }                   //класс                  //тип члена класса         //имя члена класса
                sw.Close();
            }
        }
        //b.извлекает все общедоступные публичные методы класса;
        public void PublicMethods()
        {
            Console.WriteLine("Public methods:");
            foreach (MethodInfo method in type.GetMethods())
            {
                if (method.IsPublic)    //если публичный, то выводит
                {
                    Console.WriteLine(method.Name);
                }
            }
        }
        //c.получает информацию о полях и свойствах класса;
        public void Fields()
        {
            Console.WriteLine("\nFields and Properties:");
            foreach (FieldInfo field in type.GetFields())
            {
                Console.WriteLine(field.FieldType + " - " + field.Name);
            }
            foreach (PropertyInfo prorertie in type.GetProperties())
            {
                Console.WriteLine(prorertie.PropertyType + " - " + prorertie.Name);
            }
        }
        //d.получает все реализованные классом интерфейсы;
        public void Interfaces()
        {
            Console.WriteLine("\nInterfaces:");
            foreach (Type interfaces in type.GetInterfaces())
            {
                Console.WriteLine(interfaces.Name);
            }
        }
        //e.выводит по имени класса имена методов, которые содержат заданный(пользователем) 
        //тип параметра(имя класса передается в качестве аргумента);
        public void Methods(Test test)
        {
            bool fl = false;
            Type t = typeof(Test);
            MethodInfo[] methods = t.GetMethods();
            Console.WriteLine("\nEnter the type of the args(Int32, Double, String...): ");
            string arg = Console.ReadLine();
            Console.WriteLine("Methods of class {0} with args type {1}:", test, arg);
            for (int i = 0; i < methods.Length; i++)
            {
                ParameterInfo[] param = methods[i].GetParameters();
                for (int j = 0; j < param.Length; j++)
                {
                    if (arg == param[j].ParameterType.Name)
                    {
                        Console.WriteLine(methods[i].Name);
                        fl = true;
                    }
                }
            }
            if (!fl)
            {
                Console.WriteLine("No methods.");
            }
        }
        //f.вызывает некоторый метод класса, при этом значения для его параметров 
        //необходимо прочитать из текстового файла.
        public void Runtimemethod(Test test, string method)
        {
            Console.WriteLine("\nMethods {0} with parameter from file:", method);
            FileStream fstream = new FileStream("param.txt", FileMode.Open);
            StreamReader sr = new StreamReader(fstream);
            object[] obj1 = { sr.ReadLine()};
            Type t = typeof(Test);
            MethodInfo m = t.GetMethod(method);
            object result = m.Invoke(test, obj1);   //вызываем метод экземпляра test с параметром obj1
            Console.WriteLine(result);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Reflector reflector = new Reflector("Lab_OOP_12.Test");
            reflector.AboutClass();
            reflector.PublicMethods();
            reflector.Fields();
            reflector.Interfaces();
            Test test = new Test();
            reflector.Methods(test);                
            reflector.Runtimemethod(test, "ToConsole");
        }
    }
}
