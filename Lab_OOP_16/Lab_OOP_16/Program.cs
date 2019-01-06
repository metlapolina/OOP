using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Lab_OOP_16
{
    public static class Tasker
    {
        //1.Используя TPL создайте длительную по времени задачу(на основе Task) на выбор:
        //умножение вектора размера 100000 на число
        public static void GetTask(int size, int num)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Vector vector = new Vector(size);
            Task task = Task.Factory.StartNew(() => vector = vector * num);
            //1) Выведите идентификатор текущей задачи, проверьте во время выполнения – завершена ли задача и выведите ее статус. 
            Console.WriteLine("ID: "+ task.Id +"\nStatus: " +task.Status);
            task.Wait();
            stopwatch.Stop();
            //2) Оцените производительность выполнения используя объект Stopwatch на нескольких прогонах.
            Console.WriteLine(stopwatch.Elapsed);
        }

        //2. Реализуйте второй вариант этой же задачи с токеном отмены CancellationToken и отмените задачу.
        public static void GetTaskC(int size, int num)
        {
            CancellationTokenSource cancellationToken = new CancellationTokenSource();
            Vector vector = new Vector(size);
            var task = new Task(() => vector = vector * num, cancellationToken.Token);
            var task1 = new Task(() => vector = vector * num, cancellationToken.Token);
            task.Start();
            task1.Start();
            cancellationToken.Cancel(); //отменяем задачи
            Task.WaitAll(task, task1);
            Console.WriteLine("Status: " + task.Status);
            Console.WriteLine("Status: " + task1.Status);
        }

        //3.Создайте три задачи с возвратом результата и используйте их для выполнения четвертой задачи. Например, расчет по формуле.
        public static async Task Sum(Vector first, Vector second, Vector third)
        {
            Task<int> task1 = new Task<int>(() => first.Sum());
            task1.Start();
            Task<int> task2 = new Task<int>(() => second.Sum());
            task2.Start();
            Task<int> task3 = new Task<int>(() => third.Sum());
            task3.Start();
            Task<Vector> task4 = new Task<Vector>(() => new Vector(task1.Result + task2.Result + task3.Result));
            //4.Создайте задачу продолжения(continuation task) в двух вариантах:
            //1) C ContinueWith -планировка на основе завершения множества предшествующих задач
            Task task5 = task4.ContinueWith(t=>Console.WriteLine("\n---continuation---"));
            task4.Start();
            task5.Wait();
            Console.WriteLine("Result of the sum: "+task4.Result.Sum());
        }
    }
    class Program
    {
        static Task<int> FactorialAsync(int x)
        {
            int res = 1;
            return Task.Run(() =>
            {
                for (int i = 1; i <= x; i++)
                {
                    res *= i;
                }
                return res;
            });
        }
        static async Task DisplayResultAsync()  //асинхронная функция
        {
            int res = await FactorialAsync(7);//await-нужен чтобы приостановить выполнение метода до тех пор,пока эта задача не завершится
            Thread.Sleep(3);
            Console.WriteLine("Factorial of num {0} = {1}", 7, res);
        }
        static void Main(string[] args)
        {
            Tasker.GetTask(100000000, 11);
            Tasker.GetTask(100000000, 75);
            Tasker.GetTask(100000000, 202);
            //Tasker.GetTaskC(100000000, 90);   //отмена задач

            //4.Создайте задачу продолжения(continuation task) в двух вариантах:
            //2) На основе объекта ожидания и методов GetAwaiter(),GetResult();
            Tasker.Sum(new Vector(100), new Vector(20), new Vector(30)).GetAwaiter().GetResult();
                                                        //получаем объект ожидания и результат вычислений

            //5.Используя Класс Parallel распараллельте вычисления циклов For(), ForEach().
            Paralleler.For();
            Paralleler.ForEach();
            //6.Используя Parallel.Invoke() распараллельте выполнение блока операторов.
            Paralleler.DoubleTask(100000);

            Console.WriteLine();
            //7.Используя Класс BlockingCollection реализуйте следующую задачу:
            //Есть 5 поставщиков бытовой техники, они завозят уникальные товары на склад(каждый по одному) и 10 покупателей – покупают все подряд,
            //если товара нет - уходят.В вашей задаче: cпрос превышает предложение.Изначально склад пустой.
            //У каждого поставщика своя скорость завоза товара.Каждый раз при изменении состоянии склада выводите наименования товаров на складе.
            Stock.Work();

            //8.Используя async и await организуйте асинхронное выполнение любого метода.
            Task t = DisplayResultAsync();
            t.Wait();
        }
    }
}
