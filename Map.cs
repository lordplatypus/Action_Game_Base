using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLib;

namespace Action_Game_Base
{
    public class Map
    {
        public const int CellSize = 32;
        public const int None = -1;
        public const int Wall = 0;

        PlayScene playScene;
        int[,] map;
        int[,] objectMap;
        int width = 10;
        int height = 10;

        public Map(PlayScene playScene)
        {
            this.playScene = playScene;
            
            map = new int[width, height];
            objectMap = new int[width, height];
            CreateMapArray();
            SpawnObject();
        }

        void CreateMapArray()
        {//fill in arrays with numbers (they will be changed later)
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (x == 0 || y == 0 || x == width - 1 || y == height - 1)
                    {
                        map[x, y] = 0;
                    }
                    else map[x, y] = -1;
                    objectMap[x, y] = -1; //filled with 'empty space'
                }
            }
            objectMap[2, 2] = 0;
            for (int i = 1; i < 7; i++)
            {
                map[5, i] = 0;
            }
        }

        

        void SpawnObject()
        {//places objects (i.e. player, enemies, boss, items, etc.) using 'objectMap'
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    //if the spot is empty, skip
                    if (objectMap[x, y] == -1) continue;
                    //convert from block coordinates to world coordinates
                    int worldX = x * CellSize;
                    int worldY = y * CellSize;
                    //spawn object based on ID stored at that location
                    if (objectMap[x, y] == 0)
                    {//Spawn player
                        playScene.gameObjects.Add(new Enemy(playScene, worldX, worldY));
                    }
                }
            }
        }

        public void DrawMap()
        {
            int left = (int)(Camera.x / CellSize);
            int top = (int)(Camera.y / CellSize);
            int right = (int)(Camera.x + Screen.Width - 1 / CellSize);
            int bottom = (int)(Camera.y + Screen.Height - 1 / CellSize);

            if (left < 0) left = 0;
            if (top < 0) top = 0;
            if (right >= width) right = width - 1;
            if (bottom >= height) bottom = height - 1;

            for (int y = top; y <= bottom; y++)
            {
                for (int x = left; x <= right; x++)
                {
                    int id = map[x, y];

                    if (id == None) continue;

                    Camera.DrawGraph(x * CellSize, y * CellSize, Image.tile);
                }
            }
        }

        public int GetTerrain(float worldX, float worldY)
        {
            int mapX = (int)(worldX / CellSize);
            int mapY = (int)(worldY / CellSize);

            if (mapX < 0 || mapX >= width || mapY < 0 || mapY >= height)
                return None;

            return map[mapX, mapY];
        }

        public bool IsWall(float worldX, float worldY)
        {
            int terrainID = GetTerrain(worldX, worldY);

            if (terrainID == 0)
            {
                return true;
            }
            else return false;
        }

        public void DeleteWall(float worldX, float worldY)
        {
            int mapX = (int)(worldX / CellSize);
            int mapY = (int)(worldY / CellSize);

            if (GetTerrain(mapX, mapY) == 0)
            {
                map[mapX, mapY] = -1;
            }
        }

        bool finish = false;
        Point lastLoc;

        public Point Move(Point location)
        {
            Point playerLoc = new Point((int)(playScene.player.x / CellSize), (int)(playScene.player.y / CellSize));
            Point enemyLoc = new Point((int)(location.x / CellSize), (int)(location.y / CellSize));
            List<Point> visited = new List<Point>();
            visited.Add(playerLoc);

            Point moveTo = DFS(playerLoc, visited, enemyLoc);
            finish = false;
            moveTo.x *= CellSize;
            moveTo.y *= CellSize;
            return moveTo;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="location">Player location</param>
        /// <param name="visited"></param>
        /// <param name="end">Enemy(the one calling the function) location</param>
        /// <returns></returns>
        Point DFS(Point location, List<Point> visited, Point end)
        {
            Point moveTo = end;

            Point currentLoc = location;
            visited.Add(currentLoc);

            bool firstChoice = false;
            bool secondChoice = false;
            bool thirdChoice = false;
            bool fourthChoice = false;
            Point up = new Point(currentLoc.x, currentLoc.y - 1);
            Point right = new Point(currentLoc.x + 1, currentLoc.y);
            Point down = new Point(currentLoc.x, currentLoc.y + 1);
            Point left = new Point(currentLoc.x - 1, currentLoc.y);


            Point firstMove = location;
            Point secondMove = location;
            Point thirdMove = location;
            Point fourthMove = location;
            Point playerLoc = new Point((int)(playScene.player.x / CellSize), (int)(playScene.player.y / CellSize));
            if (end.x < playerLoc.x && end.y < playerLoc.y)
            {
                if (Math.Abs(playerLoc.y - end.y) > Math.Abs(playerLoc.x - end.x))
                {//prioritize the further point
                    firstMove = down;
                    secondMove = left;
                    thirdMove = right;
                    fourthMove = up;
                }
                else
                {
                    firstMove = left;
                    secondMove = down;
                    thirdMove = up;
                    fourthMove = right;
                }
            }
            else if (end.x > playerLoc.x && end.y < playerLoc.y)
            {
                if (Math.Abs(playerLoc.y - end.y) > Math.Abs(playerLoc.x - end.x))
                {//prioritize the further point
                    firstMove = down;
                    secondMove = right;
                    thirdMove = left;
                    fourthMove = up;
                }
                else
                {
                    firstMove = right;
                    secondMove = down;
                    thirdMove = up;
                    fourthMove = left;
                }
            }
            else if (end.x > playerLoc.x && end.y > playerLoc.y)
            {
                if (Math.Abs(playerLoc.y - end.y) > Math.Abs(playerLoc.x - end.x))
                {//prioritize the further point
                    firstMove = up;
                    secondMove = right;
                    thirdMove = left;
                    fourthMove = down;
                }
                else
                {
                    firstMove = right;
                    secondMove = up;
                    thirdMove = down;
                    fourthMove = left;
                }
            }
            else if (end.x < playerLoc.x && end.y > playerLoc.y)
            {
                if (Math.Abs(playerLoc.y - end.y) > Math.Abs(playerLoc.x - end.x))
                {//prioritize the further point
                    firstMove = up;
                    secondMove = left;
                    thirdMove = right;
                    fourthMove = down;
                }
                else
                {
                    firstMove = left;
                    secondMove = up;
                    thirdMove = down;
                    fourthMove = right;
                }
            }
            else if (end.x == playerLoc.x)
            {
                if (end.y > playerLoc.y)
                {//prioritize the further point
                    firstMove = down;
                    secondMove = left;
                    thirdMove = right;
                    fourthMove = up;
                }
                else
                {
                    firstMove = up;
                    secondMove = right;
                    thirdMove = left;
                    fourthMove = down;
                }
            }
            else if (end.y == playerLoc.y)
            {
                if (end.x > playerLoc.x)
                {//prioritize the further point
                    firstMove = right;
                    secondMove = down;
                    thirdMove = up;
                    fourthMove = left;
                }
                else
                {
                    firstMove = left;
                    secondMove = up;
                    thirdMove = down;
                    fourthMove = right;
                }
            }

            DFSFinish(currentLoc, end);
            if (finish) return currentLoc;

            if (!visited.Contains(firstMove) && map[(int)firstMove.x, (int)firstMove.y] == -1) firstChoice = true;
            if (!visited.Contains(secondMove) && map[(int)secondMove.x, (int)secondMove.y] == -1) secondChoice = true;
            if (!visited.Contains(thirdMove) && map[(int)thirdMove.x, (int)thirdMove.y] == -1) thirdChoice = true;
            if (!visited.Contains(fourthMove) && map[(int)fourthMove.x, (int)fourthMove.y] == -1) fourthChoice = true;

            if (firstChoice && !finish)
            {
                moveTo = DFS(firstMove, visited, end);
            }
            if (secondChoice && !finish)
            {
                moveTo = DFS(secondMove, visited, end);
            }
            if (thirdChoice && !finish)
            {
                moveTo = DFS(thirdMove, visited, end);
            }
            if (fourthChoice && !finish)
            {
                moveTo = DFS(fourthMove, visited, end);
            }

            return moveTo;
        }

        //Point DFS(Point location, List<Point> visited, Point end)
        //{
        //    Point moveTo = end;

        //    Point currentLoc = location;
        //    visited.Add(currentLoc);

        //    Point up = new Point(currentLoc.x, currentLoc.y - 1);
        //    bool canMoveUp = false;
        //    Point right = new Point(currentLoc.x + 1, currentLoc.y);
        //    bool canMoveRight = false;
        //    Point down = new Point(currentLoc.x, currentLoc.y + 1);
        //    bool canMoveDown = false;
        //    Point left = new Point(currentLoc.x - 1, currentLoc.y);
        //    bool canMoveLeft = false;

        //    DFSFinish(currentLoc, end);
        //    if (finish) return currentLoc;

        //    if (!visited.Contains(up) && map[(int)up.x, (int)up.y] == -1) canMoveUp = true;
        //    if (!visited.Contains(right) && map[(int)right.x, (int)right.y] == -1) canMoveRight = true;
        //    if (!visited.Contains(down) && map[(int)down.x, (int)down.y] == -1) canMoveDown = true;
        //    if (!visited.Contains(left) && map[(int)left.x, (int)left.y] == -1) canMoveLeft = true;

        //    if (canMoveUp && !finish)
        //    {
        //        moveTo = DFS(up, visited, end);
        //    }
        //    if (canMoveRight && !finish)
        //    {
        //        moveTo = DFS(right, visited, end);
        //    }
        //    if (canMoveDown && !finish)
        //    {
        //        moveTo = DFS(down, visited, end);
        //    }
        //    if (canMoveLeft && !finish)
        //    {
        //        moveTo = DFS(left, visited, end);
        //    }

        //    return moveTo;
        //}

        void DFSFinish(Point current, Point end)
        {
            if (finish) return;

            if (current.x == end.x &&
                current.y - 1 == end.y)
            {//up
                finish = true;
            }
            else if (current.x + 1== end.x &&
                current.y == end.y)
            {//right
                finish = true;
            }
            else if (current.x == end.x &&
                current.y + 1 == end.y)
            {//down
                finish = true;
            }
            else if (current.x - 1 == end.x &&
                current.y == end.y)
            {//left
                finish = true;
            }
        }
    }
}
