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
     class Astar
    {



         List<Tile> ClosedList = new List<Tile>();
         List<Tile> OpenList = new List<Tile>();
         Tile[] Map = new Tile[400];
         Tile Start;
         Tile End;


        public void FindPath(byte[] map, List<ITower> Towers) 
        {
            //create TileMap
            for (int i = 0; i < 400; i++)
            {
                if (map[i] == 10) 
                {
                    Map[i] = new Tile(getPoint(i), Tile.TileType.UnWalkableTile);
                }
                else if (map[i] == 0) 
                {
                     Map[i] = new Tile(getPoint(i),Tile.TileType.WalkableTile); 
                }
                else if (map[i] == 1)
                {
                    Map[i] = new Tile(getPoint(i), Tile.TileType.StartTile);
                    Start = Map[i];
                        
                }
                else if (map[i] == 255)
                {
                    Map[i] = new Tile(getPoint(i), Tile.TileType.EndTile);
                    End = Map[i];
                }
           
                
                
            }

            // adding starting point to the open List
            OpenList.Add(Start);

            //starting loop
                while (true) 
                {
                    
                    //get the best tile
                    Tile Best = null;

                    for (int i = 0; i < OpenList.Count ; i++)
                    {
                        if (Best != null) 
                        {
                            if (Best.F < OpenList[i].F)
                            {
                                Best = OpenList[i];
                            }
                        }

                    }   
                    //Move the Tile to the closed List
                    OpenList.Remove(Best);
                    ClosedList.Add(Best);
                    


                }
            

        }

       static int calcH(Point tile ,Point endTile) 
       {
           return Math.Abs(endTile.X - tile.X) + Math.Abs(endTile.Y- tile.Y); 

       }

       static Point getPoint(int tileindex) 
       {
           return new Point(tileindex % 20, tileindex / 20);
       }
        

       



    }
    class Tile 
    {
        public enum TileType
	    {
	         StartTile,
             EndTile,
             WalkableTile,
             UnWalkableTile
    	}
        public int G{get; set;}
        public int H{get; set;}
        public TileType Type {get; set;}
        public Point position { get; set; }
        public int F
        {
            get{return G+H;}
            
        }
        public Tile Parrent { get; set; }


        public Tile(Point p,TileType type)
        {
            
            
        }

        
    }


}
