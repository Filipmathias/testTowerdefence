using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.GamerServices;
using System.IO;
using System.Windows.Forms;

namespace TowerDefence
{
    class MapEditor:IScreen
    {
        byte[] mapData = new byte[400];
        
        //Fill?? YES!
        ButtonSimple ButtonFill = new ButtonSimple(new Vector2(5, 5), new Vector2(100, 40),"Fill", Color.Green, Color.Black, 1f);

        //Save
        ButtonSimple SaveButton = new ButtonSimple(new Vector2(5, 45), new Vector2(100, 40), "Save", Color.Green, Color.Black, 1f);

        //Load
        ButtonSimple LoadButton = new ButtonSimple(new Vector2(5, 85), new Vector2(100, 40), "Load", Color.Blue, Color.Black, 1f);  
        
        //Back
        ButtonSimple BackButton = new ButtonSimple(new Vector2(5, 125), new Vector2(100, 40), "Back", Color.Red, Color.Yellow, 1f);

        //ones
        ButtonSimple I1Button = new ButtonSimple(new Vector2(200, 500), new Vector2(50, 20), "1", Color.Green, Color.Black, 0.5f);
        ButtonSimple D1Button = new ButtonSimple(new Vector2(0, 500), new Vector2(50, 20), "1", Color.Red, Color.Black, 0.5f);
       
        //tens
        ButtonSimple I10Button = new ButtonSimple(new Vector2(200, 520), new Vector2(50, 20), "10", Color.Green, Color.Black, 0.5f);
        ButtonSimple D10Button = new ButtonSimple(new Vector2(0, 520), new Vector2(50, 20), "10", Color.Red, Color.Black, 0.5f);
      
        //hundreds
        ButtonSimple I100Button = new ButtonSimple(new Vector2(200, 540), new Vector2(50, 20), "100", Color.Green, Color.Black, 0.5f);
        ButtonSimple D100Button = new ButtonSimple(new Vector2(0, 540), new Vector2(50, 20), "100", Color.Red, Color.Black, 0.5f);
        
        //map
        ButtonSimple[] buttonArray = new ButtonSimple[400];

        UIHandler IScreen.UI
        {
            get { return new UIHandler(); }
        }

        void IScreen.Load()
        {
        }

        public MapEditor() 
        {
            for (int i = 0; i < 400; i++)
            {
                buttonArray[i] = new ButtonSimple(new Vector2((400 + i * 30) - (i / 20 * 600), (i / 20) * 30), new Vector2(30, 30), "0", getColor(0), Color.Black, 0.8f);
            }
        }
        byte SelectedValue = 0;

        void IScreen.Update(GameTime gametime)
        {
            //button updates
            // map
            for (int i = 0; i < 400; i++)
            {
                buttonArray[i].Update(gametime);
                if (buttonArray[i].IsDown)
                {

                    mapData[i] = SelectedValue;
                    buttonArray[i].Text = SelectedValue.ToString();
                    buttonArray[i].BColor = getColor(SelectedValue);
                }
            }


    
            //1
            I1Button.Update(gametime);
            if (I1Button.WasPressed) 
            {
                SelectedValue++;
            }
            
            D1Button.Update(gametime);
            if (D1Button.WasPressed)
            {
                SelectedValue--;
            }
            
            //10
            I10Button.Update(gametime);
            if (I10Button.WasPressed)
            {
                SelectedValue += 10;
            }
            D10Button.Update(gametime);
            if (D10Button.WasPressed)
            {
                SelectedValue-=10;
            }
            
            //100
            I100Button.Update(gametime);
            if (I100Button.WasPressed)
            {
                SelectedValue += 100;
            }
            D100Button.Update(gametime);
            if (D100Button.WasPressed)
            {
                SelectedValue -= 100;
            }

            //Load
            LoadButton.Update(gametime);
            if (LoadButton.WasPressed)
            {
                LoadFile();
            }


            //Save
            SaveButton.Update(gametime);
            if (SaveButton.WasPressed) 
            {
                SaveFile();
            }
            //Back
            BackButton.Update(gametime);
            if (BackButton.WasPressed)
            {
                ((ScreenModule)Game1.Instance.Modules.Get("Screen")).Screen =new MenuScreen() ;
            }
            //Fill
            ButtonFill.Update(gametime);
            if (ButtonFill.WasPressed)
            {
                for (int i = 0; i < 400; i++)
                {
                    mapData[i] = SelectedValue;
                    buttonArray[i] = new ButtonSimple(new Vector2((400 + i * 30) - (i / 20 * 600), (i / 20) * 30), new Vector2(30, 30),SelectedValue.ToString(), getColor(SelectedValue), Color.Black, 0.8f);

                }
            }


        }

        void IScreen.Draw(SpriteBatch spriteBatch)
        {
            //background
            spriteBatch.Draw(UILoader.ButtonTexture, new Rectangle(0, 0, 250, 75), Color.Silver);
            spriteBatch.Draw(UILoader.ButtonTexture, new Rectangle(0, 75, 250, 375), Color.White);
            spriteBatch.Draw(UILoader.ButtonTexture, new Rectangle(0, 450, 250, 150), Color.Silver);


            //buttons
            //fill 
            ButtonFill.Draw(spriteBatch);
            //load
            LoadButton.Draw(spriteBatch);
            
            //Save 
            SaveButton.Draw(spriteBatch);
        
            //Back
            BackButton.Draw(spriteBatch);
        
    

            //1
            I1Button.Draw(spriteBatch);
            D1Button.Draw(spriteBatch);
            //10
            I10Button.Draw(spriteBatch);
            D10Button.Draw(spriteBatch);
            //100
            I100Button.Draw(spriteBatch);
            D100Button.Draw(spriteBatch);
            
            //text 
            spriteBatch.DrawString(Game1.Instance.debugFont, SelectedValue.ToString(), new Vector2(100, 500), Color.Black);
           
            
            foreach (var bs in buttonArray)
            {
                bs.Draw(spriteBatch);
            }
        }

        void SaveFile()
        {

            var SFD = new SaveFileDialog();
            SFD.AddExtension = true;
            SFD.DefaultExt = ".TDM";
            SFD.InitialDirectory= System.IO.Path.GetDirectoryName(Application.ExecutablePath)+"\\maps";
            

            if (SFD.ShowDialog() == DialogResult.OK) 
            {
                
                File.WriteAllBytes(SFD.FileName, mapData);

            }

        }


        void LoadFile() 
        {
            var LFD = new OpenFileDialog();
            LFD.InitialDirectory = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\maps";
            LFD.Multiselect = false;
            if (LFD.ShowDialog()== DialogResult.OK) 
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
