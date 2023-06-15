using Labyrinth.Console.Obstacles;
using System;
using System.Collections.Generic;

namespace Labyrinth.Console.Extensions
{
    public static class GeneralExtensions
    {
        public static Coordinates CalculateNewCoordinates(this Coordinates playerCoords, ConsoleKeyInfo key)
        {
            Coordinates newCoords;
            if (key.Key == ConsoleKey.UpArrow)
                newCoords = playerCoords with { Y = playerCoords.Y - 1 };
            else if (key.Key == ConsoleKey.RightArrow)
                newCoords = playerCoords with { X = playerCoords.X + 1 };
            else if (key.Key == ConsoleKey.DownArrow)
                newCoords = playerCoords with { Y = playerCoords.Y + 1 };
            else if (key.Key == ConsoleKey.LeftArrow)
                newCoords = playerCoords with { X = playerCoords.X - 1 };
            else newCoords = playerCoords;

            return newCoords;
        }

        public static bool IsWithinBorders(this Coordinates coords, Playground playground, Dictionary<Coordinates, Obstacle> obstacles)
        {
            return coords.X >= 0 && coords.Y >= playground.SystemRows && coords.X < playground.Width && coords.Y < playground.Height && !obstacles.ContainsKey(coords);
        }
    }
}
