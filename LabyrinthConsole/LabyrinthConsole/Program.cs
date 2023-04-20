﻿using LabyrinthConsole;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

Process currentProcess = Process.GetCurrentProcess();

Dictionary<ObstacleEdges, char> edgeSymbolsMap = ConstructObstacleEdgesMap();

// 1. Fix the game screen.
// 1.1. Add check for the resolution of the screen. Or dynamicaly adjust the settings.
int playgroundWidth = Console.LargestWindowWidth - 20, playgroundHeight = Console.LargestWindowHeight - 6, systemRows = 1;

Console.SetWindowSize(playgroundWidth, playgroundHeight);
Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.CursorVisible = false;

// TODO: Center the playground:
// Console.SetWindowPosition(10, 3);

// TODO: If cmd is used, our player should be rendered as a '*'. Else, use some unicode figure.
// TODO: Extract the X and Y coordinates into a common structure/class.
Coordinates PlayerCoordinates = new Coordinates();
PlayerCoordinates.PositionX = 0;
PlayerCoordinates.PositionY = systemRows;
RenderPlayer();

Random random = new Random();
for (int i = 0; i < 20; i++)
{
    int randomObstacleX = random.Next(0, playgroundWidth);
    int randomObstacleY = random.Next(systemRows + 1, playgroundHeight);
    ObstacleEdges randomObstacleEdges = (ObstacleEdges)random.Next(1, 16);
    Obstacle currentObstacle = new Obstacle(randomObstacleX, randomObstacleY, randomObstacleEdges);

    RenderObstacle(currentObstacle);
}

// 3. Move the character.
ConsoleKeyInfo pressedKey = Console.ReadKey();
while (pressedKey.Key != ConsoleKey.Escape)
{
    // 4. Configure this - ask the user for its preferrences.
    if (pressedKey.Key == ConsoleKey.UpArrow && PlayerCoordinates.PositionY > systemRows)
    {
        ClearPlayer();
        PlayerCoordinates.PositionY--;
        RenderPlayer();
    }
    else if (pressedKey.Key == ConsoleKey.RightArrow && PlayerCoordinates.PositionX + 1 < playgroundWidth)
    {
        ClearPlayer();
        PlayerCoordinates.PositionX++;
        RenderPlayer();
    }
    else if (pressedKey.Key == ConsoleKey.DownArrow && PlayerCoordinates.PositionY + 1 < playgroundHeight)
    {
        ClearPlayer();
        PlayerCoordinates.PositionY++;
        RenderPlayer();
    }
    else if (pressedKey.Key == ConsoleKey.LeftArrow && PlayerCoordinates.PositionX > 0)
    {
        ClearPlayer();
        PlayerCoordinates.PositionX--;
        RenderPlayer();
    }


    pressedKey = Console.ReadKey();
}

void ClearPlayer()
{
    Console.SetCursorPosition(PlayerCoordinates.PositionX, PlayerCoordinates.PositionY);
    Console.Write(' ');
}

void RenderPlayer()
{
    Console.SetCursorPosition(PlayerCoordinates.PositionX, PlayerCoordinates.PositionY);
    Console.Write(Constants.PlayerSymbol);

    Console.SetCursorPosition(0, 0);

    StringBuilder sb = new StringBuilder(capacity: playgroundWidth);
    sb.Append($"Player coordinates - x: {PlayerCoordinates.PositionX}, y: {PlayerCoordinates.PositionY}");
    sb.Append(new string(' ', playgroundWidth - sb.Length));
    Console.Write(sb.ToString());
}

void RenderObstacle(Obstacle obstacle)
{
    Console.SetCursorPosition(obstacle.PositionX, obstacle.PositionY);
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