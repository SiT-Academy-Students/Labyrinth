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
Playground playground = new Playground { Width = Console.LargestWindowWidth - 20, Height = Console.LargestWindowHeight - 6, SystemRows = 1 };

Console.SetWindowSize(playground.Width, playground.Height);
Console.OutputEncoding = Encoding.UTF8;
Console.CursorVisible = false;

var flowController = new ConsoleFlowController();
var mapGenerator = new MapGenerator(obstaclesDict, flowController, playground.Width - 1, 33, playground.SystemRows, playground.Height - 1);
mapGenerator.GenerateMapBorders();
mapGenerator.GenerateRandomObstacles();

Coordinates playerCoordinates = new Coordinates { X = 0, Y = playground.SystemRows };
RenderPlayer();

ConsoleKeyInfo pressedKey = Console.ReadKey(intercept: true);
while (pressedKey.Key != ConsoleKey.Escape)
{
    // 3. Configure this - ask the user for its preferrences.

    Coordinates newPlayerCoordinates = playerCoordinates.CalculateNewCoordinates(pressedKey);
    if (newPlayerCoordinates.IsWithinBorders(playground, obstaclesDict))
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

void PrintDebugInfo()
{
    Console.WriteLine($"Largest Width: {Console.LargestWindowWidth}; Largest Height: {Console.LargestWindowHeight}");

    Console.WriteLine($"Buffer Width: {Console.BufferWidth}; Buffer Height: {Console.BufferHeight}");
}