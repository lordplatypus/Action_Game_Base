using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Action_Game_Base
{
    class Enemy : GameObject
    {
        const int CellSize = 32;
        const float Speed = 2f;

        float angleToPoint = 0;
        float vx = 0;
        float vy = 0;
        Point moveTo;

        public Enemy(PlayScene playScene, float x, float y) : base (playScene)
        {
            this.x = x;
            this.y = y;

            imageWidth = 16;
            imageHeight = 16;
            hitboxOffsetLeft = 0;
            hitboxOffsetRight = 0;
            hitboxOffsetTop = 0;
            hitboxOffsetBottom = 0;
        }

        public override void Update()
        {
            if (playScene.playerExists)
            {
                moveTo = playScene.map.Move(new Point(x, y));
                angleToPoint = MyMath.PointToPointAngle(x, y, moveTo.x, moveTo.y);
                MoveX();
                MoveY();
            }
        }

        void MoveX()
        {
            vx = (float)Math.Cos(angleToPoint) * Speed;
            x += vx;

            float left = GetLeft();
            float right = GetRight() - .01f;
            float top = GetTop();
            float middle = top + 8;
            float bottom = GetBottom() - .01f;

            if (playScene.map.IsWall(left, top) ||
                playScene.map.IsWall(left, middle) ||
                playScene.map.IsWall(left, bottom))
            {//check right
                float wallRight = left - left % Map.CellSize + Map.CellSize;
                SetLeft(wallRight);
            }
            else if (playScene.map.IsWall(right, top) ||
                playScene.map.IsWall(right, middle) ||
                playScene.map.IsWall(right, bottom))
            {//check left
                float wallLeft = right - right % Map.CellSize;
                SetRight(wallLeft);
            }
        }

        void MoveY()
        {
            vy = (float)Math.Sin(angleToPoint) * Speed;
            y += vy;

            float left = GetLeft();
            float right = GetRight() - .01f;
            float top = GetTop();
            float middle = left + 8;
            float bottom = GetBottom() - .01f;

            if (playScene.map.IsWall(left, top) ||
                playScene.map.IsWall(middle, top) ||
                playScene.map.IsWall(right, top))
            {//check up
                float wallUp = top - top % Map.CellSize + Map.CellSize;
                SetTop(wallUp);
            }
            else if (playScene.map.IsWall(left, bottom) ||
                playScene.map.IsWall(middle, bottom) ||
                playScene.map.IsWall(right, bottom))
            {//check down
                float wallDown = bottom - bottom % Map.CellSize;
                SetBottom(wallDown);
            }
        }

        public override void Draw()
        {
            Camera.DrawGraph(x, y, Image.enemy);
            moveTo.x /= 32;
            moveTo.y /= 32;
            Camera.DrawString(x, y, moveTo.ToString());
        }

        public override void OnCollision(GameObject other)
        {
        }
    }
}
