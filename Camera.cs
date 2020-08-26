using DxLibDLL;

namespace Action_Game_Base
{
    public static class Camera
    {
        public static float x;
        public static float y;

        public static void LookAt(float targetX, float targetY)
        {
            x = targetX - Screen.Width / 2;
            y = targetY - Screen.Height / 2;
        }

        public static void DrawGraph(float worldX, float worldY, int handle)
        {
            //if (flip) DX.DrawTurnGraphF(worldX - x, worldY - y, handle);
            DX.DrawGraphF(worldX - x, worldY - y, handle);
        }

        public static void DrawLineBox(float left, float top, float right, float bottom, uint color)
        {
            DX.DrawBox(
                (int)(left - x + .5f),
                (int)(top - y + .5f),
                (int)(right - x + .5f),
                (int)(bottom - y + .5f),
                color,
                DX.FALSE);
        }

        public static void DrawTileOutline(int worldX, int worldY)
        {
            worldX = worldX - (worldX % 32);
            worldY = worldY - (worldY % 32);


            DX.DrawBox(
                worldX - (int)x,
                worldY - (int)y,
                worldX - (int)x + 32,
                worldY - (int)y + 32,
                DX.GetColor(255, 0, 0),
                DX.FALSE);
        }

        public static void DrawString(float worldX, float worldY, string text)
        {
            DX.DrawStringF(worldX - x, worldY - y, text, DX.GetColor(255, 255, 255));
        }

        public static int ConvertScreenXToWorldX(int screenX)
        {
            return screenX + (int)x;
        }

        public static int ConvertScreenYToWorldY(int screenY)
        {
            return screenY + (int)y;
        }

        public static void DrawParticle(float worldX, float worldY, bool isDead, int red, int green, int blue, int blendMode, int alpha, float scale, float angle, int imageHandle)
        {
            if (isDead)
                return;

            DX.SetDrawBright(red, green, blue);
            DX.SetDrawBlendMode(blendMode, alpha);

            DX.DrawRotaGraphFastF(worldX - x, worldY - y, scale, angle, imageHandle);

            DX.SetDrawBlendMode(DX.DX_BLENDMODE_ALPHA, 255);
            DX.SetDrawBright(255, 255, 255);
        }
    }
}
