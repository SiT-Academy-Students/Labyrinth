using Labyrinth.Console;
using Labyrinth.Console.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

Dictionary<Coordinates, Obstacle> obstaclesDict = new Dictionary<Coordinates, Obstacle>();

// 1. Fix the game screen.
// 1.1. Add check for the resolution of the screen. Or dynamicaly adjust the settings.

Playground playgroundParameters = new Playground();
Console.SetWindowSize(playgroundParameters.Width, playgroundParameters.Height);
Console.OutputEncoding = Encoding.UTF8;
Console.CursorVisible = false;

Coordinates playerCoordinates = new Coordinates { X = 0, Y = playgroundParameters.SystemRows };
RenderPlayer();

var flowController = new ConsoleFlowController();

Coordinates playerCoordinates = new Coordinates { X = 0, Y = playgroundParameters.SystemRows };
RenderPlayer();

var mapGen = new MapGenerator(obstaclesDict, flowController);
mapGen.GenerateMapBorders();
mapGen.GenerateRandomObstacles();

ConsoleKeyInfo pressedKey = Console.ReadKey(intercept: true);
while (pressedKey.Key != ConsoleKey.Escape)
{
    // 3. Configure this - ask the user for its preferrences.

    Coordinates newPlayerCoordinates = playerCoordinates.CalculateNewCoordinates(pressedKey);
    if (newPlayerCoordinates.IsWithinBorders(bannedCoordinates))
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

    StringBuilder sb = new StringBuilder(capacity: playgroundParameters.Width);
    sb.Append($"Player coordinates - x: {playerCoordinates.X}, y: {playerCoordinates.Y}");
    sb.Append(new string(' ', playgroundParameters.Width - sb.Length));
    Console.Write(sb.ToString());
}

void PrintDebugInfo()
{
    Console.WriteLine($"Largest Width: {Console.LargestWindowWidth}; Largest Height: {Console.LargestWindowHeight}");

    Console.WriteLine($"Buffer Width: {Console.BufferWidth}; Buffer Height: {Console.BufferHeight}");
}
