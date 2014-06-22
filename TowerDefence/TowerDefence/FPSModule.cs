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
    class FPSModule:IModule
    {

        void IModule.Load()
        {
            
        }
        int Fpsshow = 0;
        int Fps = 0;
        Double time=0;
        void IModule.Update(GameTime gametime)
        {
            Fps++;
            time += gametime.ElapsedGameTime.TotalMilliseconds;
            if (time >= 1000) 
            {
                Fpsshow = Fps;
                Fps = 0;
                time = 0;
            }
        }

        void IModule.Draw(SpriteBatch spriteBatch)
        {
            if(Game1.Instance.keyState.IsKeyDown(Keys.O))
            {
                spriteBatch.DrawString(Game1.Instance.debugFont,Fpsshow.ToString(),new Vector2(0,0),Color.Red);
            }
        }

        void IModule.Drop()
        {
            
        }
    }
}
