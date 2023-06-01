using System;

namespace Labyrinth.Console.Obstacles
{
    [Flags]
    public enum ObstacleEdges
    {
        None = 0,
        Top = 1,
        Right = 2,
        Bottom = 4,
        Left = 8
    }
}
