using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarSystem
{
    public abstract class Rocket
    {
        int _mass;
        string _name;
        string _status;

        public Rocket(int mass, string name, string status)
        {
            Mass = mass;
            Name = name;
            Status = status;
        }
        public int Mass
        {
            get { return _mass; }
            set { _mass = value; }
        }
        public string Name 
        {
            get { return _name; }
            set { _name = value; }
        }
        public string Status
        {
            get { return _status; }
            set { _status = value; }
        }
    }
}
