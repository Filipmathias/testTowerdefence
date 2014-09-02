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
            ui.UIobjects.Add("EditorButton", new ButtonSimple(new Vector2(400, 200), new Vector2(100, 40), "Editor", Color.Green, Color.Black, 1f));
            ui.UIobjects.Add("BackButton", new ButtonSimple(new Vector2(400, 280), new Vector2(100, 40), "Back", Color.Yellow, Color.Red, 1f));
            ui.UIobjects.Add("PathButton", new ButtonSimple(new Vector2(400, 240), new Vector2(100, 40), "PathingTester", Color.Yellow, Color.Red, 1f));
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
            
            if (((ButtonSimple)ui.UIobjects["EditorButton"]).WasPressed)
            {
                ((ScreenModule)Game1.Instance.Modules.Get("Screen")).Screen = new MapEditor();
            }
          
            if (((ButtonSimple)ui.UIobjects["PathButton"]).WasPressed)
            {
                ((ScreenModule)Game1.Instance.Modules.Get("Screen")).Screen = new PathFindingTester();
            }

        }

        public void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            
        }
    }
}
