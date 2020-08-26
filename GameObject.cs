using DxLibDLL;
using MyLib;

namespace Action_Game_Base
{
    public abstract class GameObject
    {
        public float x;
        public float y;
        public bool isDead = false;

        protected PlayScene playScene;
        protected int imageWidth;
        protected int imageHeight;
        protected int hitboxOffsetLeft = 0;
        protected int hitboxOffsetRight = 0;
        protected int hitboxOffsetTop = 0;
        protected int hitboxOffsetBottom = 0;

        float prevX;
        float prevY;
        float prevLeft;
        float prevRight;
        float prevTop;
        float prevBottom;

        public GameObject(PlayScene playScene)
        {
            this.playScene = playScene;
        }

        public virtual float GetLeft()
        {
            return x + hitboxOffsetLeft;
        }

        public virtual void SetLeft(float left)
        {
            x = left - hitboxOffsetLeft;
        }

        public virtual float GetRight()
        {
            return x + imageWidth - hitboxOffsetRight;
        }

        public virtual void SetRight(float right)
        {
            x = right + hitboxOffsetRight - imageWidth;
        }

        public virtual float GetTop()
        {
            return y + hitboxOffsetTop;
        }

        public virtual void SetTop(float top)
        {
            y = top - hitboxOffsetTop;
        }

        public virtual float GetBottom()
        {
            return y + imageHeight - hitboxOffsetBottom;
        }

        public virtual void SetBottom(float bottom)
        {
            y = bottom + hitboxOffsetBottom - imageHeight;
        }

        public float GetDeltaX()
        {
            return x - prevX;
        }

        public float GetDeltaY()
        {
            return y - prevY;
        }

        public float GetPrevLeft()
        {
            return prevLeft;
        }

        public float GetPrevRight()
        {
            return prevRight;
        }

        public float GetPrevTop()
        {
            return prevTop;
        }

        public float GetPrevBottom()
        {
            return prevBottom;
        }

        public void StorePositionAndHitBox()
        {
            prevX = x;
            prevY = y;
            prevLeft = GetLeft();
            prevRight = GetRight();
            prevTop = GetTop();
            prevBottom = GetBottom();
        }

        public abstract void Update();

        public abstract void Draw();

        public void DrawHitBox()
        {
            //Camera.DrawLineBox(GetLeft(), GetTop(), GetRight(), GetBottom(), DX.GetColor(255, 0, 0));
        }

        public abstract void OnCollision(GameObject other);

        public virtual bool IsVisible()
        {
            return MyMath.RectRectIntersection(
                x, y, x + imageWidth, y + imageHeight,
                Camera.x, Camera.y, Camera.x + Screen.Width, Camera.y + Screen.Height);
        }

        public virtual void Kill()
        {
            isDead = true;
        }
    }
}
