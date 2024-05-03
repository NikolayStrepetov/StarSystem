using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarSystem
{
    internal class EarthType: Planet
    {
        public EarthType() { }

        public EarthType(PlanetType planetType, string name, double mass, float radius, double distance, double eccentricity, float albedo, double period, List<Satelite> satelites)
            :base(planetType, name, distance, albedo, period, satelites)
        {
            Mass = mass;
            Radius = radius;
            Eccentricity = eccentricity;
        }

        public override double Mass
        {
            get { return _mass; }
            set
            {
                if (value >= 0.1 && value <= 10) _mass = value;
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
                if (Math.Round(value, 1) >= 0.7 && Math.Round(value, 1) <= 2) _radius = value;
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
                if (value >= 0.005 && value <= 0.09) _eccentricity = value;
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
                if (_radius <= 1) return (float)(1 / _mass * _radius);
                else return (float)(1 / _mass * (1 / _radius));
            }
            else
            {
                if (_radius <= 1) return (float)(_mass * _radius);
                else return (float)(_mass * (1 / _radius));
            }
        }
        public override string GetEarthLikeName()
        {
            return _name;
        }
    }
}
