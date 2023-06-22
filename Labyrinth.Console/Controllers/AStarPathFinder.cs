using Labyrinth.Console.Extensions;
using Labyrinth.Console.Obstacles;
using System.Collections.Generic;

namespace Labyrinth.Console.Controllers
{
    public class AStarPathFinder : BasePathFinder, IPathFinder
    {
        public AStarPathFinder(Coordinates start, Coordinates end, Playground playground, IFlowController flowController)
            : base(start, end, playground, flowController)
        {
        }

        public override bool SolutionExists(IDictionary<Coordinates, Obstacle> obstaclesMap)
        {
            PriorityQueue<Coordinates, long> queue = new PriorityQueue<Coordinates, long>();
            queue.Enqueue(this.Start, 1);

            HashSet<Coordinates> visited = new HashSet<Coordinates>();
            visited.Add(this.Start);

            bool hasPath = false;
            while (queue.Count > 0)
            {
                Coordinates current = queue.Dequeue();

                for (int i = 0; i < 4; i++)
                {
                    Coordinates nextCoordinates = new Coordinates { X = current.X + DirectionsX[i], Y = current.Y + DirectionsY[i] };

                    if (!visited.Contains(nextCoordinates) && nextCoordinates.IsAvailable(this.Playground, obstaclesMap))
                    {
                        queue.Enqueue(nextCoordinates, CalculateDistance(nextCoordinates));
                        visited.Add(nextCoordinates);

                        this.PrintDebugInfo(nextCoordinates, '.');

                        if (nextCoordinates == this.End)
                        {
                            hasPath = true;
                            break;
                        }
                    }
                }

                if (hasPath) break;
            }

            this.ClearDebugInfo(visited);
            return hasPath;
        }

        private long CalculateDistance(Coordinates coordinate)
        {
            long xDiff = coordinate.X - this.End.X;
            long yDiff = coordinate.Y - this.End.Y;

            return xDiff * xDiff + yDiff * yDiff;
        }
    }
}
