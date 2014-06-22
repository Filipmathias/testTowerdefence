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

    class UILoader 
    {
        public static Texture2D ButtonTexture;
        public static Texture2D ArrowTexture;
        public static Texture2D bgTexture;



        public static void Load() 
        {
            ButtonTexture = Game1.Instance.Content.Load<Texture2D>("Interface/buttontest");
            ArrowTexture = Game1.Instance.Content.Load<Texture2D>("Interface/Arrow");
            bgTexture = Game1.Instance.Content.Load<Texture2D>("Interface/bg");
        }


    }
    class UIHandler
    {
        public  Dictionary<string, IUserInterface> UIobjects = new Dictionary<string,IUserInterface>();

        public void Update(GameTime gametime) 
        {
            foreach (IUserInterface ui in UIobjects.Values) 
            {
                ui.Update(gametime);
            }
        }
        public void Draw(SpriteBatch spritebatch) 
        {
            foreach (IUserInterface ui in UIobjects.Values)
            {
                ui.Draw(spritebatch);
            }
        }

    }

    class ScreenModule:IModule
    {

        public IScreen Screen = new MenuScreen();

        void IModule.Load()
        {
            
        }

        void IModule.Update(Microsoft.Xna.Framework.GameTime gametime)
        {
            Screen.UI.Update(gametime);
            Screen.Update(gametime);
        }

        void IModule.Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            Screen.Draw(spriteBatch);
            Screen.UI.Draw(spriteBatch);
        }

        void IModule.Drop()
        {

        }
    }

    interface IScreen
    {
        UIHandler UI { get; }
        void Load();
        void Update(GameTime gametime);
        void Draw(SpriteBatch spriteBatch);
    }
    interface IUserInterface 
    {
        void Update(GameTime gametime);
        void Draw(SpriteBatch spriteBatch);
    }

    class MenuScreen : IScreen 
    {

        UIHandler ui = new UIHandler();
        public MenuScreen() 
        {
            ui.UIobjects.Add("StartButton", new ButtonSimple(new Vector2(400, 200), new Vector2(100, 40), "Start", Color.Green, Color.Black,1f));
            ui.UIobjects.Add("ExitButton", new ButtonSimple(new Vector2(400, 240), new Vector2(100, 40),"EXIT", Color.Red,Color.Black,1f));
            ui.UIobjects.Add("DoNotPressButton", new ButtonSimple(new Vector2(350, 280), new Vector2(200, 40), "PressThisFaggot", Color.Red, Color.Black, 1f));

        }

        UIHandler IScreen.UI
        {
            get
            {
                return ui;
            }
            
        }

        void IScreen.Load()
        {

        }
        bool FLASHY = false;
        void IScreen.Update(GameTime gametime)
        {
            if (((ButtonSimple)ui.UIobjects["ExitButton"]).WasPressed) 
            {
                Game1.Instance.Exit();
            }
            if (((ButtonSimple)ui.UIobjects["StartButton"]).WasPressed)
            {
                ((ScreenModule)Game1.Instance.Modules.Get("Screen")).Screen = new GameScreen();
            }
            if (((ButtonSimple)ui.UIobjects["DoNotPressButton"]).WasPressed)
            {
                ((ButtonSimple)ui.UIobjects["DoNotPressButton"]).BColor = Color.DeepPink;
                ((ButtonSimple)ui.UIobjects["StartButton"]).BColor = Color.DeepPink;
                ((ButtonSimple)ui.UIobjects["ExitButton"]).BColor = Color.DeepPink;
                FLASHY = true;
            }

            

        }

        void IScreen.Draw(SpriteBatch spriteBatch)
        {
            if(FLASHY){
                spriteBatch.Draw(UILoader.bgTexture,new Rectangle(0,0,1000,600),Color.White);
            }
        }
    }


    //InterfaceObjects
    class ButtonSimple:IUserInterface
    {

        bool wasPressed=false;
        bool isDown = false;
        public Vector2 Position { get; set; }
        public bool WasPressed { get { return wasPressed; } }
        public bool IsDown { get { return isDown; } }
        public Color BColor { get; set; }
        public Color TextColor{get;set;}
        public Vector2 Size { get; set; }
        public string Text { get; set; }
        public float TextScale { get; set; }
        public ButtonSimple(Vector2 position,Vector2 size, string text, Color Bcolor,Color textColor, float textScale) 
        {
            Position = position;
            Size = size;
            BColor = Bcolor;
            TextColor = textColor;
            Text = text;
            TextScale = textScale;

        }



        bool checkHover() 
        {
            return new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y).Contains(new Point(Game1.Instance.mouseState.X, Game1.Instance.mouseState.Y));
            
        
        }
        public void Update(GameTime gametime)
        {

            if (wasPressed) 
            {
                wasPressed = false;
            }
            MouseState ms = Game1.Instance.mouseState;
            if (isDown & ms.LeftButton == ButtonState.Released)
            {
                wasPressed = true;


            }
            if (ms.LeftButton == ButtonState.Pressed & checkHover())
            {
                isDown = true;

            }
            else 
            {
                isDown = false;
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector3 Col = BColor.ToVector3();
            if (checkHover()) 
            {
                Col = Col * 0.8f;
            
            }
            SpriteFont font = Game1.Instance.debugFont;
            

            spriteBatch.Draw(UILoader.ButtonTexture, new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y),new Color(Col));
            spriteBatch.DrawString(Game1.Instance.debugFont,Text,Position + new Vector2(4,4),TextColor,0,Vector2.Zero,TextScale,SpriteEffects.None,0);
               
        }
    }

    class MPointer : IModule
    {
        public Vector2 Size;
        public Color BColor;
        MouseState ms = new MouseState();
        public MPointer()
        {
            Size.X = 15;
            Size.Y = 15;
            BColor = Color.HotPink;
        }
        
        public void Load()
        {
           

        }

        public void Update(GameTime gametime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            ms = Game1.Instance.mouseState;

            spriteBatch.Draw(UILoader.ArrowTexture, new Rectangle((int)ms.X, (int)ms.Y, (int)Size.X, (int)Size.Y), BColor);
        }

        public void Drop()
        {

        }
    }


}
