using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarSystem
{
    public delegate void newDelegate3(string name);
    public delegate void newDelegate4();
    public abstract class Planet: IEarthLike
    {
        public event newDelegate3 AddSat;
        public event newDelegate4 ClearSat;
        public enum PlanetType
        {
            None,
            Dwarf,
            Giant,
            EarthType
        }

        protected PlanetType _planetType;
        protected string _name;
        protected double _mass;
        protected float _radius;
        protected double _distance;
        protected double _eccentricity;
        protected float _albedo;
        protected double _period;
        protected List<Satelite> _satelites;

        protected Planet() { }

        protected Planet(PlanetType planetType, string name, double distance, float albedo, double period, List<Satelite> satelites)
        {
            PType = planetType;
            Name = name;
            Distance = distance;
            Albedo = albedo;
            Period = period;
        }

        public PlanetType PType
        { 
            get { return _planetType; }
            set { _planetType = value; }
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

        public float Albedo
        {
            get { return _albedo; }
            set
            {
                if (value >= 0 && value <= 1) _albedo = value;
                else
                {
                    throw new ArgumentException("Введено неверное альбедо!");
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

        public void AddSatelite(Satelite satelite)
        {
            if (satelite == null)
            {
                throw new ArgumentException(nameof(satelite));
            }
            _satelites.Add(satelite);
            AddSat(satelite.Name);
        }

        public void SetSatelites(List<Satelite> satelites)
        {
            _satelites = satelites;
        }
        public List<Satelite> GetSatelites()
        {
            return _satelites;
        }
        public void ClearSatelites()
        {
            _satelites.Clear();
            ClearSat();
        }


        public abstract double Eccentricity { get; set; }
        public abstract double Mass { get; set; }
        public abstract float Radius { get; set; }

        public abstract float GetEarthLikeCoefficient();
        public abstract string GetEarthLikeName();
    }
}
