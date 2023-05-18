using Labyrinth.Console.Controllers;
using System.Collections.Generic;

namespace Labyrinth.Console
{
    public class MapGenerator
    {
        private static int leftBorder => 33;
        private static int rightBorder => System.Console.LargestWindowWidth - 20;
        private static int topBorder => Constants.systemRows - 1;
        private static int botBorder => System.Console.LargestWindowHeight - 6;

        public MapGenerator(Dictionary<Coordinates, Obstacle> obstaclesDict, IFlowController flowController)
        {
            this._obstaclesDict = obstaclesDict;
            this._flowController = flowController;
        }

        private readonly Dictionary<Coordinates, Obstacle> _obstaclesDict;
        private readonly IFlowController _flowController;

        public void RenderObstacle(Obstacle obstacle)
        {
            this._obstaclesDict[obstacle.Coordinates] = obstacle;
            this._flowController.RenderObstacle(obstacle);
        }

        public void GenerateMapBorders()
        {
            for (int x = leftBorder + 1; x < rightBorder; x++)
            {
                for (int y = topBorder; y < botBorder; y++)
                {
                    int whatEdges = 0;
                    if (x + 1 == rightBorder) whatEdges += 4;
                    if (x - 1 == leftBorder) whatEdges += 1;
                    if (y - 1 == topBorder) whatEdges += 8;
                    if (y + 1 == botBorder) whatEdges += 2;
                    if (whatEdges > 0)
                    {
                        if(whatEdges == 9) whatEdges = 6;
                        else if(whatEdges == 6) whatEdges = 9;
                        Coordinates currentCoordi = new Coordinates() { X = x, Y = y };
                        Obstacle currentObstacle = new Obstacle(currentCoordi, (ObstacleEdges)whatEdges);
                        RenderObstacle(currentObstacle);
                    }
                }
            }
        }

        public void GenerateRandomObstacles()
        {
            for (int i = 0; i < 20; i++)
            {
                int randomObstacleX = RandomDataGenerator.NextInteger(leftBorder + 1, rightBorder - 1);
                int randomObstacleY = RandomDataGenerator.NextInteger(Constants.systemRows + 1, botBorder - 1);
                ObstacleEdges randomObstacleEdges = (ObstacleEdges)RandomDataGenerator.NextInteger(1, 16);

                Coordinates currentObstacleCoordinates = new Coordinates { X = randomObstacleX, Y = randomObstacleY };
                Obstacle currentObstacle = new Obstacle(currentObstacleCoordinates, randomObstacleEdges);

                RenderObstacle(currentObstacle);
            }
        }
    }
}
//Top = 1, ═
//Right = 2, ║
//Bottom = 4, ═
//Left = 8, ║