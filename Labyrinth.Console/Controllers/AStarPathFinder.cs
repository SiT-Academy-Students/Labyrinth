using Labyrinth.Console.Extensions;
using Labyrinth.Console.Obstacles;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Labyrinth.Console.Controllers
{
    public class AStarPathFinder : IPathFinder
    {
        private static int[] _directionsX = new int[] { 1, 0, 0, -1 };
        private static int[] _directionsY = new int[] { 0, 1, -1, 0 };

        private readonly Coordinates _start, _end;
        private readonly Playground _playground;
        private readonly bool _debug;

        public AStarPathFinder(Coordinates start, Coordinates end, Playground playground, bool debug)
        {
            this._start = start;
            this._end = end;
            this._playground = playground;
            this._debug = debug;
        }

        public bool SolutionExists(IDictionary<Coordinates, Obstacle> obstaclesMap)
        {
            PriorityQueue<Coordinates, long> queue = new PriorityQueue<Coordinates, long>();
            queue.Enqueue(this._start, 1);

            HashSet<Coordinates> visited = new HashSet<Coordinates>();
            visited.Add(this._start);

            bool hasPath = false;
            while (queue.Count > 0)
            {
                Coordinates current = queue.Dequeue();

                for (int i = 0; i < 4; i++)
                {
                    Coordinates nextCoordinates = new Coordinates { X = current.X + _directionsX[i], Y = current.Y + _directionsY[i] };

                    if (!visited.Contains(nextCoordinates) && nextCoordinates.IsAvailable(this._playground, obstaclesMap))
                    {
                        queue.Enqueue(nextCoordinates, CalculateDistance(nextCoordinates));
                        visited.Add(nextCoordinates);

                        if (this._debug)
                        {
                            System.Console.SetCursorPosition(nextCoordinates.X, nextCoordinates.Y);
                            System.Console.Write('.');
                        }

                        if (nextCoordinates == this._end)
                        {
                            hasPath = true;
                            break;
                        }
                    }
                }
                
                if (hasPath) break;
            }

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

        private long CalculateDistance(Coordinates coordinate)
        {
            long xDiff = coordinate.X - this._end.X;
            long yDiff = coordinate.Y - this._end.Y;

            return xDiff * xDiff + yDiff * yDiff;
        }
    }
}
