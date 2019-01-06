using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//1. Используя делегаты (множественные) и события промоделируйте ситуации. При реализации методов везде где возможно использовать лямбда-выражения.
//Создать класс Игра с событиями Атака и Лечить.
namespace Lab_OOP_9
{
    public class Game
    {
        public string name;
        public int health;
        public Game()
        {
            name = "NoName";
            health = 100;
        }
        public Game(string name, int health)
        {
            this.name = name;
            this.health = health;
        }
        public event EventHandler<ActArgs> Attack;      //события на основе стандартного делегата
        public event EventHandler<ActArgs> Treatment;
        public void ToAtt()    //метод, инициирующий событие
        {
            Console.WriteLine(this.name+", you have been attacked.");
            Attack?.Invoke(this, new ActArgs(this.health));//вызов события, если не null
        }
        public void ToTr()
        {
            Console.WriteLine(this.name + ", you have been treated.");
            Treatment?.Invoke(this, new ActArgs(this.health));
        }
    }
    public class ActArgs : EventArgs  //класс, содержащий инф. о событиях
    {
        int h;
        public ActArgs(int h)
        {
            this.h = h;
        }
        public void ToAttack()  //метод-обработчик события
        {
            int heal = h;
            h -= 10;
            if (h <= 0)
            {
                Console.WriteLine("You are dead (*o*)");
            }
            else
            {
                Console.WriteLine("Health level dropped from {0} to {1}!", heal, h);
            }
        }
        public void ToTreat()
        {
            h += 20;
            if (h > 100) { h = 100; }
            Console.WriteLine("Now the level of health is {0}.", h);
        }
    }
    public static class StringHandler
    {
        //2. Создайте пять методов пользовательской обработки строки (например, удаление знаков препинания, добавление символов, замена на заглавные,
        //удаление лишних пробелов и т.п.). Используя стандартные типы делегатов (Action, Func) организуйте алгоритм последовательной обработки строки
        //написанными вами методами.
        public static string RemoveS(string str, Func<string, string> test1){ return test1(str); }      //удаление знаков
        public static void AddToString(string str, Action<string> test2) => test2(str);                 //добавление строки
        public static string RemoveSpase(string str, Func<string,string> test3) { return test3(str); }  //удаление пробелов
        public static string Upper(string str, Func<string, string> test4) { return test4(str); }       //в верхний регистр
        public static string Lower(string str, Func<string, string> test5) { return test5(str); }       //в нижний регистр
    }

    //В main создать некоторое количество игровых объектов.Подпишите объекты на события произвольным образом.
    //Проверить состояния игровых объектов после наступления событий.
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("------------------Game------------------");
            Game user1 = new Game("Tonny",30);
            Game user2 = new Game("Jack", 20);
            Game user3 = new Game();
            Console.WriteLine("\nUser1");
            user1.Attack += User_Attack;    //подписываем объект на событие Атака
            user1.Attack += User_Attack;
            user1.Attack += User_Attack;
            user1.ToAtt();
            Console.WriteLine("\nUser2");
            user2.Treatment += User_Treatment;  //подписываем объект на событие Лечение
            user2.ToTr();
            Console.WriteLine("\nUser3");
            user3.Treatment += User_Treatment;
            user3.ToTr();
            user3.Attack += User_Attack;
            user3.ToAtt();

            Console.WriteLine("\n\n--------------Работа со строками--------------");
            string str = "P! o, I          ?       .t 4";
            Func<string,string> test1;  //встроенный делегат, второй параметр - возврат 
            Action<string> test2;       //не возвр значений
            Func<string,string> test3;
            Func<string,string> test4;
            Func<string, string> test5;
            test1 = str1 => {  //блочное лямбда-выражение(упрощенная запись анонимных методов) 
                char[] sign = { '.', ',', '!', '?', '-', ':' };
                for (int i = 0; i < str1.Length; i++)
                {
                    if (sign.Contains(str1[i]))
                    {
                        str1 = str1.Remove(i, 1);
                    }
                }
                Console.WriteLine(str1);
                return str1;
            };
            test2 = delegate(string str2)   //анонимный метод(безямынный блок кода)
            {
                str2+="+addsymbols";
                Console.WriteLine(str2);
            };
            test3 = str3 =>
              {
                  str3 = str3.Replace(" ", string.Empty);
                  Console.WriteLine(str3);
                  return str3;
              };
            test4 = str4 =>
              {
                  str4 = str4.ToUpper();
                  Console.WriteLine(str4);
                  return str4;
              };
            test5 = str5 =>
            {
                str5 = str5.ToLower();
                Console.WriteLine(str5);
                return str5;
            };
            Console.WriteLine("Строка до: "+str);
            Console.WriteLine("Строки после: ");
            string s1, s2, s3;
            s1 = StringHandler.RemoveS(str,test1);
            StringHandler.AddToString(s1,test2);
            s2 = StringHandler.RemoveSpase(s1, test3);
            s3 = StringHandler.Upper(s2, test4);
            StringHandler.Lower(s3, test5);

        }

        private static void User_Treatment(object sender, ActArgs e)
        {
            e.ToTreat();    //обработчик события
        }

        private static void User_Attack(object sender, ActArgs e)
        {
            e.ToAttack();   //обработчик события
        }
    }
}
