using Labyrinth.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Labyrinth.Console
{
    public class MapGenerator
    {
        MapGenerator() { }
        private int leftBorder => 33;
        private int rightBorder => System.Console.LargestWindowWidth - 20; //This should be able to be = to Console.LargestWindowWidth... 
        private int topBorder => Constants.systemRows;
        private int botBorder => System.Console.LargestWindowHeight - 6; //This should be able to be = to Console.LargestWindowHeight... 

        private int toptrue = 0;
        private int bottrue = 0;
        private int lefttrue = 0;
        private int righttrue = 0;


        public Obstacle generateMapBorders(int i, int j)
        {
            
            toptrue = 0;bottrue = 0;lefttrue = 0; righttrue = 0;
            if (i - 1 <= rightBorder) righttrue = 2;
            if (i + 1 >= leftBorder) lefttrue = 8;
            if (j - 1 <= topBorder) toptrue = 1;
            if (j + 1 >= botBorder) bottrue = 4;
            int whatEdge = 0;
            if(righttrue + lefttrue + toptrue + lefttrue > 0)
            {
                ObstacleEdges ObstacleEdges = (ObstacleEdges)(whatEdge);
                Coordinates ObstacleCoordinates = new Coordinates { X = i, Y = j };
                Obstacle currentObstacle = new Obstacle(ObstacleCoordinates, ObstacleEdges);
                return currentObstacle;
            }
            return null;
        }
    }
}
//Top = 1, ═
//Right = 2, ║
//Bottom = 4, ═
//Left = 8, ║