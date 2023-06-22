using Labyrinth.Console.Extensions;
using Labyrinth.Console.Obstacles;
using System.Collections.Generic;

namespace Labyrinth.Console.Controllers
{
    public class BfsPathFinder : BasePathFinder, IPathFinder
    {
        public BfsPathFinder(Coordinates start, Coordinates end, Playground playground, IFlowController flowController)
            : base(start, end, playground, flowController)
        {
        }

        public override bool SolutionExists(IDictionary<Coordinates, Obstacle> obstaclesMap)
        {
            Queue<Coordinates> queue = new Queue<Coordinates>();
            queue.Enqueue(this.Start);

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
                        queue.Enqueue(nextCoordinates);
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
    }
}
