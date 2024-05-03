using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarSystem
{
    public delegate void newDelegate(string name);
    public delegate void newDelegate2();
    public class StarSystem
    {
        public event newDelegate AddSt;
        public event newDelegate AddPl;
        public event newDelegate2 ClearPl;

        private Star _star;
        private List<Planet> _planets = new List<Planet>();

        public StarSystem() { }

        public StarSystem(Star star, List<Planet> planets)
        {
            _star = star;
            _planets = planets;
        }
        internal void AddStar(Star star)
        {
            if (star != null)
            {
                _star = star;
            }
            AddSt(star.Name);
        }

        public void AddPlanet(Planet planet)
        {
            if (planet != null)
            {
                _planets.Add(planet);
            }
            AddPl(planet.Name);
        }


        public Star GetStar() { return _star; }
        public List<Planet> GetPlanets() { return _planets; }
        public void ClearPlanets()
        {
            _planets.Clear();
            ClearPl();
        }
    }
}
