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


     class PathResult
     {
         public List<Point> pathPoints;
         public bool Success{ get;set;}
         public PathResult()
         {
            Success = false;
            pathPoints = new List<Point>();
         }
         public PathResult(List<Point> path)
         {
            Success = true;
            pathPoints =path;
         }



     }
     class Astar
     {

         List<Tile> ClosedList = new List<Tile>();
         List<Tile> OpenList = new List<Tile>();
         Tile[] Map = new Tile[400];
         Tile Start = null;
         Tile End = null;
         List<Point> getPath() 
         {
             
             List<Point> Result = new List<Point>();
             Tile Curr = End;
             
             while (true)
             {
                 Result.Add(Curr.position);
                 Curr = Curr.Parrent;

                 if (Curr == Start) 
                 {
                     break;
                 }

             }
             Result.Add(Start.position);
             Result.Reverse();
             return Result;
         }



        public PathResult FindPath(byte[] map, List<ITower> Towers,Point? startpoint = null) 
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
            



            foreach(ITower t in Towers)
            {
                Map[t.Position.X + t.Position.Y * 20] = new Tile(t.Position, Tile.TileType.UnWalkableTile);
            }
            if (startpoint != null)
            {
                Start = Map[startpoint.Value.X +startpoint.Value.Y * 20];
            }


            // adding starting point to the open List

            if (Start == null | End == null) 
            {
                return new PathResult();
            }

            
            Start.H = calcH(Start.position, End.position);
            OpenList.Add(Start);


            //starting loop
                while (true) 
                {
                    if (OpenList.Count == 0)
                    {
                        return new PathResult();
                    }
                    //get the best tile
                    Tile Best = null;

                    for (int i = 0; i < OpenList.Count ; i++)
                    {
                        if (Best != null) 
                        {
                            if (Best.F > OpenList[i].F)
                            {
                                Best = OpenList[i];
                            }
                        }
                        else 
                        {
                            Best = OpenList[i];
                        }

                    }   
                    //Move the Tile to the closed List
                    OpenList.Remove(Best);
                    ClosedList.Add(Best);
                    if (Best == End) 
                    {
                        return new PathResult(getPath());
                    }
                    // get the tiles ajecent to 

                    OpenList.AddRange(NextTiles(Best));
                }
            



        }

        List<Tile> NextTiles(Tile parrent) 
        {

            List<Tile> tiles = new List<Tile>();
            int index = parrent.position.Y * 20 + parrent.position.X;


            int newindex = index -1 ;
            //left
            if (TileValid(newindex,index)) 
            {

                //set the parrent of tile
                Map[newindex].Parrent = Map[index];
                //calculate G
                Map[newindex].G = Map[newindex].G + 1; 
                //calculate H
                Map[newindex].H = calcH(Map[newindex].position, End.position);
                // adding the tile
                tiles.Add(Map[newindex]);
            }
            //up
           
            newindex = index - 20;
            
            if (TileValid(newindex,index))
            {
                Map[newindex].Parrent = Map[index];
                Map[newindex].G = Map[newindex].G + 1;
                Map[newindex].H = calcH(Map[newindex].position, End.position);
                tiles.Add(Map[newindex]);
            }
            //right
           
            newindex = index + 1;
            
            if (TileValid(newindex,index))
            {
                Map[newindex].Parrent = Map[index];
                Map[newindex].G = Map[newindex].G + 1;
                Map[newindex].H = calcH(Map[newindex].position, End.position);
                tiles.Add(Map[newindex]);
            }
           
            //down
            newindex = index + 20;
            
            if (TileValid(newindex,index))
            {
                Map[newindex].Parrent = Map[index];
                Map[newindex].G = Map[newindex].G + 1;
                Map[newindex].H = calcH(Map[newindex].position, End.position);
                tiles.Add(Map[newindex]);
            }
             
            
            return tiles;

        }
        bool TileValid(int index, int parrent)
        {
            


            if (index > 399 || index < 0) 
            {
                return false;
            }

            if (Map[index].Type == Tile.TileType.UnWalkableTile) 
            {
                return false;
            }

            if (ClosedList.Contains(Map[index]))
            {
                return false;
            }

            if (OpenList.Contains(Map[index])) 
            {
                if (Map[parrent].G + 1 < Map[index].G) 
                {
                    Map[index].G += 1;
                    Map[index].Parrent = Map[parrent];
                    
                }
                return false;
            }

            
            return true;
        
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
            position = p;
           Type = type;
            
        }

        
    }


}
