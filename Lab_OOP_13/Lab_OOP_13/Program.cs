using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;

namespace Lab_OOP_13
{
    //2. Создать класс XXXDiskInfo c методами для вывода информации о
    //a.свободном месте на диске
    //b.Файловой системе
    //c.Для каждого существующего диска - имя, объем, доступный объем, метка тома.
    //d.Продемонстрируйте работу класса
    public class MPGDiskInfo
    {
        public void DiskInfo()
        {
            Console.WriteLine("DiskInfo:");
            DriveInfo[] drives = DriveInfo.GetDrives(); //получение массива дисков
            foreach(DriveInfo drive in drives)
            {
                Console.WriteLine("\tName: "+drive.Name);
                Console.WriteLine("\tType: " + drive.DriveType);
                if (drive.IsReady)
                {
                    Console.WriteLine("\tFileSystem: " + drive.DriveFormat);
                    Console.WriteLine("\tFreeSpace: total - {0} GB, available - {1} GB", drive.TotalFreeSpace/1024/1024/1024, drive.AvailableFreeSpace/1024/1024/1024);
                    Console.WriteLine("\tTotalSize: {0} GB", drive.TotalSize/1024/1024/1024);
                    Console.WriteLine("\tVolumeLabel: " + drive.VolumeLabel);
                }
                Console.WriteLine();
            }
        }
    }

    //3. Создать класс XXXFileInfo c методами для вывода информации о конкретном файле
    //a.Полный путь
    //b.Размер, расширение, имя
    //c.Время создания
    //d. Продемонстрируйте работу класса
    public class MPGFileInfo
    {
        public void FileData(string path)
        {
            Console.WriteLine("FileInfo:");
            FileInfo fileInf = new FileInfo(path);
            if (fileInf.Exists)
            {
                Console.WriteLine("\tAll way : {0}", fileInf.DirectoryName);
                Console.WriteLine("\tName: {0}", fileInf.Name);
                Console.WriteLine("\tCapacity: {0}\n\tExtension: {1}\n\tCreationTime: {2}", fileInf.Length, fileInf.Extension, fileInf.CreationTime);
            }
            else
            {
                Console.WriteLine("This file doesn't exists");
            }
        }
    }

    //4. Создать класс XXXDirInfo c методами для вывода информации о конкретном директории
    //a.Количестве файлов
    //b.Время создания
    //c.Количестве поддиректориев
    //d.Список родительских директориев
    //e. Продемонстрируйте работу класса
    public class MPGDirInfo
    {
        public void DirInfo(string dirName)
        {
            Console.WriteLine("\nDirInfo:");
            if (Directory.Exists(dirName))
            {
                Console.WriteLine("Files:");
                string[] files = Directory.GetFiles(dirName);
                int countF = 0;
                foreach (string s in files)
                {
                    countF++;
                }
                Console.WriteLine("\tCountOfFiles: {0}", countF);
                DirectoryInfo dirInfo = new DirectoryInfo(dirName);
                Console.WriteLine("\tCreationTime: {0}", dirInfo.CreationTime);

                Console.WriteLine("SubDir:");
                string[] dirs = Directory.GetDirectories(dirName);
                int countSD = 0;
                foreach (string s in dirs)
                {
                    countSD++;
                }
                Console.WriteLine("\tCountOfSubDirectories: {0}", countSD);

                Console.WriteLine("\tParents: {0}", dirInfo.Parent);
            }
            else
            {
                Console.WriteLine("This directory doesn't exists");
            }
        }
    }

