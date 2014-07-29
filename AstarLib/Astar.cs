using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstarLib
{
    public class Astar
    {
        byte[] map;
        byte X;
        byte Y;
        
        public Astar(byte Y, byte X, byte[] Map,int Start,int End) 
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
