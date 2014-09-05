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



    public class Enemy //Normal Energy
    {
        Texture2D texture;
        MapArea Map;

        public Queue<Point> Path
        {
            get;
            set;
        }
        public bool Alive =true;
        Vector2 Pos { get; set; }
        float Health { get; set; }
        byte Armor { get; set; }
        float Speed { get; set; }
        int PathPos = 0;

        Point PointPos
        {
            get { return new Point((int)LocalPos.X / 30, (int)LocalPos.Y / 30); }
        }
        
        Vector2 LocalPos
        {
            get { return Pos - Map.DrawPos; }
        }

        public Enemy(float i)
        {
            Map = Game1.Instance.gameModule.map;
            Path = new Queue<Point>(new Astar().FindPath(Map.MapData, Game1.Instance.gameModule.map.Towers).pathPoints);

            if (Path.Count != 0)
            {
                Point posT = Path.Dequeue();
                Point tarT = Path.Dequeue();
                Pos = new Vector2((posT.X * 30) + (int)Map.DrawPos.X + 5, (posT.Y * 30) + (int)Map.DrawPos.Y + 5);
                target = new Vector2((tarT.X * 30) + (int)Map.DrawPos.X + 5, (tarT.Y * 30) + (int)Map.DrawPos.Y + 5);
            }

            Speed = 100;
        }


        virtual public  void Die() 
        {
            

        }


        public void Damage(int dmg)
        {
            Health = Health - ((float)dmg - (float)dmg * (float)Armor * 0.01f);
            if (Health <= 0)
            {
               
            }

        }

        Vector2 target;
        public void Update(GameTime gametime)
        {
            Vector2 t = target - Pos;
            t.X = Math.Sign(t.X);
            t.Y = Math.Sign(t.Y);

            if (gametime.ElapsedGameTime.TotalSeconds * Speed >= Vector2.Distance(target, Pos))
            {

                if (Path.Count != 0)
                {
                    float remaining = (float)(gametime.ElapsedGameTime.TotalSeconds * Speed - Vector2.Distance(target, Pos));
                    Pos = target;
                    Point p = Path.Dequeue();
                    target = new Vector2(p.X * 30 + (int)Map.DrawPos.X + 5, p.Y * 30 + (int)Map.DrawPos.Y + 5);
                }

            }
            else
            {
                Pos += t * (float)(Speed * gametime.ElapsedGameTime.TotalSeconds);
            }
        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(UILoader.circle, new Rectangle((int)Pos.X, (int)Pos.Y, 20, 20), Color.Black);
        }

        public void UpdatePath()
        {
            Path = new Queue<Point>(new Astar().FindPath(Map.MapData, Map.Towers, PointPos).pathPoints);
        }
    }
}
