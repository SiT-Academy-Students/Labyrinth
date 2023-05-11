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
        readonly private int leftBorder = 33;
        readonly private int rightBorder = 220; //This should be able to be = to Console.LargestWindowWidth... 
        readonly private int topBorder = Constants.systemRows;
        readonly private int botBorder = 57; //This should be able to be = to Console.LargestWindowHeight... 

        private int toptrue = 0;
        private int bottrue = 0;
        private int lefttrue = 0;
        private int righttrue = 0;


        public Obstacle generateMapBorders(int i, int j)
        {
            
            toptrue = 0;bottrue = 0;lefttrue = 0; righttrue = 0;
            if (i - 1 <= rightBorder) righttrue = 1;
            if (i + 1 >= leftBorder) lefttrue = 1;
            if (j - 1 <= topBorder) toptrue = 1;
            if (j + 1 >= botBorder) bottrue = 1;

            ObstacleEdges ObstacleEdges = (ObstacleEdges)(righttrue+lefttrue+toptrue+lefttrue);

            Coordinates ObstacleCoordinates = new Coordinates { X = i, Y = j };
            Obstacle currentObstacle = new Obstacle(ObstacleCoordinates, ObstacleEdges);
            return currentObstacle;
        }
    }
}
//Top = 1, ═
//Right = 2, ║
//Bottom = 4, ═
//Left = 8, ║