using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Verlet
{
    public class Player: VBody
    {        
        public Player(VPoint pos, Canvas canva) 
        { 
            points.Add(new VPoint(pos.pos.x, pos.pos.y, 5, canva));
            points.Add(new VPoint(pos.pos.x-12, pos.pos.y, 5, canva));
            points.Add(new VPoint(pos.pos.x - 6, pos.pos.y - 4, 1, canva));
            points.Add(new VPoint(pos.pos.x - 6, pos.pos.y - 6, 5, canva));
            points.Add(new VPoint(pos.pos.x - 6, pos.pos.y +2, 1, canva));
            points.Add(new VPoint(pos.pos.x - 9, pos.pos.y +6, 5, canva));
            points.Add(new VPoint(pos.pos.x - 3, pos.pos.y + 6, 5, canva));
            sticks.Add(new VStick(points[0], points[2], canva));
            sticks.Add(new VStick(points[1], points[2], canva));
            sticks.Add(new VStick(points[2], points[3], canva));
            sticks.Add(new VStick(points[2], points[4], canva));
            sticks.Add(new VStick(points[4], points[5], canva));
            sticks.Add(new VStick(points[4], points[6], canva));
            sticks.Add(new VStick(points[3], points[4], canva));
        }

        public Boolean Render(Canvas canva)
        {
            Boolean w;
            int c = 0;
            for (int i = 1; i < points.Count; i++)
            {
                w = points[i].WRender();
                if (w) c++;
                canva.g.FillEllipse(Brushes.Black, points[i].pos.x - points[i].radius, points[i].pos.y - points[i].radius, points[i].diameter, points[i].diameter);
            }
            for (int i = 0; i < sticks.Count - 1; i++)
            {
                sticks[i].Update();
                canva.DrawLine(new System.Drawing.Point((int)sticks[i].startPoint.pos.x, (int)sticks[i].startPoint.pos.y), new System.Drawing.Point((int)sticks[i].endPoint.pos.x, (int)sticks[i].endPoint.pos.y), System.Drawing.Color.Black);
            }
            if (c > 0) return true; else return false;

        }
    }
}
