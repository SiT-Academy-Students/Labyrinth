using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthConsole
{
    public class Obstacle : Coordinates
    {
        public Obstacle(int positionX, int positionY, ObstacleEdges edges)
        {
            this.PositionX = positionX;
            this.PositionY = positionY;
            this.Edges = edges;
        }
        public int PositionX { get; }
        public int PositionY { get; }
        public ObstacleEdges Edges { get; }
    }
}
