namespace Labyrinth.Console
{
    public class Obstacle
    {
        // Each obstacle must have a position - x, y

        // Instead of four variables, use flag enumeration
        bool HasTopEdge { get; set; }
        bool HasRightEdge { get; set; }
        bool HasBottomEdge { get; set; }
        bool HasLeftEdge { get; set; }
    }
}
