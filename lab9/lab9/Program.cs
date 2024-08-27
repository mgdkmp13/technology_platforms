using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Xml.XPath;

namespace lab9
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Car> myCars = new List<Car>()
            {
                new Car("E250", new Engine(1.8, 204, "CGI"), 2009),
                new Car("E350", new Engine(3.5, 292, "CGI"), 2009),
                new Car("A6", new Engine(2.5, 187, "FSI"), 2012),
                new Car("A6", new Engine(2.8, 220, "FSI"), 2012),
                new Car("A6", new Engine(3.0, 295, "TFSI"), 2012),
                new Car("A6", new Engine(2.0, 175, "TDI"), 2011),
                new Car("A6", new Engine(3.0, 309, "TDI"), 2011),
                new Car("S6", new Engine(4.0, 414, "TFSI"), 2012),
                new Car("S8", new Engine(4.0, 513, "TFSI"), 2012)
            };

            zad1(myCars);
            

            //zad2
            serializationXml(myCars, "cars.xml");

            zad3("cars.xml");

            //zad4
            createXmlFromLinq(myCars);

           
            zad5(myCars);

            zad6("cars.xml");

            List<Car> carsDeserialized = deserializationXml("cars.xml");

            Console.WriteLine("\nDeserializacja samochodów:");
            foreach (var car in carsDeserialized)
            {
                Console.WriteLine($"Model: {car.Model} \tYear: {car.Year} \tEngine Model: {car.Motor.Model} \tDisplacement: {car.Motor.Displacement} \tHorsepower: {car.Motor.HorsePower}");
            }
        }

        static void zad1(List<Car> myCars)
        {
            // query 1
            Console.WriteLine("Zad1_1");
            var query1_1 = from car in myCars
                           where car.Model == "A6"
                           select new
                           {
                               engineType = car.Motor.Model == "TDI" ? "diesel" : "petrol",
                               hppl = car.Motor.HorsePower / car.Motor.Displacement
                           }
                           into obj
                           select obj;


            foreach (var item in query1_1)
            {
                Console.WriteLine("Engine type: {0}\t HPPL: {1}", item.engineType, item.hppl);
            }

            //query 2
            Console.WriteLine("\nZad1_2");
            var query1_2 = from car in query1_1
                           group car.hppl by car.engineType;

            foreach (var item in query1_2)
            {
                double sum = 0.0;
                int counter = 0;
                foreach (var value in item)
                {
                    sum += value;
                    counter++;
                }
                var avg = sum / counter;
                Console.WriteLine("{0}: {1}", item.Key, avg);
            }

        }

        static void serializationXml(List<Car> collection1, string fileName)
        {
            var serializer = new XmlSerializer(collection1.GetType(), new XmlRootAttribute("cars"));

            using (TextWriter textWriter = new StreamWriter(fileName))
            {
                serializer.Serialize(textWriter, collection1);
            }
        }

        static List<Car> deserializationXml(string fileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Car>), new XmlRootAttribute("cars"));

            List<Car> cars;

            FileStream fileStream = new FileStream(fileName, FileMode.Open);
            cars = (List<Car>)serializer.Deserialize(fileStream);

            return cars;
        }

        static void zad3(string path)
        {
            Console.WriteLine("\nZad3_1");
            XElement rootNode = XElement.Load(path);
            double avgHP = (double)rootNode.XPathEvaluate("sum(car/Engine[@Model != 'TDI']/HorsePower) div count(car/Engine[@Model != 'TDI'])");
            Console.WriteLine("Średnia moc silników innych niż TDI: {0}", avgHP);


            Console.WriteLine("\nZad3_2");
            IEnumerable<XElement> models = rootNode.XPathSelectElements("//car/Model[not(. = following::car/Model)]");
            Console.WriteLine("Modele samochodów:");
            foreach (var m in models)
            {
                Console.WriteLine("- {0}", m.Value);
            }
        }

        private static void createXmlFromLinq(List<Car> myCars)
        {
            IEnumerable<XElement> nodes = from car in myCars
                                          select new XElement("car",
                                            new XElement("Model", car.Model),
                                            new XElement("Year", car.Year),
                                            new XElement("Engine",
                                                new XAttribute("Model", car.Motor.Model),
                                                new XElement("Displacement", car.Motor.Displacement),
                                                new XElement("HorsePower", car.Motor.HorsePower)
            )); // zapytanie LINQ
            XElement rootNode = new XElement("cars", nodes); // stwórz węzeł zawierający wyniki zapytania
            rootNode.Save("CarsFromLinq.xml");
        }

        static void zad5(List<Car> myCars)
        {
            XElement xhtmlTable = new XElement("table",
                new XElement("tr",
                    new XElement("th", "Model"),
                    new XElement("th", "Year"),
                    new XElement("th", "Engine Displacement"),
                    new XElement("th", "Engine HorsePower"),
                    new XElement("th", "Engine Model")
                ),
                new XAttribute("border", "solid 2px"),
                from car in myCars
                select new XElement("tr",
                    new XElement("td", car.Model),
                    new XElement("td", car.Year),
                    new XElement("td", car.Motor.Displacement),
                    new XElement("td", car.Motor.HorsePower),
                    new XElement("td", car.Motor.Model)
                )
            ); ;

            xhtmlTable.Save("template.html");
        }

        static void zad6(string fileName)
        {
            XDocument xmlFile = XDocument.Load(fileName);

            var xml6_1 = from cars in xmlFile.Element("cars").Elements("car").Elements("Engine").Elements("HorsePower")
                         select cars;

            foreach (XElement horsePower in xml6_1)
            {
                horsePower.Name = "HP";
            }

            var xml6_2 = from cars in xmlFile.Element("cars").Elements("car")
                         select cars;

            foreach (XElement car in xml6_2)
            {
                string year = car.Element("Year").Value;
                car.Element("Model").Add(new XAttribute("Year", year));
                car.Element("Year").Remove();
            }

            xmlFile.Save("cars6.xml");
        }
    }
}