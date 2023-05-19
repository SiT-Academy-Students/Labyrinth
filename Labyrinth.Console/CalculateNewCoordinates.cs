using LabyrinthConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinth.Console
{
    public static class CalculateCoords
    {
        public static Coordinates CalculateNewCoordinates(this Coordinates playerCoords, ConsoleKeyInfo key)
        {
            Coordinates newCoords = new Coordinates();
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
    }
}
