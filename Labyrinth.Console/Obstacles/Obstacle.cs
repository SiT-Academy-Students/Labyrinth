namespace Labyrinth.Console.Obstacles
{
    public class Obstacle
    {
        public Obstacle(Coordinates coordinates, ObstacleEdges edges)
        {
            Coordinates = coordinates;
            Edges = edges;
        }

        public Coordinates Coordinates { get; }
        public ObstacleEdges Edges { get; }
    }
}
