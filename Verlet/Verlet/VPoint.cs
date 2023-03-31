using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Verlet
{
    public class VPoint
    {
        public Vec2 pos, old, gravity, vel;
        public float friction, groundFriction, radius, mass, diameter, bounce;
        public Color color;
        public Canvas bound;

        public VPoint(float x, float y, Canvas b)
        {
            pos = new Vec2(x, y);
            old = new Vec2(x, y);

            friction = 0.97f;
            groundFriction = 0.7f;

            gravity = new Vec2(0, 1);
            //bounce = 1.05f;

            radius = 5;
            color = Color.Orange;
            mass = 1;

            bound = b;

            radius = 5;
            diameter = radius + radius;
            mass = 1;

        }
        public VPoint(float x, float y, float r, Canvas b)
        {
            pos = new Vec2(x, y);
            old = new Vec2(x, y);

            friction = 0.9f;
            groundFriction = 0.7f;

            gravity = new Vec2(0, 1);
            //bounce = 1.05f;

            radius = r;
            color = Color.Orange;
            mass = 1;

            bound = b;

            diameter = radius + radius;
            mass = 1;

        }
        public VPoint(float x, float y, Canvas b, Vec2 v)
        { 
            pos = new Vec2(x, y);
            old = new Vec2(x, y);
            old -= v;

            friction = 0.97f; 
            groundFriction = 0.7f;

            gravity = new Vec2(0, 1);
            //bounce = 1.05f;

            radius = 5;
            color = Color.Orange; 
            mass = 1;

            bound = b;

            radius = 5;
            diameter = radius + radius;
            mass = 1;

        }
        public Boolean Constraints() 
        {
            Boolean win = false;
            if (pos.x > bound.Width - radius)
            {
                //pos.x = bound.Width - radius;
                win = true;
            }
            if (pos.x < radius)
            {
                pos.x = radius;

            }
            if (pos.y > bound.Height - radius)
            {
                pos.y = bound.Height - radius;
            }
            if (pos.y < radius)
            {
                pos.y = radius;
            }
            return win;
        }
        public Boolean Render () 
        {
            Boolean win;
            Update();
            win = Constraints();
            bound.g.FillEllipse(Brushes.Red, pos.x-radius, pos.y-radius, diameter, diameter);
            return win;
        }
        public Boolean WRender()
        {
            Boolean win;
            Update();
            win = Constraints();
            return win;
        }
        public void FixedRender()
        {
            pos = old;
            bound.g.FillEllipse(Brushes.Red, pos.x - radius, pos.y - radius, diameter, diameter);
        }
        public void Update () 
        {
            vel = pos - old;
            vel *= friction;

            if (pos.y >= bound.Height - radius && vel.MagSQR() > 0.000001)
            {
                float m = vel.Length();
                vel.x /= m;
                vel.y /= m;
                vel *= m * groundFriction;

            }
            old = pos;
            pos += vel; 
            pos += gravity;

        }
    }
}
