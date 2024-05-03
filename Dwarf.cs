using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarSystem
{
    internal class Dwarf: Planet
    {
        public Dwarf() { }

        public Dwarf(PlanetType planetType, string name, double mass, float radius, double distance, double eccentricity, float albedo, double period, List<Satelite> satelites)
            : base(planetType, name, distance, albedo, period, satelites)
        {
            Mass = mass;
            Radius = radius;
            Eccentricity = eccentricity;
        }

        Random rnd = new Random();
        public override double Mass
        {
            get { return _mass; }
            set
            {
                if (value >= 0.0001 && value <= 0.001) _mass = value;
                else
                {
                    throw new ArgumentException("Введена неверная масса!");
                }
            }
        }

        public override float Radius
        {
            get { return _radius; }
            set
            {
                if (value >= 800 && value <= 3000) _radius = value;
                else
                {
                    throw new ArgumentException("Введен неверный радиус!");
                }
            }
        }
        public override double Eccentricity
        {
            get { return _eccentricity; }
            set
            {
                if (value >= 0.1 && value <= 0.4) _eccentricity = value;
                else
                {
                    throw new ArgumentException("Введен неверный эксцентриситет!");
                }
            }
        }

        public override float GetEarthLikeCoefficient()
        {
            if (_mass >= 1)
            {
                return (float)(1 / _mass * _radius / 6371);
            }
            else
            {
                return (float)(_mass * _radius / 6371);
            }
        }
        public override string GetEarthLikeName()
        {
            return _name;
        }
    }
}
