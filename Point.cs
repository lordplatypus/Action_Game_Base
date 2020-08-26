using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Action_Game_Base
{
    public struct Point
    {
        public float x;
        public float y;

        public Point(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public override string ToString()
        {
            return "(" + x + ", " + y + ")";
        }

        public static implicit operator float[] (Point p)
        {
            float[] result = new float[] { p.x, p.y };
            return result;
        }

        //public static implicit operator bool (Point p1, Point p2)
        //{
        //    if (p1.x == p2.x && p1.y == p2.y) return true;
        //    else return false;
        //}
    }
}
