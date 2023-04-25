using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthConsole
{
    public class Obstacle
    {
        public Obstacle(int positionX, int positionY, ObstacleEdges edges)
        {
            this.X = positionX;
            this.Y = positionY;
            this.Edges = edges;
        }
        public int X { get; }
        public int Y { get; }
        public ObstacleEdges Edges { get; }
    }
}
