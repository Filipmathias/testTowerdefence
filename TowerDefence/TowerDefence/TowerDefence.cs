using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.GamerServices;
using System.IO;
using System.Windows.Forms;

namespace TowerDefence
{
    class TowerDefence : IScreen
    {
        public TowerDefence()
        {

        }
        public UIHandler UI
        {
            get { throw new NotImplementedException(); }
        }

        public void Load()
        {
            throw new NotImplementedException();
        }

        public void Update(GameTime gametime)
        {
            throw new NotImplementedException();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }
    }
    



    class Enemy 
    {
        Texture2D texture;
        public List<Point> Path { get; set; }
        Vector2 Pos { get; set; }
        
        float Health { get; set; }
        byte Armor { get; set; }
        bool Alive { get; set; }
        float Speed { get; set; }
        int PathPos = 0;

        public Enemy(List<Point> path) 
        {
            Alive = true;
            if(path.Count !=0)
            {
                Pos =  new  Vector2(path[0].X * 30 + 405,path[0].Y*30+5);
                target = new Vector2(path[1].X * 30 + 405, path[1].Y * 30+5); 
            }
                Path = path;
                Speed = 100;
        }



        public void Damage(int dmg) 
        {
            Health = Health - ((float)dmg - (float)dmg * (float)Armor * 0.01f);
            if (Health<=0) 
            {
                Alive = false;
            }

        }

        
        Vector2 target ;
        int pointinpath= 1;
        public void Update(GameTime gametime)
        {
            Vector2 t = target - Pos;
            t.X = Math.Sign(t.X);
            t.Y = Math.Sign(t.Y);

            if (gametime.ElapsedGameTime.TotalSeconds * Speed >= Vector2.Distance(target,Pos)) 
            {

                if (pointinpath+1 < Path.Count)
                {
                    
                    pointinpath++;
                    float remaining = (float)(gametime.ElapsedGameTime.TotalSeconds * Speed - Vector2.Distance(target, Pos));
                    Pos = new Vector2(Path[pointinpath - 1].X * 30 + 405, Path[pointinpath - 1].Y * 30 + 5);
                    target = new Vector2(Path[pointinpath].X * 30 + 405, Path[pointinpath].Y * 30 + 5);
                 

                }
                else
                {
                    pointinpath = 1;
                    Pos = new Vector2(Path[0].X * 30 + 405, Path[0].Y * 30 + 5);
                    target = new Vector2(Path[1].X * 30 + 405, Path[1].Y * 30 + 5);
                }
                
            }
            else 
            {
                Pos += t * (float)(Speed * gametime.ElapsedGameTime.TotalSeconds);
            }
           




        }    
        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(UILoader.circle, new Rectangle((int)Pos.X, (int)Pos.Y, 20, 20), Color.Black);


        }


    }

    class MapArea 
    {
        
        
        bool wasPressed=false;
        bool isDown = false;
        List<ITower> Towers = new List<ITower>();
        byte[] MapData = new byte[400];
        List<Enemy> Enemies = new List<Enemy>();
        string MapPath = "";
        bool firstrun = true;
        List<Point> enemyPath = new List<Point>();
        Point pointSquare = new Point(-1,-1);
        public bool WasPressed { get { return wasPressed; } }
        public bool IsDown { get { return isDown; } }
        public Point Squarepressed { get { return pointSquare; } }
        public Enemy enemy;


        public MapArea()
        {
            MapData = File.ReadAllBytes(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\maps\\Default.TDM");
            enemyPath = new Astar().FindPath(MapData, Towers).pathPoints;
            firstrun = false;
            enemy = new Enemy(enemyPath);
        }

        bool checkHover()
        {
            return new Rectangle(400, 0, 600, 600).Contains(new Point(Game1.Instance.mouseState.X, Game1.Instance.mouseState.Y));


        }

       public void Update(GameTime gametime)
        {


           //check pressed
          if (wasPressed)
          {
              wasPressed = false;
          }
          MouseState ms = Game1.Instance.mouseState;
          if (isDown & ms.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Released)
          {
              wasPressed = true;
              pointSquare = new Point((Game1.Instance.mouseState.X-400) / 30, Game1.Instance.mouseState.Y / 30);

          }
          if (ms.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed & checkHover())
          {
              isDown = true;

          }
          else
          {
              isDown = false;
          }
          enemy.Update(gametime);




        }
        
        public void Draw(SpriteBatch spriteBatch)
        {
            DrawMap(spriteBatch);
            foreach(ITower t in Towers)
            {
                t.Draw(spriteBatch);
            }
            enemy.Draw(spriteBatch);
        }
        
        
        public bool AddTower(ITower tower, Point pos) 
        {
            if (MapData[pos.X + pos.Y * 20] == 0)
            {
                foreach (ITower t in Towers)
                {
                    if (t.Position == pos)
                    {
                        return false;

                    }

                }

                ITower[] towerTemp = new ITower[Towers.Count];
                Towers.CopyTo(towerTemp);
                List<ITower> tempTowerlist = new List<ITower>();
                tempTowerlist.AddRange(towerTemp);
                tempTowerlist.Add(tower);




                PathResult result = new Astar().FindPath(MapData, tempTowerlist);
                if (result.Success)
                {
                    Towers.Add(tower);
                    enemyPath = result.pathPoints;
                    return true;
                }
                else
                {
                    return false;
                }

            }

            return false;

            
        }
        



        void DrawMap(SpriteBatch spriteBatch) 
        {
            for (int i = 0; i < 400; i++)
            {
                spriteBatch.Draw(UILoader.ButtonTexture, new Rectangle((400 + i * 30) - (i / 20 * 600), (i / 20) * 30, 30, 30), getColor(MapData[i]));
            }   


        }





        Color getColor(byte b)
        {
            switch (b)
            {
                case 0: return Color.LightSlateGray;
                case 1: return Color.LimeGreen;
                case 255: return Color.Orange;
                case 10: return Color.Brown;
                default: return Color.White;
            }

        }


    }

}
