using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.GamerServices;

namespace TowerDefence
{
    class OptionsUI
    {

    }

    class OptionsScreen : IScreen
    {
        UIHandler ui = new UIHandler();
        public OptionsScreen()
        {
            ui.UIobjects.Add("BackButton", new ButtonSimple(new Vector2(400, 200), new Vector2(100, 40), "Back", Color.Green, Color.Black, 1f));
        }

        public UIHandler UI
        {
            get 
            {
                return ui;
            }
        }

        public void Load()
        {
        }

        public void Update(Microsoft.Xna.Framework.GameTime gametime)
        {
            if (((ButtonSimple)ui.UIobjects["BackButton"]).WasPressed)
            {
                ((ScreenModule)Game1.Instance.Modules.Get("Screen")).Screen = new MenuScreen();
            }
        }

        public void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            
        }
    }
}
