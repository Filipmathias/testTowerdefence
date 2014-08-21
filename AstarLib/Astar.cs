using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.GamerServices;
namespace AstarLib
{
    public class Astar
    {
        byte[] _map;
        int _width;
        int _height;
        int _start;
        int _end;


        public Astar(int width, int height, byte[] Map, int start, int end) 
        {
        
        }  

    }


    class Tile
    {
        public int G{get;set;}
        public int H{get;set;}
        public int F 
        { 
            get 
            {
                return G + H; 
            }

        }
        public Tile Parrent { get; set; }
    }


}
