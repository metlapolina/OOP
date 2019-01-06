using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization.Formatters.Soap;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Xml;

namespace Lab_OOP_14
{
    [Serializable]  //объект доступен для служб сериализации
    [DataContract]  //контракт данных - тип, объект которого описывает информационный фрагмент
    public abstract class Author
    {                               
        protected string firstName;
        protected string lastName;
        [DataMember]
        public string FirstName
        {
            get => firstName;
            set => firstName = value;
        }
        [DataMember]
        public string LastName
        {
            get => lastName;
            set => lastName = value;
        }
        public override string ToString()   
        {
            return "Type " + base.ToString() + ". The author - " + FirstName + " " + LastName + ".";
        }
        virtual public void ToPrint() { }  
        public abstract void Same();       
    }

    [Serializable]
    [DataContract]
    public class Book : Author
    {
        protected string nameOfW;

        public Book()
        {
            this.firstName = "Unknown";
            this.lastName = "Non";
            this.nameOfW = "Book";
        }
        public Book(string name, string sname, string nameOfW)
        {
            this.firstName = name;
            this.lastName = sname;
            this.nameOfW = nameOfW;
        }
        [DataMember]
        public string NameOfW
        {
            get => nameOfW;
            set => nameOfW = value;
        }
        public override string ToString()
        {                               
            return "" + base.ToString() + " The name of Book is " + NameOfW + ".";
        }
        override public void ToPrint()
        {
            Console.WriteLine(this.ToString());
        }
        public override void Same()
        {                             
            Console.WriteLine("I am the same method(abstract)!");
        }
        public void Info()
        {
            Console.WriteLine("The author of " + NameOfW + " is " + FirstName + " " + LastName + ".");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //1.Из лабораторной №5 выберите класс с наследованием и/или композицией/агрегацией для сериализации. 
            //Выполните сериализацию/десериализацию объекта используя:

            //a.бинарный формат
            //сериализация: объект -> поток байт
            //десериализация: поток байт -> сохраненный объект
            Book book = new Book("Jack","London","White Fang"); //объект для сериализации
            Console.WriteLine("Binary:");
            BinaryFormatter formatter = new BinaryFormatter();  //создаем объект BinaryFormatter, который сериализует, используя двоичный формат
            using (FileStream fs = new FileStream("Bin.dat", FileMode.OpenOrCreate))    //получем поток, куда будем записывать сериализованный объект
            {           
                formatter.Serialize(fs,book);
            }
            using (FileStream fs = new FileStream("Bin.dat", FileMode.OpenOrCreate))
            {
                Book newBook = (Book)formatter.Deserialize(fs);
                newBook.Info();
            }

            //b.SOAP формат
            Book book1 = new Book("Stephen", "King", "The Green Mile");
            Console.WriteLine("\nSOAP:");
            SoapFormatter soapformatter = new SoapFormatter();  //сохраняет состояние объекта в виде сообщения SOAP
                                                                //(стандартный XML-формат для передачи и приема сообщений от веб - служб)
            using (Stream fs = new FileStream("SOAP.dat", FileMode.OpenOrCreate))
            {
                soapformatter.Serialize(fs, book1);
            }
            using (Stream fs = new FileStream("SOAP.dat", FileMode.OpenOrCreate))
            {
                Book newBook1 = (Book)soapformatter.Deserialize(fs);
                newBook1.Info();
            }

            //c.JSON формат
            Book book2 = new Book("Charles", "Bukowski", "Post Office");
            Console.WriteLine("\nJSON:");
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(Book));
            using (FileStream fs = new FileStream("JSONForm.json", FileMode.OpenOrCreate))
            {
                jsonFormatter.WriteObject(fs, book2);
            }
            using (FileStream fs = new FileStream("JSONForm.json", FileMode.OpenOrCreate))
            {
                Book newB = jsonFormatter.ReadObject(fs) as Book;
                newB.Info();
            }

            //d.XML формат
            Book book3 = new Book("Ray", "Bradbury", "Fahrenheit 451");
            Console.WriteLine("\nXML:");
            XmlSerializer xml = new XmlSerializer(typeof(Book));    //ограничения: конструктор без параметров, указание типа
            using (FileStream fs = new FileStream("XMLSerial.xml", FileMode.OpenOrCreate))
            {
                xml.Serialize(fs, book3);
            }
            using (FileStream fs = new FileStream("XMLSerial.xml", FileMode.OpenOrCreate))
            {
                Book newBook3 = xml.Deserialize(fs) as Book;
                newBook3.Info();
            }

