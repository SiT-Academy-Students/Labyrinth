using Labyrinth.Console.Obstacles;
using System.Collections.Generic;
using System.Threading;

namespace Labyrinth.Console.Controllers
{
    public abstract class BasePathFinder : IPathFinder
    {
        protected static int[] DirectionsX = { 1, 0, 0, -1 };
        protected static int[] DirectionsY = { 0, 1, -1, 0 };
        
        private readonly IFlowController _flowController;

        protected Coordinates Start { get; }
        protected Coordinates End { get; }
        protected Playground Playground { get; }

        protected BasePathFinder(Coordinates start, Coordinates end, Playground playground, IFlowController flowController = null)
        {
            this.Start = start;
            this.End = end;
            this.Playground = playground;
            this._flowController = flowController;
        }

        public abstract bool SolutionExists(IDictionary<Coordinates, Obstacle> obstaclesMap);

        protected void ClearDebugInfo(IEnumerable<Coordinates> coordinates)
        {
            if (this._flowController is null) return;
            foreach (var coordinate in coordinates) this.PrintDebugInfo(coordinate, ' ');
        }

        protected void PrintDebugInfo(Coordinates coordinates, char symbol)
        {
            if (this._flowController is null) return;

            this._flowController.Render(coordinates, symbol);
            Thread.Sleep(1);
        }
    }
}
