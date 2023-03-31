using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Verlet
{
    public class VRope: VBody
    {
        public VRope(VPoint a, VPoint b, Canvas canva) 
        { 
            float dist = a.pos.Distance(b.pos);
            dist /= 100;
            for (int i = 0; i < dist; i++)
            {
                points.Add(new VPoint(a.pos.x+(b.pos.x-a.pos.x)*i/dist, a.pos.y + (b.pos.y - a.pos.y) * i / dist, canva));
            }
            for (int i = 0;i < points.Count-1; i++)
            {
                sticks.Add(new VStick(points[i], points[i + 1], points[i].pos.Distance(points[i+1].pos), canva));
            }
            for (int i = 0; i < points.Count - 1; i++)
            {
                sticks.Add(new VStick(points[i], points[i + 1], points[i].pos.Distance(points[i + 1].pos), canva));
            }
            sticks.Add(new VStick(points[points.Count - 1], b, points[points.Count - 1].pos.Distance(b.pos), canva));
        }
        
        public void Render(Canvas canva)
        {
            points[0].FixedRender();
            for (int i = 1; i < points.Count; i++)
            {
                points[i].WRender();
            }
            for (int i = 0; i < sticks.Count - 1; i++)
            {
                sticks[i].Update();
                canva.DrawLine(new System.Drawing.Point((int)sticks[i].startPoint.pos.x, (int)sticks[i].startPoint.pos.y), new System.Drawing.Point((int)sticks[i].endPoint.pos.x, (int)sticks[i].endPoint.pos.y), System.Drawing.Color.White);
            }
            
        }
    }
}
