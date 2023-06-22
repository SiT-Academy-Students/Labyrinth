using Labyrinth.Console.Extensions;
using Labyrinth.Console.Obstacles;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Labyrinth.Console.Controllers
{
    public class DfsPathFinder : IPathFinder
    {
        private static int[] _directionsX = new int[] { 1, 0, 0, -1 };
        private static int[] _directionsY = new int[] { 0, 1, -1, 0 };

        private readonly Coordinates _start, _end;
        private readonly Playground _playground;
        private readonly bool _debug;

        public DfsPathFinder(Coordinates start, Coordinates end, Playground playground, bool debug)
        {
            this._start = start;
            this._end = end;
            this._playground = playground;
            this._debug = debug;
        }

        public bool SolutionExists(IDictionary<Coordinates, Obstacle> obstaclesMap)
        {
            HashSet<Coordinates> visited = new HashSet<Coordinates>();
            bool hasPath = this.Dfs(this._start, obstaclesMap, visited);
            
            if (this._debug)
            {
                foreach (var coordinate in visited)
                {
                    System.Console.SetCursorPosition(coordinate.X, coordinate.Y);
                    System.Console.Write(' ');
                    Thread.Sleep(0);
                }
            }

            return hasPath;
        }

        private bool Dfs(Coordinates current, IDictionary<Coordinates, Obstacle> obstaclesMap, HashSet<Coordinates> visited)
        {
            if (current == this._end) return true;

            visited.Add(current);
            
            if (this._debug)
            {
                System.Console.SetCursorPosition(current.X, current.Y);
                System.Console.Write('.');
                Thread.Sleep(0);
            }

            bool hasPath = false;
            for (int i = 0; i < 4 && !hasPath; i++)
            {
                Coordinates nextCoordinates = new Coordinates { X = current.X + _directionsX[i], Y = current.Y + _directionsY[i] };
                hasPath = !visited.Contains(nextCoordinates) && nextCoordinates.IsAvailable(this._playground, obstaclesMap) && Dfs(nextCoordinates, obstaclesMap, visited);
            }

            return hasPath;
        }
    }
}
