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
    class MapEditor:IScreen
    {
        byte[] mapData = new byte[400];
        ButtonSimple ButtonFill = new ButtonSimple(new Vector2(5, 5), new Vector2(100, 40),"Fill", Color.Green, Color.Black, 1f);  

        UIHandler IScreen.UI
        {
            get { return new UIHandler(); }
        }

        void IScreen.Load()
        {


        }

        void IScreen.Update(GameTime gametime)
        {
            ButtonFill.Update(gametime);

        }

        void IScreen.Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(UILoader.ButtonTexture, new Rectangle(0, 0, 250, 75), Color.Silver);
            spriteBatch.Draw(UILoader.ButtonTexture, new Rectangle(0, 75, 250, 375), Color.White);
            spriteBatch.Draw(UILoader.ButtonTexture, new Rectangle(0, 450, 250, 150), Color.Silver);

            ButtonFill.Draw(spriteBatch);
        }
    }



}
