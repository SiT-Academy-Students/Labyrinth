using Labyrinth.Console.Obstacles;

namespace Labyrinth.Console.Controllers
{
    public interface IFlowController
    {
        void RenderObstacle(Obstacle obstacle);

        public void QuickObstacleGeneration(Coordinates currentCoordinates, ObstacleEdges currentEdges);
    }

}
