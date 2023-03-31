using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Verlet
{
    public class VBox: VBody
    {

        float w, h;
        int ra;
        List<Point> d = new List<Point>();
        public VBox(Canvas canvas, Random rand)
        {

            w = rand.Next(11, 500);
            h = rand.Next(10, 500);
            PointF pos = new PointF(rand.Next((int)w, (int)w + 500), rand.Next((int)h, (int)h + 100));
            points.Add(new VPoint(pos.X - w / 2, pos.Y - h / 2, canvas));
            points.Add(new VPoint(pos.X + w / 2, pos.Y - h / 2, canvas));
            points.Add(new VPoint(pos.X - w / 2, pos.Y + h / 2, canvas));
            points.Add(new VPoint(pos.X + w / 2, pos.Y + h / 2, canvas));
            sticks.Add(new VStick(points[0], points[1], points[0].pos.Distance(points[1].pos), canvas));
            sticks.Add(new VStick(points[0], points[2], points[0].pos.Distance(points[2].pos), canvas));
            sticks.Add(new VStick(points[1], points[2], points[1].pos.Distance(points[2].pos), canvas));
            sticks.Add(new VStick(points[1], points[3], points[1].pos.Distance(points[3].pos), canvas));
            sticks.Add(new VStick(points[2], points[3], points[2].pos.Distance(points[3].pos), canvas));
            sticks.Add(new VStick(points[3], points[0], points[3].pos.Distance(points[0].pos), canvas));
        }
        public VBox(PointF pos, float w, float h, Canvas canvas) 
        {
            float r;
            if (w > h)
            {
                r = w / 4;
            } else
            {
                r = h / 4;
            }
            points.Add(new VPoint(pos.X+r, pos.Y-r, r, canvas));
            points.Add(new VPoint(pos.X+w-r, pos.Y-r, r, canvas));
            points.Add(new VPoint(pos.X+r, pos.Y-h+r, r, canvas));
            points.Add(new VPoint(pos.X+w-r, pos.Y-h+r, r, canvas));
            for (int i = 0; i < 4; i++)
            {
                sticks.Add(new VStick(points[0], points[1], points[0].pos.Distance(points[1].pos), canvas));
                sticks.Add(new VStick(points[0], points[2], points[0].pos.Distance(points[2].pos), canvas));
                sticks.Add(new VStick(points[1], points[2], points[1].pos.Distance(points[2].pos), canvas));
                sticks.Add(new VStick(points[1], points[3], points[1].pos.Distance(points[3].pos), canvas));
                sticks.Add(new VStick(points[2], points[3], points[2].pos.Distance(points[3].pos), canvas));
                sticks.Add(new VStick(points[3], points[0], points[3].pos.Distance(points[0].pos), canvas));
            }
            ra = (int)r;
        }
        public VBox(PointF pos, float w, float h, Canvas canvas, Vec2 v)
        {
            points.Add(new VPoint(pos.X - w / 2, pos.Y - h / 2, canvas, v));
            points.Add(new VPoint(pos.X + w / 2, pos.Y - h / 2, canvas, v));
            points.Add(new VPoint(pos.X - w / 2, pos.Y + h / 2, canvas, v));
            points.Add(new VPoint(pos.X + w / 2, pos.Y + h / 2, canvas, v));
            sticks.Add(new VStick(points[0], points[1], points[0].pos.Distance(points[1].pos), canvas));
            sticks.Add(new VStick(points[0], points[2], points[0].pos.Distance(points[2].pos), canvas));
            sticks.Add(new VStick(points[1], points[2], points[1].pos.Distance(points[2].pos), canvas));
            sticks.Add(new VStick(points[1], points[3], points[1].pos.Distance(points[3].pos), canvas));
            sticks.Add(new VStick(points[2], points[3], points[2].pos.Distance(points[3].pos), canvas));
            sticks.Add(new VStick(points[3], points[0], points[3].pos.Distance(points[0].pos), canvas));
        }

        public void Render (Canvas c, int r, int b)
        {
            Boolean w;
            int wins = 0;
            for (int i = 0; i < points.Count; i++)
            {
                w = points[i].WRender();
                if (w) wins++;
            }
            for (int i = 0; i < sticks.Count; i++)
            {
                sticks[i].Update();
            }
            if (wins < 4)
            {
                d.Clear();
                d.Add(new Point((int)(points[0].pos.x), (int)(points[0].pos.y)));
                d.Add(new Point((int)(points[1].pos.x), (int)(points[1].pos.y)));
                d.Add(new Point((int)(points[2].pos.x), (int)(points[2].pos.y)));
                d.Add(new Point((int)(points[3].pos.x), (int)(points[3].pos.y)));
                c.Draw2ShadedTriangle(d[0], d[1], d[2], d[3], ra, r, b);
            }
        }
        public override string ToString()
        {
            return w.ToString() + "" + h.ToString();
        }



    }
}
