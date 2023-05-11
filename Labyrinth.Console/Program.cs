using Labyrinth.Console;
using System;
using System.Collections.Generic;
using System.Text;

Dictionary<ObstacleEdges, char> edgeSymbolsMap = ConstructObstacleEdgesMap();
Dictionary<Coordinates, Obstacle> obstaclesDict = new Dictionary<Coordinates, Obstacle>();

// 1. Fix the game screen.
// 1.1. Add check for the resolution of the screen. Or dynamicaly adjust the settings.
int playgroundWidth = Console.LargestWindowWidth - 20, playgroundHeight = Console.LargestWindowHeight - 6;
Console.SetWindowSize(playgroundWidth, playgroundHeight);
Console.OutputEncoding = Encoding.UTF8;
Console.CursorVisible = false;

var mapGen = new MapGenerator(obstaclesDict, edgeSymbolsMap);
mapGen.GenerateMapBorders();

// 3. Move the character.
Coordinates playerCoordinates = new Coordinates { X = 0, Y = Constants.systemRows };
RenderPlayer();

ConsoleKeyInfo pressedKey = Console.ReadKey(intercept: true);
while (pressedKey.Key != ConsoleKey.Escape)
{
    // 4. Configure this - ask the user for its preferrences.
    Coordinates newPlayerCoordinates;
    if (pressedKey.Key == ConsoleKey.UpArrow)
        newPlayerCoordinates = playerCoordinates with { Y = playerCoordinates.Y - 1 };
    else if (pressedKey.Key == ConsoleKey.RightArrow)
        newPlayerCoordinates = playerCoordinates with { X = playerCoordinates.X + 1 };
    else if (pressedKey.Key == ConsoleKey.DownArrow)
        newPlayerCoordinates = playerCoordinates with { Y = playerCoordinates.Y + 1 };
    else if (pressedKey.Key == ConsoleKey.LeftArrow)
        newPlayerCoordinates = playerCoordinates with { X = playerCoordinates.X - 1 };
    else newPlayerCoordinates = playerCoordinates;

    if (newPlayerCoordinates.X >= 0 && newPlayerCoordinates.Y >= Constants.systemRows && newPlayerCoordinates.X < playgroundWidth && newPlayerCoordinates.Y < playgroundHeight && !obstaclesDict.ContainsKey(newPlayerCoordinates))
    {
        ClearPlayer();
        playerCoordinates = newPlayerCoordinates;
        RenderPlayer();
    }

    pressedKey = Console.ReadKey(intercept: true);
}

void ClearPlayer()
{
    Console.SetCursorPosition(playerCoordinates.X, playerCoordinates.Y);
    Console.Write(' ');
}

void RenderPlayer()
{
    Console.SetCursorPosition(playerCoordinates.X, playerCoordinates.Y);
    Console.Write(Constants.PlayerSymbol);

    Console.SetCursorPosition(0, 0);

    StringBuilder sb = new StringBuilder(capacity: playgroundWidth);
    sb.Append($"Player coordinates - x: {playerCoordinates.X}, y: {playerCoordinates.Y}");
    sb.Append(new string(' ', playgroundWidth - sb.Length));
    Console.Write(sb.ToString());
}

void PrintDebugInfo()
{
    Console.WriteLine($"Largest Width: {Console.LargestWindowWidth}; Largest Height: {Console.LargestWindowHeight}");

    Console.WriteLine($"Buffer Width: {Console.BufferWidth}; Buffer Height: {Console.BufferHeight}");
}

static Dictionary<ObstacleEdges, char> ConstructObstacleEdgesMap()
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