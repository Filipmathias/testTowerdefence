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
    class UIGame
    {
        UIHandler UGame = new UIHandler();
        
    }

    class GameScreen: IScreen
    {
        UIHandler ui = new UIHandler();
        MenuSystem menu = new EmptyMenu();
        public GameScreen()
        {
            
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

        }

        public void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            menu.Draw(spriteBatch);
        }
    }
    interface MenuSystem
    {
        UIHandler UI { get; }
        void Load();
        void Update();
        void Draw(SpriteBatch spriteBatch);
    }

    class EmptyMenu : MenuSystem
    {

        UIHandler ui = new UIHandler();
        public EmptyMenu()
        {
            ui.UIobjects.Add("ExitButton", new ButtonSimple(new Vector2(0,500), new Vector2(75,20),"EXIT", Color.Red, Color.Black,0.5f));
            ui.UIobjects.Add("OptionsButton", new ButtonSimple(new Vector2(75, 500), new Vector2(75, 20), "Options", Color.Red, Color.Black,0.5f));
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

        public void Update()
        {
            if (((ButtonSimple)ui.UIobjects["ExitButton"]).WasPressed)
            {
                ((ScreenModule)Game1.Instance.Modules.Get("Screen")).Screen = new MenuScreen();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //Background
            spriteBatch.Draw(UILoader.ButtonTexture, new Rectangle(0, 0, 250,  75), Color.Silver);
            spriteBatch.DrawString(Game1.Instance.debugFont, "Race: ", new Vector2(4, 4), Color.Black);
            spriteBatch.DrawString(Game1.Instance.debugFont, "Gold: ", new Vector2(4, 35), Color.Gold);
            spriteBatch.Draw(UILoader.ButtonTexture, new Rectangle(0, 75, 250, 375), Color.White);
            spriteBatch.Draw(UILoader.ButtonTexture, new Rectangle(0, 450, 250, 150), Color.Silver);
            ui.Draw(spriteBatch);
        }
    }



}
