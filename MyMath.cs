using System;

namespace Action_Game_Base
{
    public static class MyMath
    {
        public static bool RectRectIntersection(
            float aLeft, float aTop, float aRight, float aBottom,
            float bLeft, float bTop, float bRight, float bBottom)
        {
            return
                aLeft < bRight &&
                aRight > bLeft &&
                aTop < bBottom &&
                aBottom > bTop;
        }

        public const float PI = (float)Math.PI;
        public const float Deg2Rad = PI / 180f; // 度からラジアンに変換する定数

        public static float Lerp(float a, float b, float t)
        {
            return a + (b - a) * t;
        }

        public static float PointToPointAngle(float fromX, float fromY, float toX, float toY)
        {
            return (float)Math.Atan2(toY - fromY, toX - fromX);
        }

        public static float DistanceBetweenTwoPoints(float fromX, float fromY, float toX, float toY)
        {
            return (float)Math.Sqrt(Math.Pow(toX - fromX, 2) + Math.Pow(toY - fromY, 2));
        }
    }
}
