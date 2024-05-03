using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarSystem
{
    public class FlyProgress : IFlyProgress<Rocket, SpaceShip>
    {
        public void StartFly(Rocket rocket)
        {
            Console.WriteLine($"Ракета {rocket.Name} Масса: {rocket.Mass}, Текущий статус: {rocket.Status}");
        }
        public SpaceShip MoveOn(int mass, string name, string status)
        {
            return new SpaceShip(mass, name, status);
        }
    }
}
