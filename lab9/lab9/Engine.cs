using lab9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace lab9
{
    public class Engine
    {
        public double Displacement { get; set; }
        public double HorsePower { get; set; }

        [XmlAttribute("Model")]
        public string Model { get; set; }

        public Engine()
        {
            Displacement = 0.0;
            HorsePower = 0.0;
            Model = "";
        }

        public Engine(double displacement, double horsePower, string model)
        {
            Displacement = displacement;
            HorsePower = horsePower;
            Model = model;
        }
    }
}
