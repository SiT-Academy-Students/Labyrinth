using LabyrinthConsole;

namespace Labyrinth.Console
{
    public class Obstacle
    {
        public Obstacle(Coordinates coordinates, ObstacleEdges edges)
        {
            this.Coordinates = coordinates;
            this.Edges = edges;
        }

        public Coordinates Coordinates { get; }
        public ObstacleEdges Edges { get; }
    }
}
