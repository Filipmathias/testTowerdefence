using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.GamerServices;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;

namespace TowerDefence
{
    class PathFindingTester : IScreen
    {

        //map
        ButtonSimple[] buttonArray = new ButtonSimple[400];
        byte[] mapData = new byte[400];
        
        //Load
        ButtonSimple LoadButton = new ButtonSimple(new Vector2(5, 5), new Vector2(100, 40), "Load", Color.Blue, Color.Black, 1f);
        //Test
        ButtonSimple TestButton = new ButtonSimple(new Vector2(5, 45), new Vector2(100, 40), "Test", Color.Green, Color.Black, 1f);
        //Back
        ButtonSimple BackButton = new ButtonSimple(new Vector2(5, 85), new Vector2(100, 40), "Back", Color.Red, Color.Yellow, 1f);


        public PathFindingTester() 
        {
            for (int i = 0; i < 400; i++)
            {
                buttonArray[i] = new ButtonSimple(new Vector2((400 + i * 30) - (i / 20 * 600), (i / 20) * 30), new Vector2(30, 30), "0", getColor(0), Color.Black, 0.8f);

            }
        
        }

        UIHandler IScreen.UI
        {
            get { return new UIHandler(); }
        }

        void IScreen.Load()
        {
            throw new NotImplementedException();
        }

        void IScreen.Update(GameTime gametime)
        {
            TestButton.Update(gametime);
            BackButton.Update(gametime);
            if (BackButton.WasPressed)
            {
                ((ScreenModule)Game1.Instance.Modules.Get("Screen")).Screen = new MenuScreen();
            }
            if (TestButton.WasPressed)
            {
                PathFindingLogic();
            }

            LoadButton.Update(gametime);
            if (LoadButton.WasPressed)
            {
                LoadFile();
            }



        }

        void IScreen.Draw(SpriteBatch spriteBatch)
        {
            //background
            spriteBatch.Draw(UILoader.ButtonTexture, new Rectangle(0, 0, 250, 75), Color.Silver);
            spriteBatch.Draw(UILoader.ButtonTexture, new Rectangle(0, 75, 250, 375), Color.White);
            spriteBatch.Draw(UILoader.ButtonTexture, new Rectangle(0, 450, 250, 150), Color.Silver);

            LoadButton.Draw(spriteBatch);
            BackButton.Draw(spriteBatch);
            TestButton.Draw(spriteBatch);

            foreach (var bs in buttonArray)
            {
                bs.Draw(spriteBatch);
            }

        }
        





        void PathFindingLogic()
        {
            var timer = new Stopwatch();
            // timer started;
            timer.Start();
 


            timer.Stop();
            MessageBox.Show("time: " + timer.Elapsed.TotalMilliseconds + " ms");


        }

        void LoadFile()
        {
            var LFD = new OpenFileDialog();
            LFD.InitialDirectory = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\maps";
            LFD.Multiselect = false;
            if (LFD.ShowDialog() == DialogResult.OK)
            {
                mapData = File.ReadAllBytes(LFD.FileName);
                for (int i = 0; i < 400; i++)
                {
                    buttonArray[i] = new ButtonSimple(new Vector2((400 + i * 30) - (i / 20 * 600), (i / 20) * 30), new Vector2(30, 30), mapData[i].ToString(), getColor(mapData[i]), Color.Black, 0.8f);

                }

            }
        }

        Color getColor(byte b)
        {
            switch (b)
            {
                case 0: return Color.LightSlateGray;
                case 1: return Color.LimeGreen;
                case 255: return Color.Orange;
                case 10: return Color.Brown;
                default: return Color.White;

            }

        }
    }
}
