using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.GamerServices;

namespace TowerDefence
{
    class TempTower
    {
    }
    interface ITower
    {
        Vector2 Position {get;} 
        void Update(GameTime gametime);
        void Draw(SpriteBatch spriteBatch);
    }

    class Tier1Normal: ITower
    {
        Vector2 Pos;
        Vector2 Size;
        Color Col;
        int Damage;
        int ASpeed;
        public Tier1Normal(Vector2 position)
        {
            Size = new Vector2(30,30);
            Col = Color.Ivory;
            Pos = position;
        }
        public Vector2 Position
        {
            get
            {
                return Pos;
            }
        }
        public void Update(GameTime gametime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(UILoader.ButtonTexture, new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y), Col);
        }
    }
}