    //5. Создать класс XXXFileManager.Набор методов определите самостоятельно.С его помощью выполнить следующие действия:
    //a.Прочитать список файлов и папок заданного диска. Создать директорий XXXInspect, создать текстовый файл
    //xxxdirinfo.txt и сохранить туда информацию. Создать копию файла и переименовать его. 
    //Удалить первоначальный файл.
    public class MPGFileManager
    {
        public void FileManager(string path)
        {
            Console.WriteLine("\nFileManager:");
            string filepath = path + "\\OOP\\" + "MPGInspect";
            Directory.CreateDirectory(filepath);

            string filename = filepath + "\\" + "mpgdirinfo.txt";
            FileInfo file = new FileInfo(filename);

            using (FileStream fstream = new FileStream(filename, FileMode.Create))
            {
                StreamWriter sw = new StreamWriter(fstream);
                if (Directory.Exists(path))
                {
                    sw.WriteLine("Files:");
                    string[] files = Directory.GetFiles(path);
                    foreach (string s in files)
                    {
                        sw.WriteLine(s);
                    }

                    sw.WriteLine("SubDir:");
                    string[] dirs = Directory.GetDirectories(path);
                    foreach (string s in dirs)
                    {
                        sw.WriteLine(s);
                    }
                }
                else
                {
                    sw.WriteLine("This directory doesn't exists");
                }
                sw.Close();
            }
            Console.WriteLine("Text written to file.");

            file.CopyTo(filepath + "\\" + "newMpgDirInfo.txt", true);
            file.Delete();

            //b.Создать еще один директорий XXXFiles.Скопировать в него все файлы с заданным расширением из заданного
            //пользователем директория. Переместить XXXFiles в XXXInspect.
            string filecopydir = path + "\\OOP\\" + "MPGFiles";
            DirectoryInfo directory = new DirectoryInfo(filecopydir);
            directory.Create();

            DirectoryInfo d = new DirectoryInfo(path);
            FileInfo[] files2 = d.GetFiles("*.jpg");
            foreach (FileInfo s in files2)
            {
                s.CopyTo(filecopydir + "\\"+s.Name, true);
            }
            directory.MoveTo("D:\\OOP\\MPGInspect\\MPGFiles");
            Console.WriteLine("MPGFiles moved to MPGInspect.");

            //c.Сделайте архив из файлов директория XXXFiles.
            //Разархивируйте его в другой директорий.
            ZipFile.CreateFromDirectory("D:\\OOP\\MPGInspect\\MPGFiles", "D:\\OOP\\arch.zip");
            ZipFile.ExtractToDirectory("D:\\OOP\\arch.zip", "D:\\OOP\\Archive\\");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                MPGLog();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
        }
        //1. Создать класс XXXLog. Он должен отвечать за работу с текстовым файлом xxxlogfile.txt,
        //в который записываются все действия пользователя и соответственно методами записи в текстовый файл, чтения, поиска нужной информации.
        //a.Используя данный класс выполните запись всех последующих действиях пользователя с указанием действия,
        //детальной информации(имя файла, путь) и времени (дата/время)
        public static void MPGLog()
        {
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = "D:\\OOP\\";
            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
               | NotifyFilters.FileName | NotifyFilters.DirectoryName;      //тип отслеживаемых изменений
            watcher.Filter = "*.*";         //фильтр слежения - за всеми файлами
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.Created += new FileSystemEventHandler(OnChanged);
            watcher.Deleted += new FileSystemEventHandler(OnChanged);
            watcher.Renamed += new RenamedEventHandler(OnRenamed);
            watcher.EnableRaisingEvents = true;

            MPGDiskInfo diskInfo = new MPGDiskInfo();
            diskInfo.DiskInfo();

            MPGFileInfo fileInfo = new MPGFileInfo();
            fileInfo.FileData("D:\\Лаба8\\Debug\\Лаба8.exe");

            MPGDirInfo dirInfo = new MPGDirInfo();
            dirInfo.DirInfo("D:\\KSiS\\ConsoleApp1");

            MPGFileManager fileManager = new MPGFileManager();
            fileManager.FileManager("D:");
        }
        
        private static void OnChanged(object source, FileSystemEventArgs e)
        {
            StreamWriter writer = new System.IO.StreamWriter("D:\\OOP\\mpglogfile.txt", true);
            writer.WriteLine("File:" + e.FullPath + " - " + e.ChangeType.ToString() + " - " + DateTime.Now.ToString());
            writer.Close();
        }

        private static void OnRenamed(object source, RenamedEventArgs e)
        {
            StreamWriter writer = new System.IO.StreamWriter("D:\\OOP\\mpglogfile.txt", true);
            writer.WriteLine("File: "+e.OldFullPath+" renamed to "+e.FullPath + " - " + DateTime.Now.ToString());
            writer.Close();
        }
    }
}
