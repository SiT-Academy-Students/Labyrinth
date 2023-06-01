using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Labyrinth.Console.Obstacles;

namespace Labyrinth.Console.PlayerMovements
{
    public static class WithinBorders
    {
        public static bool IsWithinBorders(this Coordinates coords, Dictionary<Coordinates, Obstacle> obstacle)
        {
            Playground playground = new Playground();
            if (coords.X >= 0 && coords.Y >= playground.SystemRows.Y && coords.X < playground.Width.X && coords.Y < playground.Height.Y && !obstacle.ContainsKey(coords))
            {
                return true;
            }
            return false;
        }
    }
}