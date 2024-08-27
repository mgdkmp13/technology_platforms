using lab10;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace lab10
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

        public override string ToString()
        {
            return $"Model:\t\t{this.Model}\nYear:\t\t{this.Year}\nEngine:\t\t{this.Motor.Model}\nDisplacement:\t{this.Motor.Displacement}\nHorsePower:\t{this.Motor.HorsePower}\n";
        }
    }
}
