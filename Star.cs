using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarSystem
{
    public class Star
    {
        private string _name;
        private string _spectralClass;
        private float _mass;
        private double _radius;
        private float _magnitude;
        private float _temperature;
        private double _luminosity;

        public Star() { }
        public Star(string name, string spectralClass, float mass, double radius, float magnitude, float temperature, double luminosity)
        {
            Name = name;
            SpectralClass = spectralClass;
            Mass = mass;
            Radius = radius;
            Magnitude = magnitude;
            Temperature = temperature;
            Luminosity = luminosity;
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

        public string SpectralClass
        { 
            get { return _spectralClass; }
            set
            {
                if (value == "O" || value == "B" || value == "A" || value == "F" ||
                    value == "G" || value == "K" || value == "M")
                {
                    _spectralClass = value;
                }
                else
                {
                    throw new ArgumentException("Введен неверный спектральный класс!");
                }
            } 
        }
        public float Mass
        { 
            get { return _mass; } 
            set 
            {
                if (value > 0) _mass = value;
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
                if (value > 0) _radius = value;
                else
                {
                    throw new ArgumentException("Введен неверный радиус!");
                }
            }
        }
        public float Magnitude
        {
            get { return _magnitude; }
            set
            {
                if (value >= -40 && value <= 50) _magnitude = value;
                else
                {
                    throw new ArgumentException("Введена неверная звездная величина!");
                }
            }
        }
        public float Temperature
        { 
            get { return _temperature; } 
            set
            {
                if (value >= 2500 && value <= 45000)
                _temperature = value;
                else
                {
                    throw new ArgumentException("Введена неверная температура");
                }
            }
        }

        public double Luminosity
        {
            get { return _luminosity; }
            set
            {
                if (value >= 0.001 && value <= 700000) _luminosity = value;
                else
                {
                    throw new ArgumentException("Введена неверная светимость");
                }
            }
        }
    }
}
