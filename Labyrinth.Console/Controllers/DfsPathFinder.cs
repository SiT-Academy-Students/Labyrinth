using Labyrinth.Console.Extensions;
using Labyrinth.Console.Obstacles;
using System.Collections.Generic;
    
namespace Labyrinth.Console.Controllers
{
    public class DfsPathFinder : BasePathFinder, IPathFinder
    {
        public DfsPathFinder(Coordinates start, Coordinates end, Playground playground, IFlowController flowController)
            : base(start, end, playground, flowController)
        {
        }

        public override bool SolutionExists(IDictionary<Coordinates, Obstacle> obstaclesMap)
        {
            HashSet<Coordinates> visited = new HashSet<Coordinates>();
            bool hasPath = this.Dfs(this.Start, obstaclesMap, visited);
            
            this.ClearDebugInfo(visited);
            return hasPath;
        }

        private bool Dfs(Coordinates current, IDictionary<Coordinates, Obstacle> obstaclesMap, HashSet<Coordinates> visited)
        {
            if (current == this.End) return true;

            visited.Add(current);
            
            this.PrintDebugInfo(current, '.');
            bool hasPath = false;
            for (int i = 0; i < 4 && !hasPath; i++)
            {
                Coordinates nextCoordinates = new Coordinates { X = current.X + DirectionsX[i], Y = current.Y + DirectionsY[i] };
                hasPath = !visited.Contains(nextCoordinates) && nextCoordinates.IsAvailable(this.Playground, obstaclesMap) && Dfs(nextCoordinates, obstaclesMap, visited);
            }

            return hasPath;
        }
    }
}
