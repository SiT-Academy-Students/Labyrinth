using Labyrinth.Console.Extensions;
using Labyrinth.Console.Obstacles;
using System.Collections.Generic;
using System.Threading;

namespace Labyrinth.Console.Controllers
{
    public class DfsPathFiner : IPathFinder
    {
        private static int[] _directionsX = new int[] { 1, 0, 0, -1 };
        private static int[] _directionsY = new int[] { 0, 1, -1, 0 };

        private readonly Coordinates _start, _end;
        private readonly Playground _playground;

        public DfsPathFiner(Coordinates start, Coordinates end, Playground playground)
        {
            this._start = start;
            this._end = end;
            this._playground = playground;
        }

        public bool SolutionExists(IDictionary<Coordinates, Obstacle> obstaclesMap)
        {
            HashSet<Coordinates> visited = new HashSet<Coordinates>();
            return this.Dfs(this._start, obstaclesMap, visited);
        }

        private bool Dfs(Coordinates current, IDictionary<Coordinates, Obstacle> obstaclesMap, HashSet<Coordinates> visited)
        {
            if (current == this._end) return true;

            visited.Add(current);
            
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
