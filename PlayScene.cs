using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;
using MyLib;

namespace Action_Game_Base
{
    public class PlayScene : Scene
    {
        public Map map;
        public Player player;
        public List<GameObject> gameObjects = new List<GameObject>();

        int mouseX;
        int mouseY;
        public bool playerExists = false;

        public PlayScene()
        {
            map = new Map(this);
            //Camera.LookAt(player.x, player.y);
        }

        public override void Update()
        {
            MouseInput();

            int gameObjectsCount = gameObjects.Count;
            for (int i = 0; i < gameObjectsCount; i++)
            {
                gameObjects[i].StorePositionAndHitBox();
                gameObjects[i].Update();
            }

            for (int i = 0; i < gameObjects.Count; i++)
            {
                GameObject a = gameObjects[i];

                for (int j = i + 1; j < gameObjects.Count; j++)
                {
                    if (a.isDead) break;

                    GameObject b = gameObjects[j];

                    if (b.isDead) continue;

                    if (MyMath.RectRectIntersection(
                        a.GetLeft(), a.GetTop(), a.GetRight(), a.GetBottom(),
                        b.GetLeft(), b.GetTop(), b.GetRight(), b.GetBottom()))
                    {
                        a.OnCollision(b);
                        b.OnCollision(a);
                    }
                }
            }

            gameObjects.RemoveAll(go => go.isDead);

            //Camera.LookAt(player.x, player.y);
        }

        void MouseInput()
        {
            //Grab mouse location, relative to the screen size
            DX.GetMousePoint(out mouseX, out mouseY);
            //Convert mouse location, relative to world coordinents
            mouseX = Camera.ConvertScreenXToWorldX(mouseX);
            mouseY = Camera.ConvertScreenYToWorldY(mouseY);

            if (Input.GetMouseDown(DX.MOUSE_INPUT_LEFT))
            {
                if (map.GetTerrain(mouseX, mouseY) == -1 && !playerExists)
                {
                    player = new Player(this, mouseX, mouseY);
                    gameObjects.Add(player);
                    playerExists = true;
                }
            }
            if (Input.GetMouseDown(DX.MOUSE_INPUT_RIGHT) && playerExists)
            {
                player.Kill();
                playerExists = false;
            }
        }

        public override void Draw()
        {
            map.DrawMap();

            foreach (GameObject go in gameObjects)
            {
                go.Draw();
            }
        }       
    }
}
