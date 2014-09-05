using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.GamerServices;
using Newtonsoft.Json;

namespace TowerDefence
{

    public class WaveHandler
    {
        Queue<Wave> _waves = new Queue<Wave>();
        List<Enemy> _active = new List<Enemy>();
        private Queue<Wave> _currentWave;

        public Queue<Wave> Waves
        {
            get { return _waves; }
            set { _waves = value; }
        }

        public List<Enemy> ActiveEnemies
        {
            get { return _active; }
            set { _active = value; }
        }

        // write code for loading the Waves
        public WaveHandler(Wave[] waves)
        {
            _waves = new Queue<Wave>(waves);
        }

        private List<Enemy> ConvertEnemies( enemu input)
        {
        }

        private double waveCD = 0;
        private double enemyCD = 0;

        public void Update(GameTime game)
        {
            if (enemyCD <= 0)
            {
               

            }


        }

        public void Draw(SpriteBatch spritebat)
        {


        }
        public static Dictionary<string, Converter<int, Enemy>> EnemyConverters()
        {
            var list = new Dictionary<string, Converter<int, Enemy>>
            {
                {"Normal", new Converter<int, Enemy>((int e) => new Enemy(e))}

            };

            return list;

        }

        public class WaveData
        {
            public string Map { get; set; }
            public Wave[] Waves { get; set; }
        }
    }
    public class Wave
    {

        public List<EnemyInfo> Enemies { get; set; }
        public Wave() { Enemies = new List<EnemyInfo>(); }
    }

    public class EnemyInfo
    {

        public string Type { get; set; }
        public float level { get; set; }

    }


}
