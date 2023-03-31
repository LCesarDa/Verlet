using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Verlet
{
    public class Vec2
    {
        public float x, y;

        public Vec2(float x, float y)
        {
            this.x = x;
            this.y = y;

            Color c = Color.Red;
        }
        public static Vec2 operator -(Vec2 v1)
        {
            v1.x = -v1.x;
            v1.y = -v1.y;
            return v1;
        }
        public static Vec2 operator -(Vec2 v1, Vec2 v2)
        {
            Vec2 v3 = new Vec2(0,0);
            v3.x = v1.x - v2.x;
            v3.y = v1.y - v2.y;
            return v3;
        }
        public static Vec2 operator +(Vec2 v1, Vec2 v2)
        {
            Vec2 v3 = new Vec2(0, 0);
            v3.x = v1.x + v2.x;
            v3.y = v1.y + v2.y;
            return v3;
        }
        public static Vec2 operator *(Vec2 v1, float a)
        {
            Vec2 v3 = new Vec2(0, 0);
            v3.x = v1.x * a;
            v3.y = v1.y * a;
            return v3;
        }
        public static Vec2 operator *(Vec2 v1, Vec2 v2)
        {
            Vec2 v3 = new Vec2(0, 0);
            v3.x = v1.x * v2.x;
            v3.y = v1.y * v2.y;
            return v3;
        }
        public static Vec2 operator /(Vec2 v1, Vec2 v2)
        {
            Vec2 v3 = new Vec2(0, 0);
            v3.x = v1.x / v2.x;
            v3.y = v1.y / v2.y;
            return v3;
        }
        public static Vec2 operator /(Vec2 v1, float a)
        {
            Vec2 v3 = new Vec2(0, 0);
            v3.x = v1.x / a;
            v3.y = v1.y / a;
            return v3;
        }
        public float MagSQR()
        {
            float m = (x * x) + (y * y);
            return m;
        }
        public float Length()
        {
            float l = (float)Math.Sqrt((x * x) + (y * y));
            return l;
        }
        public float Distance(Vec2 a)
        {
            float d = (float)Math.Sqrt(Math.Pow(x - a.x, 2) + Math.Pow(y - a.y, 2));
            return d;
        }
    }
}
