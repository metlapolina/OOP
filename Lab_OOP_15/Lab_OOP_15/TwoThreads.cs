using System;
using System.IO;
using System.Threading;

namespace Lab_OOP_15
{
    static class TwoThreads
    {
        public static void Consistently()                           //сначала чет, потом нечет
        {
            object locker = new object();                           //объект-заглушка
            if (File.Exists("ch_nech.txt"))
            {
                File.Delete("ch_nech.txt");
            }
            Thread CheT = new Thread(new ThreadStart(Chet));
            Thread NecheT = new Thread(new ThreadStart(Nechet));
            NecheT.Priority = ThreadPriority.AboveNormal;           //поменяли приоритет
            CheT.Start();
            NecheT.Start();

            void Chet()
            {
                lock (locker)                   //блокирует блок кода до завершения работы текущего потока
                {
                    for (int i = 0; i < 10; i++)
                    {
                        if (i % 2 == 0)
                        {
                            Console.WriteLine(i + " ch");
                            WriteResultToFile(i);
                            Thread.Sleep(300);
                            
                        }
                    }
                }
            }
            void Nechet()
            {
                lock (locker)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        if (i % 2 != 0)
                        {
                            Console.WriteLine(i + " n");
                            WriteResultToFile(i);
                            Thread.Sleep(500);

                        }
                    }
                }
            }
            void WriteResultToFile(int data)
            {
                StreamWriter sw = new StreamWriter("ch_nech.txt", true);
                sw.WriteLine(data);
                sw.Close();
            }
        }



        public static void OneByOne()                           //чередуясь
        {
            Mutex mutex = new Mutex();
            if (File.Exists("ch_n_ch.txt"))
            {
                File.Delete("ch_n_ch.txt");
            }
            Thread CheT = new Thread(new ThreadStart(Chet));
            Thread NecheT = new Thread(new ThreadStart(Nechet));
            CheT.Start();
            NecheT.Start();

            void Chet()
            {
                for (int i = 0; i < 10; i++)
                {
                    mutex.WaitOne();            //точка входа в критическую секцию для нескольких процессов    
                    if (i % 2 == 0)             //критич секция - участок прог-мы с общим ресурсом
                    {                                     //приостанавливает вып потока, пока не будет получен мьютекс
                        Console.WriteLine(i + " ch");
                        WriteResultToFile(i);
                        Thread.Sleep(300);
                    }
                    mutex.ReleaseMutex();       //выход, поток освобождает мьютекс
                }
            }
            void Nechet()
            {
                for (int i = 0; i < 10; i++)
                {
                    mutex.WaitOne();
                    if (i % 2 != 0)
                    {
                        Console.WriteLine(i + " n");
                        WriteResultToFile(i);
                        Thread.Sleep(500);
                    }
                    mutex.ReleaseMutex();
                }
            }
            void WriteResultToFile(int data)
            {
                StreamWriter sw = new StreamWriter("ch_n_ch.txt", true);
                sw.WriteLine(data);
                sw.Close();
            }
        }
    }
}
