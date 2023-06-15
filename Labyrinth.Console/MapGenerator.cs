using Labyrinth.Console.Controllers;
using Labyrinth.Console.Obstacles;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography.X509Certificates;

namespace Labyrinth.Console
{
    public class MapGenerator
    {
        public MapGenerator(Dictionary<Coordinates, Obstacle> obstaclesDict, IFlowController flowController, int rightBorder, int leftBorder, int topBorder, int botBorder)
        {
            this._rightBorder = rightBorder;
            this._leftBorder = leftBorder;
            this._topBorder = topBorder;
            this._botBorder = botBorder;
            this._obstaclesDict = obstaclesDict;
            this._flowController = flowController;
        }

        private readonly Dictionary<Coordinates, Obstacle> _obstaclesDict;
        private readonly IFlowController _flowController;

        private readonly int _leftBorder;
        private readonly int _rightBorder;
        private readonly int _topBorder;
        private readonly int _botBorder;

        public void RenderObstacle(Coordinates coordinates, ObstacleEdges edges)
        {
            Obstacle obstacle = new Obstacle(coordinates, edges);
            this._obstaclesDict[obstacle.Coordinates] = obstacle;
            this._flowController.RenderObstacle(obstacle);

            this.NormalizeEdges(coordinates);
        }

        public void GenerateMapBorders()
        {
            ObstacleEdges edgesOrientX = ObstacleEdges.Right | ObstacleEdges.Left;
            GenerateXAxisBorders(this._leftBorder, this._rightBorder, edgesOrientX);

            ObstacleEdges edgesOrientY = ObstacleEdges.Top | ObstacleEdges.Bottom;
            GenerateYAxisBorders(this._topBorder, this._botBorder, edgesOrientY);
        }

        public void GenerateRandomObstacles()
        {
            for (int i = 0; i < 20; i++)
            {
                int randomObstacleX = RandomDataGenerator.NextInteger(_leftBorder + 1, _rightBorder - 1);
                int randomObstacleY = RandomDataGenerator.NextInteger(_topBorder + 1, _botBorder - 1);
                ObstacleEdges randomObstacleEdges = (ObstacleEdges)RandomDataGenerator.NextInteger(1, 16);

                Coordinates currentObstacleCoordinates = new Coordinates { X = randomObstacleX, Y = randomObstacleY };
                RenderObstacle(currentObstacleCoordinates, randomObstacleEdges);
            }
        }

        private void NormalizeEdges(Coordinates coordinates)
        {
            this.NormalizeEdges(coordinates, coordinates with { Y = coordinates.Y - 1 }, ObstacleEdges.Top, ObstacleEdges.Bottom);
            this.NormalizeEdges(coordinates, coordinates with { X = coordinates.X + 1 }, ObstacleEdges.Right, ObstacleEdges.Left);
            this.NormalizeEdges(coordinates, coordinates with { Y = coordinates.Y + 1 }, ObstacleEdges.Bottom, ObstacleEdges.Top);
            this.NormalizeEdges(coordinates, coordinates with { X = coordinates.X - 1 }, ObstacleEdges.Left, ObstacleEdges.Right);
        }

        private void NormalizeEdges(Coordinates source, Coordinates destination, ObstacleEdges sourceEdges, ObstacleEdges destinationEdges)
        {
            if (!this._obstaclesDict.ContainsKey(destination)) return;

            Obstacle sourceObstacle = this._obstaclesDict[source];
            if ((sourceObstacle.Edges & sourceEdges) == ObstacleEdges.None)
                this.RenderObstacle(sourceObstacle.Coordinates, sourceObstacle.Edges | sourceEdges);

            Obstacle destinationObstacle = this._obstaclesDict[destination];
            if ((destinationObstacle.Edges & destinationEdges) == ObstacleEdges.None)
                this.RenderObstacle(destinationObstacle.Coordinates, destinationObstacle.Edges | destinationEdges);
        }

        private void GenerateXAxisBorders(int cycleStart, int cycleEnd, ObstacleEdges edgeOrientation)
        {
            for (int startX = cycleStart; startX < cycleEnd; startX++)
            {
                Coordinates top = new Coordinates { X = startX, Y = this._topBorder };
                RenderObstacle(top, edgeOrientation);

                Coordinates bot = new Coordinates { X = startX, Y = this._botBorder - 1 };
                RenderObstacle(bot, edgeOrientation);
            }
        }

        private void GenerateYAxisBorders(int cycleStart, int cycleEnd, ObstacleEdges edgeOrientation)
        {
            for (int startY = cycleStart; startY < cycleEnd; startY++)
            {
                Coordinates left = new Coordinates { X = this._leftBorder, Y = startY };
                RenderObstacle(left, edgeOrientation);

                Coordinates right = new Coordinates { X = this._rightBorder - 1, Y = startY };
                RenderObstacle(right, edgeOrientation);
            }
        }
    }
}