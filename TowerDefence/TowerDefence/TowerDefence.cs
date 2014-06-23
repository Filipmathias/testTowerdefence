using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.GamerServices;
using System.IO;
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
        public EPath Path { get; set; }
        Vector2 Pos { get; set; }
        float Health { get; set; }
        byte Armor { get; set; }
        bool Alive { get; set; }
        int Speed { get; set; }
        
        public void Damage(int dmg) 
        {
            Health = Health - ((float)dmg - (float)dmg * (float)Armor * 0.01f);
            if (Health<=0) 
            {
                Alive = false;
            }

        }


        public void Update(GameTime gametime) 
        {
            

        }

        public void Draw(SpriteBatch spritebatch)
        {

        }


    }

    class Map 
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Path">the path of the map file</param>
        byte[] mapArray;
            
        public Map(string Path)
        {
            mapArray = File.ReadAllBytes(Path);
        }


        public void Draw(SpriteBatch spritebatch)
        {
            //committest
        }

    }

    class EPath 
    {
        List<Point> Points = new List<Point>();
        public void TestPath()
        {
            Points.Add(new Point(0,1));
            Points.Add(new Point(20,1));
        }

    }
    
}
