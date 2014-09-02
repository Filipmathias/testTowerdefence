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
    
    
    
    class WaveHandler
    {
        Queue<Wave> _Waves = new Queue<Wave>();
        List<Enemy> _Active = new List<Enemy>();

        public Queue<Wave> Waves
        {
            get { return _Waves; }
            set { _Waves = value; }
        }
  
        public List<Enemy> ActiveEnemies 
        {
            get { return _Active; } 
            set {_Active = value; } 
        }

        Wave _current;

        
        // write code for loading the Waves
        public WaveHandler() 
        {


        }

        public void Update(GameTime game)
        {
            


        }
        //static List<object> Constructors() 
        //{
        //    Dictionary<string, Converter<int,Enemy>> Actions = new Dictionary<string,Converter<int,Enemy>>();
  


        //}

        public void Draw(SpriteBatch spritebat) 
        {
        
        }
    }

    class Wave
    {
    
    }


}
