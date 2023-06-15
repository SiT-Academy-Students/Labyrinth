using Labyrinth.Console.Controllers;
using Labyrinth.Console.Obstacles;
using System.Collections.Generic;

namespace Labyrinth.Console
{
    public class MapGenerator
    {
        public MapGenerator(Dictionary<Coordinates, Obstacle> obstaclesDict, IFlowController flowController, IPathFinder pathFinder, Playground playground)
        {
            this._obstaclesDict = obstaclesDict;
            this._flowController = flowController;
            this._pathFinder = pathFinder;
            this._playground = playground;
        }

        private readonly Dictionary<Coordinates, Obstacle> _obstaclesDict;
        private readonly IFlowController _flowController;
        private readonly IPathFinder _pathFinder;
        private readonly Playground _playground;

        public void RenderObstacle(Coordinates coordinates, ObstacleEdges edges, bool checkIfPathExists = false)
        {
            Obstacle obstacle = new Obstacle(coordinates, edges);
            this._obstaclesDict[obstacle.Coordinates] = obstacle;

            if (checkIfPathExists && !this._pathFinder.SolutionExists(this._obstaclesDict))
                this._obstaclesDict.Remove(obstacle.Coordinates);
            else
            {
                this._flowController.RenderObstacle(obstacle);
                this.NormalizeEdges(coordinates);
            }
        }

        public void GenerateMapBorders()
        {
            ObstacleEdges edgesOrientX = ObstacleEdges.Right | ObstacleEdges.Left;
            GenerateXAxisBorders(this._playground.SystemColumns, this._playground.Width, edgesOrientX);

            ObstacleEdges edgesOrientY = ObstacleEdges.Top | ObstacleEdges.Bottom;
            GenerateYAxisBorders(this._playground.SystemRows, this._playground.Height, edgesOrientY);
        }

        public void GenerateRandomObstacles(int count)
        {
            for (int i = 0; i < count; i++)
            {
                int randomObstacleX = RandomDataGenerator.NextInteger(this._playground.SystemColumns + 1, this._playground.Width - 1);
                int randomObstacleY = RandomDataGenerator.NextInteger(this._playground.SystemRows + 1, this._playground.Height - 1);

                Coordinates currentObstacleCoordinates = new Coordinates { X = randomObstacleX, Y = randomObstacleY };
                ObstacleEdges randomObstacleEdges = (ObstacleEdges)RandomDataGenerator.NextInteger(1, 16);

                RenderObstacle(currentObstacleCoordinates, randomObstacleEdges, checkIfPathExists: true);
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
                Coordinates top = new Coordinates { X = startX, Y = this._playground.SystemRows };
                RenderObstacle(top, edgeOrientation);

                Coordinates bot = new Coordinates { X = startX, Y = this._playground.Height - 1 };
                RenderObstacle(bot, edgeOrientation);
            }
        }

        private void GenerateYAxisBorders(int cycleStart, int cycleEnd, ObstacleEdges edgeOrientation)
        {
            for (int startY = cycleStart; startY < cycleEnd; startY++)
            {
                Coordinates left = new Coordinates { X = this._playground.SystemColumns, Y = startY };
                RenderObstacle(left, edgeOrientation);

                Coordinates right = new Coordinates { X = this._playground.Width - 1, Y = startY };
                RenderObstacle(right, edgeOrientation);
            }
        }
    }
}