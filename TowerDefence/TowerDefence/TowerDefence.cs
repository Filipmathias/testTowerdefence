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
