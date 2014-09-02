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
    public interface ITower
    {

        MapArea mapArea {get;}
        Point Position {get;} 
        void Update(GameTime gametime);
        void Draw(SpriteBatch spriteBatch);
    }

    class Tier1Normal: ITower
    {
        MapArea Map;
        Point Pos;
        Color Col;
        int Damage;
        int ASpeed;



        public Tier1Normal(Point position,MapArea map)
        {
           Map =map;
            Col = Color.Navy;
            Pos = position;
        }

        public MapArea mapArea { get { return Map; } }
       
        public Point Position
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
            spriteBatch.Draw(UILoader.ButtonTexture, new Rectangle((int)Position.X*30+(int)mapArea.DrawPos.X, (int)Position.Y*30+(int)mapArea.DrawPos.Y, 30, 30), Col);
        }
    }
}
