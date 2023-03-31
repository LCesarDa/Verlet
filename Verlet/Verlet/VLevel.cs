using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Verlet
{
    public class VLevel
    {
        public VPoint player;
        public List<VPoint> anchor, ball;
        public List<VPoint> col = new List<VPoint>();
        public List<VBox> obs;
        public List<VRope> rope;
        public VSolver solve;
        public Boolean grab = false;

        public VLevel(Canvas canvas, int d, Random rand)
        {
            anchor = new List<VPoint>();
            ball = new List<VPoint>();
            obs = new List<VBox>();
            rope = new List<VRope>();
            player = new VPoint(120, 100, canvas);
            if(d < 4)
            {
                for (int i = 0; i < d; i++)
                {
                    obs.Add(new VBox(new PointF(rand.Next(0, 100), rand.Next(300, (int)canvas.Height)), rand.Next(200, 280), rand.Next(290, 350), canvas));
                }
                d = 0;
            } else 
            {
                for (int i = 0; i < 4; i++)
                {
                    obs.Add(new VBox(new PointF(0, canvas.Height), rand.Next(230, 250), rand.Next(290, 310), canvas));
                }
                d -= 4;
            }
            if (d > 0)
            {
                for (int i = 0; i < d; i++)
                {
                    ball.Add(new VPoint(i * rand.Next(200, 300), rand.Next(100, 250), i* 10, canvas));
                }
            }
            for (int i = 1; i < 7; i++)
            {
                anchor.Add(new VPoint(i * rand.Next(200, 300), rand.Next(100, 250), canvas));
            }
            for (int i = 0; i < obs.Count; i++)
            {
                col.AddRange(obs[i].points);
            }
            col.AddRange(ball);
            col.Add(player);
            solve = new VSolver(col);
        }

        public int near()
        {
            int a = 0;
            float f, m = 10000;
            for (int i = 0; i < anchor.Count; i++)
            {
                f = player.pos.Distance(anchor[i].pos);
                if (f < m)
                {
                    m = f;
                    a = i;
                }
            }
            return a;
        }
    }
}
