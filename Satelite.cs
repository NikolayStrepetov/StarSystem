using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarSystem
{
    public class Satelite: IEarthLike
    {
        private string _name;
        private double _mass;
        private double _radius;
        private double _distance;
        private double _period;

        public Satelite() { }

        public Satelite(string name, double mass, double radius, double distance, double period)
        {
            Name = name;
            Mass = mass;
            Radius = radius;
            Distance = distance;
            Period = period;
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (value != null && value != " " && value != "\n") _name = value;
                else
                {
                    throw new ArgumentException("Введено неверное название!");
                }
            }
        }

        public double Mass
        {
            get { return _mass; }
            set
            {
                if (value > 0 ) _mass = value;
                else
                {
                    throw new ArgumentException("Введена неверная масса!");
                }
            }
        }

        public double Radius
        {
            get { return _radius; }
            set
            {
                if (value >= 400 && value <= 3000) _radius = value;
                else
                {
                    throw new ArgumentException("Введен неверный радиус!");
                }
            }
        }

        public double Distance
        {
            get { return _distance; }
            set 
            {
                if (value > 0) _distance = value;
                else
                {
                    throw new ArgumentException("Введено неверное расстояние!");
                }
            }
        }

        public double Period
        {
            get { return _period; }
            set
            {
                if (value >= 0) _period = value;
                else
                {
                    throw new ArgumentException("Введен неверный орбитальный период!");
                }
            }
        }

        public float GetEarthLikeCoefficient()
        {
            if (_mass >= 1)
            {
                return (float)(1 / _mass * (_radius / 6371));
            }
            else
            {
                return (float)(_mass * (_radius / 6371));
            }
        }

        public string GetEarthLikeName()
        {
            return _name;
        }
    }
}
