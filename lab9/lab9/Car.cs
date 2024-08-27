using lab9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace lab9
{
    [XmlType(TypeName = "car")]
    public class Car
    {
        public string Model { get; set; }
        public int Year { get; set; }

        [XmlElement(ElementName = "Engine")]
        public Engine Motor { get; set; }

        public Car()
        {
            Model = "";
            Year = 0;
            Motor = null;
        }

        public Car(string model, Engine engine, int year)
        {
            Model = model;
            Motor = engine;
            Year = year;
        }
    }
}
