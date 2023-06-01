using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Labyrinth.Console.Obstacles;

namespace Labyrinth.Console.Controllers
{
    public class ConsoleFlowController : IFlowController
    {
        private static readonly Dictionary<ObstacleEdges, char> _edgeSymbolsMap = ConstructObstacleEdgesMap();

        public void RenderObstacle(Obstacle obstacle)
        {
            System.Console.SetCursorPosition(obstacle.Coordinates.X, obstacle.Coordinates.Y);
            System.Console.Write(_edgeSymbolsMap[obstacle.Edges]);
        }
        public void QuickObstacleGeneration(Coordinates currentCoordinates, ObstacleEdges currentEdges)
        {
            Obstacle currentObstacle = new Obstacle(currentCoordinates, currentEdges);
            RenderObstacle(currentObstacle);
        }

        private static Dictionary<ObstacleEdges, char> ConstructObstacleEdgesMap()
        {
            Dictionary<ObstacleEdges, char> edgeSymbolsMap = new Dictionary<ObstacleEdges, char>();
            edgeSymbolsMap[ObstacleEdges.Top] = '║';
            edgeSymbolsMap[ObstacleEdges.Bottom] = '║';
            edgeSymbolsMap[ObstacleEdges.Top | ObstacleEdges.Bottom] = '║';
            edgeSymbolsMap[ObstacleEdges.Left] = '═';
            edgeSymbolsMap[ObstacleEdges.Right] = '═';
            edgeSymbolsMap[ObstacleEdges.Left | ObstacleEdges.Right] = '═';
            edgeSymbolsMap[ObstacleEdges.Top | ObstacleEdges.Right] = '╚';
            edgeSymbolsMap[ObstacleEdges.Bottom | ObstacleEdges.Right] = '╔';
            edgeSymbolsMap[ObstacleEdges.Bottom | ObstacleEdges.Left] = '╗';
            edgeSymbolsMap[ObstacleEdges.Top | ObstacleEdges.Left] = '╝';

            edgeSymbolsMap[ObstacleEdges.Top | ObstacleEdges.Right | ObstacleEdges.Bottom] = '╠';
            edgeSymbolsMap[ObstacleEdges.Right | ObstacleEdges.Bottom | ObstacleEdges.Left] = '╦';
            edgeSymbolsMap[ObstacleEdges.Bottom | ObstacleEdges.Left | ObstacleEdges.Top] = '╣';
            edgeSymbolsMap[ObstacleEdges.Left | ObstacleEdges.Top | ObstacleEdges.Right] = '╩';

            edgeSymbolsMap[ObstacleEdges.Top | ObstacleEdges.Right | ObstacleEdges.Bottom | ObstacleEdges.Left] = '╬';

            return edgeSymbolsMap;
        }
    }
}
