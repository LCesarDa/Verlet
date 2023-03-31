using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Verlet
{
    public class VSolver
    {
        VPoint p1, p2;
        Vec2 axis, n;
        float dist, delta1, delta2; 
        List<VPoint> pts;
        public VSolver(List<VPoint> pts)
        {
            this.pts = pts;
        }
        public void Step(Canvas canva)
        {
            CheckCollisions(canva.g, (int)canva.Width, (int)canva.Height);
        }
        private void CheckCollisions(Graphics g, int Width, int Height)
        {
            for (int s = 0; s < pts.Count; s++)
            {
                for (int p = s + 1; p < pts.Count; p++)
                {
                    p1 = pts[s];
                    p2 = pts[p];
                    axis = p1.pos - p2.pos;
                    dist = axis.Length();
                    if (dist < (p1.radius + p2.radius)) //COLLISION DETECTED
                    {
                        n = axis / dist;
                        delta1 = p1.radius - dist;
                        delta2 = p2.radius - dist;
                        p1.pos -= n * 0.5f * delta1;
                        p2.pos += n * 0.5f * delta2;
                    }
                }
            }
        }

    }
}
