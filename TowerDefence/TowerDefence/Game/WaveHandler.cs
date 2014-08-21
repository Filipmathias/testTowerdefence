using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.GamerServices;

namespace TowerDefence.Game
{
    class WaveHandler
    {
        Queue<Wave> _Waves = new Queue<Wave>();
        public Queue<Wave> Waves
        {
            get { return _Waves; }
            set { _Waves = value; }
        }

        Wave _current;
        public void WaveHandler() 
        {

        }



    }
    class Wave
    {
    
    }


}
