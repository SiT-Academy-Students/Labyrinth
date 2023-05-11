using Labyrinth.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Labyrinth.Console
{
    public class MapGenerator
    {
        private int leftBorder => 33;
        private int rightBorder => System.Console.LargestWindowWidth - 20;
        private int topBorder => Constants.systemRows - 1;
        private int botBorder => System.Console.LargestWindowHeight - 6;

        private int toptrue;
        private int bottrue;
        private int lefttrue;
        private int righttrue;

        public MapGenerator(Dictionary<Coordinates, Obstacle> obstaclesDict, Dictionary<ObstacleEdges, char> edgeSymbolsMap)
        {
            this._obstaclesDict = obstaclesDict;
            this._edgeSymbolsMap = edgeSymbolsMap;
        }

        Dictionary<Coordinates, Obstacle> _obstaclesDict { get; }
        Dictionary<ObstacleEdges, char> _edgeSymbolsMap { get; }

        public void RenderObstacle(Obstacle obstacle)
        {
            if (!_obstaclesDict.ContainsKey(obstacle.Coordinates))
            {
                _obstaclesDict[obstacle.Coordinates] = obstacle;
            }
            else
            {
                _obstaclesDict.Remove(obstacle.Coordinates);
                _obstaclesDict[obstacle.Coordinates] = obstacle;
            }
            System.Console.SetCursorPosition(obstacle.Coordinates.X, obstacle.Coordinates.Y);
            System.Console.Write(_edgeSymbolsMap[obstacle.Edges]);
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
    }
}
//Top = 1, ═
//Right = 2, ║
//Bottom = 4, ═
//Left = 8, ║