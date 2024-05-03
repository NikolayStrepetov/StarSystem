using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarSystem
{
    public class SpaceShip: Rocket
    {
        public SpaceShip(int mass, string name, string status): base(mass, name, status){ }
    }
}
