using Labyrinth.Console.Obstacles;
using System.Collections.Generic;

namespace Labyrinth.Console.Controllers
{
    public interface IPathFinder
    {
        bool SolutionExists(IDictionary<Coordinates, Obstacle> obstaclesMap);
    }
}
