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
    static class Astar
    {
       static public List<Tile> FindPath(byte[] map, List<ITower> Towers) 
        {
            Point Start;
            Point End;

           byte[] SimpleMap = new byte[400];
            for (int i = 0; i < 400; i++)
            {

                
                if (map[i] == 10) 
                {
                    SimpleMap[i] = 0;
                }
                else if (map[i] == 0)
                {
                    SimpleMap[i] = 1;
                }
                else if (map[i] == 100)
                {
                    SimpleMap[i] = 2;
                }
                //check if StartingPoint
                else if (map[i] == 1)
                {
                    Start = new Point(i % 20, i / 20);

                }
                //check if EndPoint
                
                else if (map[i] == 255)
                {
                    End = new Point(i % 20, i / 20);

                }

            }
            foreach(ITower t in Towers)
            {
                SimpleMap[(int)(t.Position.Y * 20 + t.Position.X)] = 2;
            }

           

           List<Tile> OpenList  = new List<Tile>();
           List<Tile> ClosedList = new List<Tile>();




           return ClosedList;

        }
    }
    class Tile 
    {
        public int G{get; set;}
        public int H{get; set;}
        public int F
        {
            get{return G+H;}
            
        }
        public Point Position { get; set; }

        public Tile(int g, int h, Point P) 
        {
        
        }


        
    }


}
