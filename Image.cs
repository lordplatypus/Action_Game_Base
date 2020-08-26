using DxLibDLL;

namespace Action_Game_Base
{
    public static class Image
    {
        public static int player;
        public static int enemy;
        public static int tile;

        public static void Load()
        {
            player = DX.LoadGraph("Image/player.png");
            enemy = DX.LoadGraph("Image/enemy.png");
            tile = DX.LoadGraph("Image/tile.png");
        }
    }
}
