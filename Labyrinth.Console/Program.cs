using Labyrinth.Console;
using LabyrinthConsole;
using System;
using System.Collections.Generic;
using System.Text;

Dictionary<ObstacleEdges, char> edgeSymbolsMap = ConstructObstacleEdgesMap();

// 1. Fix the game screen.
// 1.1. Add check for the resolution of the screen. Or dynamicaly adjust the settings.
int playgroundWidth = Console.LargestWindowWidth - 20, playgroundHeight = Console.LargestWindowHeight - 6, systemRows = 1;

Console.SetWindowSize(playgroundWidth, playgroundHeight);
Console.OutputEncoding = Encoding.UTF8;
Console.CursorVisible = false;

Coordinates playerCoordinates = new Coordinates { X = 0, Y = systemRows };
RenderPlayer();

for (int i = 0; i < 20; i++)
{
    int randomObstacleX = RandomDataGenerator.NextInteger(0, playgroundWidth);
    int randomObstacleY = RandomDataGenerator.NextInteger(systemRows + 1, playgroundHeight);
    ObstacleEdges randomObstacleEdges = (ObstacleEdges)RandomDataGenerator.NextInteger(1, 16);

    Coordinates currentObstacleCoordinates = new Coordinates { X = randomObstacleX, Y = randomObstacleY };
    Obstacle currentObstacle = new Obstacle(currentObstacleCoordinates, randomObstacleEdges);

    RenderObstacle(currentObstacle);
}

// 3. Move the character.
ConsoleKeyInfo pressedKey = Console.ReadKey();
while (pressedKey.Key != ConsoleKey.Escape)
{
    // 4. Configure this - ask the user for its preferrences.
    if (pressedKey.Key == ConsoleKey.UpArrow && playerCoordinates.Y > systemRows)
    {
        ClearPlayer();
        playerCoordinates.Y--;
        RenderPlayer();
    }
    else if (pressedKey.Key == ConsoleKey.RightArrow && playerCoordinates.X + 1 < playgroundWidth)
    {
        ClearPlayer();
        playerCoordinates.X++;
        RenderPlayer();
    }
    else if (pressedKey.Key == ConsoleKey.DownArrow && playerCoordinates.Y + 1 < playgroundHeight)
    {
        ClearPlayer();
        playerCoordinates.Y++;
        RenderPlayer();
    }
    else if (pressedKey.Key == ConsoleKey.LeftArrow && playerCoordinates.X > 0)
    {
        ClearPlayer();
        playerCoordinates.X--;
        RenderPlayer();
    }


    pressedKey = Console.ReadKey();
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