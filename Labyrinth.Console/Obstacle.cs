namespace Labyrinth.Console
{
    public class Obstacle
    {
        public Obstacle(int x, int y, ObstacleEdges edges)
        {
            X = x;
            Y = y;
            Edges = edges;
        }

        public int X { get; }
        public int Y { get; }
        public ObstacleEdges Edges { get; }
    }
}
