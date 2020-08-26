using DxLibDLL;
using MyLib;

namespace Action_Game_Base
{
    public class Game
    {
        static Scene scene;

        public void Init()
        {
            Input.Init();
            MyRandom.Init();
            Image.Load();
            scene = new PlayScene();
        }

        public void Update()
        {
            Input.Update();
            scene.Update();
        }

        public void Draw()
        {
            scene.Draw();
        }

        public static void ChangeScene(Scene newScene)
        {
            scene = newScene;
        }
    }
}
