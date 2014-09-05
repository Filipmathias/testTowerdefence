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
        List<Enemy> _activeEnemies = new List<Enemy>();
        private Queue<Enemy> _currentWave;

        public Queue<Wave> Waves
        {
            get { return _waves; }
            set { _waves = value; }
        }

        public List<Enemy> ActiveEnemiesEnemies
        {
            get { return _activeEnemies; }
            set { _activeEnemies = value; }
        }

        // write code for loading the Waves
        public WaveHandler(Wave[] waves)
        {
            _waves = new Queue<Wave>(waves);
            _currentWave = new Queue<Enemy>(ConvertEnemies(_waves.Dequeue().Enemies));
        }

        private List<Enemy> ConvertEnemies(List<EnemyInfo> input)
        {
            var result = new List<Enemy>();
            foreach (var enemyInfo in input)
            {
                result.Add(EnemyConverters()[enemyInfo.Type].Invoke(enemyInfo.level));
            }


            return result;
        }

        private double waveCD = 10000;
        private double enemyCD = 0;

        public void Update(GameTime game)
        {
            enemyCD -= game.ElapsedGameTime.Milliseconds;

            if (_currentWave.Count != 0)
            {
                if (enemyCD <= 0)
                {
                    _activeEnemies.Add(_currentWave.Dequeue());
                    enemyCD = 500;
                }
            }
            else
            {
                waveCD -= game.ElapsedGameTime.Milliseconds;
                if (waveCD <= 0)
                {
                    _currentWave = new Queue<Enemy>( ConvertEnemies(_waves.Dequeue().Enemies));
                    
                }

            }
            

           
        }

        public void Draw(SpriteBatch spritebat)
        {


        }
        public static Dictionary<string, Converter<float, Enemy>> EnemyConverters()
        {
            var list = new Dictionary<string, Converter<float, Enemy>>
            {
                {"Normal", new Converter<float, Enemy>((float e) => new Enemy(e))},
                {"Gert20", new Converter<float, Enemy>((float e) => new Enemy(e))}


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
