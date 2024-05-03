using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarSystem
{
    public interface IFlyProgress<in T, out K>
    {
        void StartFly(T rocket);
        K MoveOn(int mass, string name, string status);
    }
}
