using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Verlet
{
    public  class VBody
    {
        public List<VPoint> points;
        public List<VStick> sticks;
        public VBody() 
        {
            points = new List<VPoint>();
            sticks = new List<VStick>();
        }
    }
}
