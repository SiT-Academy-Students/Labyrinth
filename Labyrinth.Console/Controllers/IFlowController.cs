using Labyrinth.Console.Obstacles;

namespace Labyrinth.Console.Controllers
{
    public interface IFlowController
    {
        void Render(Coordinates coordinates, char symbol);
        void RenderObstacle(Obstacle obstacle);
    }

}
