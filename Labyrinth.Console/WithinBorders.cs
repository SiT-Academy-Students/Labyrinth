using LabyrinthConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinth.Console
{
    public static class WithinBorders
    {
        public static bool IsWithinBorders(this Coordinates coords, HashSet<Coordinates> bCoords)
        {
            Playground playground = new Playground();
            if (coords.X >= 0 && coords.Y >= playground.SystemRows && coords.X < playground.Width && coords.Y < playground.Height && !bCoords.Contains(coords))
            {
                return true;
            }
            return false;
        }
    }
}