            //2.Создайте коллекцию(массив) объектов и выполните сериализацию / десериализацию.
            Book b1 = new Book("Joanne", "Rowling", "Harry Potter and the Philosopher's Stone");
            Book b2 = new Book("Joanne", "Rowling", "Harry Potter and the Prisoner of Azkaban");
            Book b3 = new Book("Joanne", "Rowling", "Harry Potter and the Order of the Phoenix");
            Book[] bs = new Book[] { b1, b2, b3 };
            Console.WriteLine("\nArray:");
            DataContractSerializer array = new DataContractSerializer(typeof(Book[]));
            using (FileStream fs = new FileStream("array.xml", FileMode.OpenOrCreate))
            {
                array.WriteObject(fs, bs);
            }
            using (FileStream fs = new FileStream("array.xml", FileMode.OpenOrCreate))
            {
                Book[] newB = (Book[])array.ReadObject(fs);
                foreach(Book b in newB)
                {
                    b.Info();
                }
            }

            //3.Используя XPath напишите два селектора для вашего XML документа.
            Console.WriteLine("\nXPath:");  //язык запросов к элементам XML-документа
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("books.xml");
            XmlElement xRoot = xDoc.DocumentElement;    //св-во, возвр. корень док-та
            Console.WriteLine("All nodes:");
            XmlNodeList all = xRoot.SelectNodes("*");   //выбирает все узлы
            foreach (XmlNode x in all)
            {
                Console.WriteLine(x.OuterXml);         //вывод всей разметки
            }
            Console.WriteLine("Several parts of the book:");
            XmlNodeList parts = xRoot.SelectNodes("Book");  //выбирает узлы Book
            foreach(XmlNode x in parts)
            {
                Console.WriteLine(x.SelectSingleNode("NameOfW").InnerText); //вывод значения первого узла NameOfW
            }

            //4.Используя Linq to XML(или Linq to JSON) создайте новый xml(json) - документ и напишите несколько запросов.
            Console.WriteLine("\nLINQ to XML:");
            XDocument xdoc = new XDocument();
            XElement bookstore = new XElement("bookstore"); //первый эл
            XAttribute bs_name_attr = new XAttribute("name","Barter Books");
            XElement bs_country_elem = new XElement("country","GB");
            XElement bs_city_elem = new XElement("city", "Alnwick");
            bookstore.Add(bs_name_attr);            //заполняем аттрибутом и элем-ми
            bookstore.Add(bs_country_elem);
            bookstore.Add(bs_city_elem);

            XElement bookstore2 = new XElement("bookstore");    //второй эл
            XAttribute bs2_name_attr = new XAttribute("name", "The Abbey Bookshop");
            XElement bs2_country_elem = new XElement("country", "France");
            XElement bs2_city_elem = new XElement("city", "Paris");
            bookstore2.Add(bs2_name_attr);          //заполняем аттрибутом и элем-ми
            bookstore2.Add(bs2_country_elem);
            bookstore2.Add(bs2_city_elem);

            XElement root = new XElement("root");   //корневой элемент
            root.Add(bookstore);
            root.Add(bookstore2);
            xdoc.Add(root);
            xdoc.Save("linq.xml");                  //сохраняем в файл

            Console.WriteLine("Request 1: What is a bookstore in GB?"); //1-й запрос
            var items = xdoc.Element("root").Elements("bookstore")
                .Where(p => p.Element("country").Value == "GB")
                .Select(p=>p);
            foreach(var item in items)
            {
                Console.WriteLine(item.Attribute("name").Value+" - "+item.Element("country").Value+" - "+ item.Element("city").Value);
            }
            Console.WriteLine("Request 2: In which country is there a bookstore called 'The Abbey Bookshop'?");//2-й запрос
            var coun = xdoc.Element("root").Elements("bookstore")
                .Where(p => p.Attribute("name").Value == "The Abbey Bookshop")
                .Select(p => p);
            foreach (var c in coun)
            {
                Console.WriteLine(c.Attribute("name").Value + " - " + c.Element("country").Value + " - " + c.Element("city").Value);
            }
        }
    }
}
