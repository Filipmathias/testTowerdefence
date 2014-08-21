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
