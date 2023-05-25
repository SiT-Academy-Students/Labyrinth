﻿using Labyrinth.Console;
using Labyrinth.Console.Extensions;
using LabyrinthConsole;
using System;
using System.Collections.Generic;
using System.Text;

Dictionary<ObstacleEdges, char> edgeSymbolsMap = ConstructObstacleEdgesMap();
HashSet<Coordinates> bannedCoordinates = new HashSet<Coordinates>();

// 1. Fix the game screen.
// 1.1. Add check for the resolution of the screen. Or dynamicaly adjust the settings.

Playground playground = new Playground { Width = Console.LargestWindowWidth - 20, Height = Console.LargestWindowHeight - 6, SystemRows = 1 };

Console.SetWindowSize(playground.Width, playground.Height);
Console.OutputEncoding = Encoding.UTF8;
Console.CursorVisible = false;

Coordinates playerCoordinates = new Coordinates { X = 0, Y = playground.SystemRows };
RenderPlayer();

for (int i = 0; i < 20; i++)
{
    int randomObstacleX = RandomDataGenerator.NextInteger(0, playground.Width);
    int randomObstacleY = RandomDataGenerator.NextInteger(playground.SystemRows + 1, playground.Height);
    ObstacleEdges randomObstacleEdges = (ObstacleEdges)RandomDataGenerator.NextInteger(1, 16);

    Coordinates currentObstacleCoordinates = new Coordinates { X = randomObstacleX, Y = randomObstacleY };
    Obstacle currentObstacle = new Obstacle(currentObstacleCoordinates, randomObstacleEdges);

    bannedCoordinates.Add(currentObstacleCoordinates);

    RenderObstacle(currentObstacle);
}

ConsoleKeyInfo pressedKey = Console.ReadKey(intercept: true);
while (pressedKey.Key != ConsoleKey.Escape)
{
    // 3. Configure this - ask the user for its preferrences.

    Coordinates newPlayerCoordinates = playerCoordinates.CalculateNewCoordinates(pressedKey);
    if (newPlayerCoordinates.IsWithinBorders(playground, bannedCoordinates))
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

    StringBuilder sb = new StringBuilder(capacity: playground.Width);
    sb.Append($"Player coordinates - x: {playerCoordinates.X}, y: {playerCoordinates.Y}");
    sb.Append(new string(' ', playground.Width - sb.Length));
    Console.Write(sb.ToString());
}

void RenderObstacle(Obstacle obstacle)
{
    Console.SetCursorPosition(obstacle.Coordinates.X, obstacle.Coordinates.Y);
    Console.Write(edgeSymbolsMap[obstacle.Edges]);
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