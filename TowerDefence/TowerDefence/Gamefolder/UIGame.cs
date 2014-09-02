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

   public class GameScreen: IScreen
    {
        UIHandler ui = new UIHandler();
        public MenuSystem menu = new EmptyMenu();
        public MapArea map = new MapArea(new Vector2(300,0));
       
        public GameScreen()
        {
            ui.UIobjects.Add("ExitButton", new ButtonSimple(new Vector2(0, 500), new Vector2(75, 20), "EXIT", Color.Red, Color.Black, 0.5f));
            ui.UIobjects.Add("OptionsButton", new ButtonSimple(new Vector2(75, 500), new Vector2(75, 20), "Options", Color.Red, Color.Black, 0.5f));
            ui.UIobjects.Add("ShopButton", new ButtonSimple(new Vector2(150, 500), new Vector2(75, 20), "Shop", Color.Gold, Color.Black, 0.5f));
            
     
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
            if (((ButtonSimple)ui.UIobjects["ExitButton"]).WasPressed)
            {
                ((ScreenModule)Game1.Instance.Modules.Get("Screen")).Screen = new MenuScreen();
            }
            if (((ButtonSimple)ui.UIobjects["OptionsButton"]).WasPressed)
            {
                menu = new OptionsMenu();
            }
            if (((ButtonSimple)ui.UIobjects["ShopButton"]).WasPressed)
            {
                menu = new ShopMenu();
            }
            if (Game1.Instance.keyState.IsKeyDown(Keys.Escape) == true)
            {
                menu = new EmptyMenu();
            }
            ui.Update(gametime);
            menu.UI.Update(gametime);
            menu.Update(gametime);
            map.Update(gametime);
            if (map.WasPressed) 
            {
                if (menu is ShopMenu)
                {
                    if(((ShopMenu)menu).BuyTower == 101)
                    { 
                        map.AddTower(new Tier1Normal(map.Squarepressed,map),map.Squarepressed);
                    }    
                }
            }


        }

        public void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            //Background
            spriteBatch.Draw(UILoader.ButtonTexture, new Rectangle(0, 0, 250, 75), Color.Silver);
            spriteBatch.DrawString(Game1.Instance.debugFont, "Race: ", new Vector2(4, 4), Color.Black);
            spriteBatch.DrawString(Game1.Instance.debugFont, "Gold: ", new Vector2(4, 35), Color.Gold);
            spriteBatch.Draw(UILoader.ButtonTexture, new Rectangle(0, 75, 250, 375), Color.White);
            spriteBatch.Draw(UILoader.ButtonTexture, new Rectangle(0, 450, 250, 150), Color.Silver);
            ui.Draw(spriteBatch);
            menu.Draw(spriteBatch);
            map.Draw(spriteBatch);


        }
    }
    public interface MenuSystem
    {
        UIHandler UI { get; }
        void Update(GameTime gametime);
        void Draw(SpriteBatch spriteBatch);
    }

    class EmptyMenu : MenuSystem
    {

        UIHandler ui = new UIHandler();
        public EmptyMenu()
        {
            
        }
        public UIHandler UI
        {
            get
            {
                return ui;
            }
        }

        public void Update(GameTime gametime)
        {
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
        }
    }
    class Tower1Norm: MenuSystem
    {
        UIHandler ui = new UIHandler();
        public Tower1Norm()
        {
            ui.UIobjects.Add("Upgrade", new ButtonSimple(new Vector2(4, 80), new Vector2(80, 20), "Upgrade Tower", Color.SeaGreen, Color.Black, 0.5f));
        }

        public UIHandler UI
        {
            get 
            {
                return ui;
            }
        }

        public void Update(GameTime gametime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(UILoader.ButtonTexture, new Rectangle(0, 75, 250, 375), Color.Khaki);
            ui.Draw(spriteBatch);
        }
    }
    class ShopMenu: MenuSystem
    {
        public int BuyTower = 0;
        UIHandler ui = new UIHandler();
        public ShopMenu()
        {
            ui.UIobjects.Add("NTower1", new ButtonSimple(new Vector2(4, 100), new Vector2(50, 50), " ", Color.Navy, Color.Black, 0.5f));
            ui.UIobjects.Add("Deselect", new ButtonSimple(new Vector2(4, 420), new Vector2(90, 20), "Deselect Tower", Color.LightBlue, Color.Black, 0.5f));
        }
        public UIHandler UI
        {
            get 
            {
                return ui;
            }
        }

        public void Update(GameTime gametime)
        {
            if (((ButtonSimple)ui.UIobjects["NTower1"]).WasPressed)
            {
                BuyTower = 101;
            }
            if (((ButtonSimple)ui.UIobjects["Deselect"]).WasPressed)
            {
                BuyTower = 0;
            }
            if (Game1.Instance.keyState.IsKeyDown(Keys.Escape) == true)
            {
                BuyTower = 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(UILoader.ButtonTexture, new Rectangle(0, 75, 250, 375), Color.RosyBrown);
            spriteBatch.DrawString(Game1.Instance.debugFont, "Shop", new Vector2(4, 75), Color.White, 0, Vector2.Zero, 0.8f, SpriteEffects.None, 0);
            ui.Draw(spriteBatch);
        }
    }
    class OptionsMenu: MenuSystem
    {
        UIHandler ui = new UIHandler();
        public UIHandler UI
        {
            get 
            {
                return ui;
            }
        }

        public void Update(GameTime gametime)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(UILoader.ButtonTexture, new Rectangle(0, 75, 250, 375), Color.Black);
            spriteBatch.DrawString(Game1.Instance.debugFont, "Options", new Vector2(4, 80), Color.White, 0, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
        }
    }



}
