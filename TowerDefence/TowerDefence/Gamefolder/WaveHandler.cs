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
    
    
    
    public class WaveHandler
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

        public void Draw(SpriteBatch spritebat)
        {


        }
       public static Dictionary<string,Converter<int,Enemy>> EnemyConverters()
        {
            Dictionary<string, Converter<int, Enemy>> list = new Dictionary<string, Converter<int, Enemy>>();
            
            list.Add("Normal",new Converter<int,Enemy>((int e)=> new Enemy(e)));
            return list;

        }
    
    
    }
    public class Wave
    {

        public List<EnemyInfo> Enemies { get; set; }
        public Wave(){Enemies = new List<EnemyInfo>();}
    }

    public class EnemyInfo
    {

        public string Type { get; set; }
        public float level { get; set; }
    
    }


}
