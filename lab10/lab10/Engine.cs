using lab10;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace lab10
{
    public class Engine : IComparable<Engine>, IComparable
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

        public int CompareTo(Engine other)
        {
            if (other == null) return 1;
            return this.HorsePower.CompareTo(other.HorsePower);
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            if (obj is Engine otherEngine)
            {
                return this.HorsePower.CompareTo(otherEngine.HorsePower);
            }
            else
            {
                throw new ArgumentException("Error: wrong type");
            }
        }
    }
}
