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
        MapArea Map;

        public Queue<Point> Path
        {
            get;
            set;
        }


        Vector2 Pos { get; set; }
        float Health { get; set; }
        byte Armor { get; set; }
        bool Alive { get; set; }
        float Speed { get; set; }
        int PathPos = 0;
        Point PointPos 
        {
            get { return new Point((int)LocalPos.X / 30 , (int)LocalPos.Y / 30); }
        }
        Vector2 LocalPos 
        {
            get { return Pos - Map.DrawPos; }
        }



        public Enemy(List<Point> path,MapArea map) 
        {
            Map = map;
            Alive = true;
            Path = new Queue<Point>(path);



            if(path.Count !=0)
            {
                Point posT = Path.Dequeue();
                Point tarT = Path.Dequeue();
                Pos = new Vector2((posT.X * 30) + (int)Map.DrawPos.X + 5, (posT.Y * 30) + (int)Map.DrawPos.Y + 5);
                target = new Vector2((tarT.X * 30) + (int)Map.DrawPos.X + 5 , (tarT.Y * 30) + (int)Map.DrawPos.Y + 5); 
            }
             
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
        public void Update(GameTime gametime)
        {
            Vector2 t = target - Pos;
            t.X = Math.Sign(t.X);
            t.Y = Math.Sign(t.Y);

            if (gametime.ElapsedGameTime.TotalSeconds * Speed >= Vector2.Distance(target,Pos)) 
            {

                if (Path.Count!=0)
                {
                    
                    float remaining = (float)(gametime.ElapsedGameTime.TotalSeconds * Speed - Vector2.Distance(target, Pos));
                    Pos = target;
                    Point p = Path.Dequeue();

                    target = new Vector2(p.X * 30 + (int)Map.DrawPos.X +5, p.Y * 30 +(int)Map.DrawPos.Y + 5);                 
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

        public void UpdatePath()
        {
            
          Path = new Queue<Point>( new Astar().FindPath(Map.MapData, Map.Towers, PointPos).pathPoints);
           

        }



    }

    class MapArea 
    {
        
        
        bool wasPressed=false;
        bool isDown = false;
        public List<ITower> Towers = new List<ITower>();
        public  byte[] MapData = new byte[400];
        List<Enemy> Enemies = new List<Enemy>();
        string MapPath = "";
        Point pointSquare = new Point(-1,-1);
        public bool WasPressed { get { return wasPressed; } }
        public bool IsDown { get { return isDown; } }
        public Point Squarepressed { get { return pointSquare; } }
        public Enemy enemy;
        public Vector2 DrawPos{get;set;}

        public MapArea(Vector2 drawPos)
        {
            MapData = File.ReadAllBytes(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\maps\\Default.TDM");
            DrawPos = drawPos;
            enemy = new Enemy(new Astar().FindPath(MapData, Towers).pathPoints, this);
        }

        bool checkHover()
        {
            return new Rectangle((int)DrawPos.X, (int)DrawPos.Y, 600, 600).Contains(new Point(Game1.Instance.mouseState.X, Game1.Instance.mouseState.Y));
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
              pointSquare = new Point((Game1.Instance.mouseState.X - (int)DrawPos.X) / 30,( Game1.Instance.mouseState.Y -(int)DrawPos.Y) / 30);
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
                
                //ITower[] towerTemp = new ITower[Towers.Count];
                //Towers.CopyTo(towerTemp);
                List<ITower> tempTowerlist = new List<ITower>();
                tempTowerlist.AddRange(Towers);
                tempTowerlist.Add(tower);
                
                if(new Astar().FindPath(MapData,tempTowerlist).Success)                
                {
                    Towers.Add(tower);
                    enemy.UpdatePath();
                    return true;
                }
                



            }

            return false;

            
        }
        



        void DrawMap(SpriteBatch spriteBatch) 
        {
            for (int i = 0; i < 400; i++)
            {
                spriteBatch.Draw(UILoader.ButtonTexture, new Rectangle(((int)DrawPos.X + i * 30) - ((int)DrawPos.Y + i / 20 * 600), (i / 20) * 30, 30, 30), getColor(MapData[i]));
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
