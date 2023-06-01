using Labyrinth.Console.Controllers;
using Labyrinth.Console.Obstacles;
using System.Collections.Generic;
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

        public void RenderObstacle(Obstacle obstacle)
        {
            this._obstaclesDict[obstacle.Coordinates] = obstacle;
            this._flowController.RenderObstacle(obstacle);
        }
        public void QuickObstacleGeneration(Coordinates currentCoordinates, ObstacleEdges currentEdges)
        {
            Obstacle currentObstacle = new Obstacle(currentCoordinates, currentEdges);
            this._flowController.RenderObstacle(currentObstacle);
        }

        public void GenerateXAxisBorders(int cycleStart, int cycleEnd, Coordinates topBorderLocation, Coordinates bottomBorderLocation, ObstacleEdges edgeOrientation)
        {
            Coordinates newCoords = new Coordinates();
            for(int startX = cycleStart; startX < cycleEnd; startX++)
            {
                newCoords = new Coordinates { X = startX, Y = topBorderLocation.Y };
                QuickObstacleGeneration(newCoords, edgeOrientation);
            }
            for (int startX = cycleStart; startX < cycleEnd; startX++)
            {
                newCoords = new Coordinates { X = startX, Y = bottomBorderLocation.Y };
                QuickObstacleGeneration(newCoords, edgeOrientation);
            }
        }
        public void GenerateYAxisBorders(int cycleStart, int cycleEnd, Coordinates leftBorderLocation, Coordinates rightBorderLocation, ObstacleEdges edgeOrientation)
        {
            Coordinates newCoords = new Coordinates();
            for (int startY = cycleStart; startY < cycleEnd; startY++)
            {
                newCoords = new Coordinates { X = leftBorderLocation.X, Y = startY };
                QuickObstacleGeneration(newCoords, edgeOrientation);
            }
            for (int startY = cycleStart; startY < cycleEnd; startY++)
            {
                newCoords = new Coordinates { X = rightBorderLocation.X, Y = startY };
                QuickObstacleGeneration(newCoords, edgeOrientation);
            }
        }

        public void GenerateMapBorders()
        {
            Coordinates topBorderLocation = new Coordinates() { Y = _topBorder };
            Coordinates botBorderLocation = new Coordinates() { Y = _botBorder };
            ObstacleEdges edgesOrientX = ObstacleEdges.Right | ObstacleEdges.Left;
            GenerateXAxisBorders(_leftBorder, _rightBorder, topBorderLocation, botBorderLocation, edgesOrientX);
            Coordinates leftBorderLocation = new Coordinates() { X = _leftBorder };
            Coordinates rightBorderLocation = new Coordinates() { X = _rightBorder };
            ObstacleEdges edgesOrientY = ObstacleEdges.Top | ObstacleEdges.Bottom;
            GenerateYAxisBorders(_topBorder, _botBorder, leftBorderLocation, rightBorderLocation, edgesOrientY);

            /*
            Coordinates newCoords = new Coordinates();
            ObstacleEdges edges = 0;
            newCoords = newCoords with { Y = _topBorder };
            for (int x = _leftBorder; x < _rightBorder; x++)
            {
                newCoords = newCoords with { X = x };
                edges = ObstacleEdges.Left | ObstacleEdges.Right;
                QuickObstacleGeneration(newCoords, edges);
            }
            newCoords = newCoords with { Y = _botBorder };
            for (int x = _leftBorder; x < _rightBorder; x++)
            {
                newCoords = newCoords with { X = x};
                edges = ObstacleEdges.Left | ObstacleEdges.Right;
                QuickObstacleGeneration(newCoords, edges);
            }
            newCoords = newCoords with { X = _leftBorder };
            for (int y = _topBorder; y < _botBorder; y++)
            {
                newCoords = newCoords with { Y = y };
                edges = ObstacleEdges.Top | ObstacleEdges.Bottom;
                QuickObstacleGeneration(newCoords, edges);
            }
            newCoords = newCoords with { X = _rightBorder };
            for (int y = _topBorder; y < _botBorder; y++)
            {
                newCoords = newCoords with { Y = y };
                edges = ObstacleEdges.Top | ObstacleEdges.Bottom;
                QuickObstacleGeneration(newCoords, edges);
            }
            /*
            int whatEdges = 0;
            if (x + 1 == rightBorder) whatEdges += 4;
            if (x - 1 == leftBorder) whatEdges += 1;
            if (y - 1 == TopBorder) whatEdges += 8;
            if (y + 1 == botBorder) whatEdges += 2;
            if (whatEdges > 0)
            {
                if (whatEdges == 9) whatEdges = 6;
                else if (whatEdges == 6) whatEdges = 9;

            }
            */
        }

        public void GenerateRandomObstacles()
        {
            for (int i = 0; i < 20; i++)
            {
                int randomObstacleX = RandomDataGenerator.NextInteger(_leftBorder + 1, _rightBorder - 1);
                int randomObstacleY = RandomDataGenerator.NextInteger(_topBorder + 1, _botBorder - 1);
                ObstacleEdges randomObstacleEdges = (ObstacleEdges)RandomDataGenerator.NextInteger(1, 16);

                Coordinates currentObstacleCoordinates = new Coordinates { X = randomObstacleX, Y = randomObstacleY };
                Obstacle currentObstacle = new Obstacle(currentObstacleCoordinates, randomObstacleEdges);

                RenderObstacle(currentObstacle);
            }
        }
    }
}