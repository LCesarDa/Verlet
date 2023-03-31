using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Verlet
{
    public class VStick
    {
        public VPoint startPoint, endPoint;
        float stiffness, length, damp, tot, m1, m2, dist, diff;
        Vec2 d, offset;
        Color color;
        Canvas canva;
        public VStick(VPoint a, VPoint b, float l, Canvas c) 
        {
            startPoint= a;
            endPoint= b;
            stiffness= 0.9f;
            damp = 1f;
            tot = a.mass + b.mass;
            m1 = b.mass/tot;
            m2 = a.mass/tot;
            color = Color.Green;
            length= l;
            canva = c;
        }
        public VStick(VPoint a, VPoint b, Canvas c)
        {
            startPoint = a;
            endPoint = b;
            stiffness = 0.9f;
            damp = 1f;
            tot = a.mass + b.mass;
            m1 = b.mass / tot;
            m2 = a.mass / tot;
            color = Color.Green;
            length = a.pos.Distance(b.pos);
            canva = c;
        }
        public void Render() 
        {
            Update();
            canva.g.DrawLine(Pens.Green, new PointF(startPoint.pos.x, startPoint.pos.y), new PointF(endPoint.pos.x, endPoint.pos.y));
            
        }
        public void Update()
        {
            d = endPoint.pos - startPoint.pos;
            dist = d.Length();
            diff = stiffness * (length - dist) / dist;
            offset = d * (diff * damp);
            startPoint.pos -= offset * m1;
            endPoint.pos += offset * m2;
        }
    }
}
