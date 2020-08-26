using System;
using System.Collections.Generic;
using DxLibDLL;
using MyLib;

namespace Action_Game_Base
{
    public class Player : GameObject
    {
        public Player(PlayScene playScene, float x, float y) : base(playScene)
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
        }        

        public override void Draw()
        {
            Camera.DrawGraph(x, y, Image.player);
            Camera.DrawString(x, y - 16, new Point((int)x/32, (int)y/32).ToString());
        }

        public override void OnCollision(GameObject other)
        {
            if (other is Enemy)
            {
                Kill();
                playScene.playerExists = false;
            }
        }
    }
}
