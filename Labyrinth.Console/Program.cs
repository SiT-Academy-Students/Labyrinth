using Labyrinth.Console;
using Labyrinth.Console.Controllers;
using Labyrinth.Console.Extensions;
using Labyrinth.Console.Obstacles;
using System;
using System.Collections.Generic;
using System.Text;

Dictionary<Coordinates, Obstacle> obstaclesDict = new Dictionary<Coordinates, Obstacle>();

// 1. Fix the game screen.
// 1.1. Add check for the resolution of the screen. Or dynamicaly adjust the settings.
Playground playground = new Playground { Width = Console.LargestWindowWidth - 20, Height = Console.LargestWindowHeight - 6, SystemRows = 1, SystemColumns = 0 };
Coordinates startCoordinates = new Coordinates { X = 1, Y = playground.SystemRows + 1 };
Coordinates finishCoordinates = new Coordinates { X = playground.Width - 2, Y = playground.Height - 2 };

Console.SetWindowSize(playground.Width, playground.Height);
Console.OutputEncoding = Encoding.UTF8;
Console.CursorVisible = false;

var flowController = new ConsoleFlowController();
var pathFinder = new DfsPathFiner(startCoordinates, finishCoordinates, playground);
var mapGenerator = new MapGenerator(obstaclesDict, flowController, pathFinder, playground);
mapGenerator.GenerateMapBorders();
mapGenerator.GenerateRandomObstacles(10000);

Coordinates playerCoordinates = startCoordinates;
RenderPlayer();
RenderFinish();

ConsoleKeyInfo pressedKey = Console.ReadKey(intercept: true);
while (pressedKey.Key != ConsoleKey.Escape)
{
    // 3. Configure this - ask the user for its preferrences.

    Coordinates newPlayerCoordinates = playerCoordinates.CalculateNewCoordinates(pressedKey);
    if (newPlayerCoordinates.IsAvailable(playground, obstaclesDict))
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

void RenderFinish()
{
    Console.SetCursorPosition(finishCoordinates.X, finishCoordinates.Y);
    Console.Write(Constants.FinishSymbol);
    Console.SetCursorPosition(0, 0);
}

void PrintDebugInfo()
{
    Console.WriteLine($"Largest Width: {Console.LargestWindowWidth}; Largest Height: {Console.LargestWindowHeight}");

    Console.WriteLine($"Buffer Width: {Console.BufferWidth}; Buffer Height: {Console.BufferHeight}");
}