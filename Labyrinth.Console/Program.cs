using Labyrinth.Console;
using System;
using System.Collections.Generic;
using System.Text;

Dictionary<ObstacleEdges, char> edgeSymbolsMap = ConstructObstacleEdgesMap();

// 1. Fix the game screen.
// 1.1. Add check for the resolution of the screen. Or dynamicaly adjust the settings.
int playgroundWidth = Console.LargestWindowWidth - 20, playgroundHeight = Console.LargestWindowHeight - 6, systemRows = 3;
bool mapEdit = false;


Console.SetWindowSize(playgroundWidth, playgroundHeight);
Console.OutputEncoding = Encoding.UTF8;
Console.CursorVisible = false;

int playerX = 0, playerY = systemRows;
RenderPlayer();

/*
for (int i = 0; i < 20; i++)
{
    int randomObstacleX = RandomDataGenerator.NextInteger(0, playgroundWidth);
    int randomObstacleY = RandomDataGenerator.NextInteger(systemRows + 1, playgroundHeight);
    ObstacleEdges randomObstacleEdges = (ObstacleEdges)RandomDataGenerator.NextInteger(1, 16);
    Obstacle currentObstacle = new Obstacle(randomObstacleX, randomObstacleY, randomObstacleEdges);
     
    RenderObstacle(currentObstacle);
}
*/



// 3. Move the character.
ConsoleKeyInfo pressedKey = Console.ReadKey();
while (pressedKey.Key != ConsoleKey.Escape)
{
    // 4. Configure this - ask the user for its preferrences.
    if (pressedKey.Key == ConsoleKey.UpArrow && playerY > systemRows)
    {
        ClearPlayer();
        playerY--;
        RenderPlayer();
    }
    else if (pressedKey.Key == ConsoleKey.RightArrow && playerX + 1 < playgroundWidth)
    {
        ClearPlayer();
        playerX++;
        RenderPlayer();
    }
    else if (pressedKey.Key == ConsoleKey.DownArrow && playerY + 1 < playgroundHeight)
    {
        ClearPlayer();
        playerY++;
        RenderPlayer();
    }
    else if (pressedKey.Key == ConsoleKey.LeftArrow && playerX > 0)
    {
        ClearPlayer();
        playerX--;
        RenderPlayer();
    }
    else if (pressedKey.Key == ConsoleKey.K)
    {
        mapEdit = !mapEdit;
    }
    else if (pressedKey.Key == ConsoleKey.Spacebar && mapEdit == true)
    {
        ObstacleEdges randomObstacleEdges = (ObstacleEdges)RandomDataGenerator.NextInteger(1, 16);
        Obstacle currentObstacle = new Obstacle(playerX+1, playerY, randomObstacleEdges);

        RenderObstacle(currentObstacle);
    }

    pressedKey = Console.ReadKey();
}

void ClearPlayer()
{
    Console.SetCursorPosition(playerX, playerY);
    Console.Write(' ');
}

void RenderPlayer()
{
    Console.SetCursorPosition(playerX, playerY);
    Console.Write(Constants.PlayerSymbol);

    Console.SetCursorPosition(0, 0);

    StringBuilder sb = new StringBuilder(capacity: playgroundWidth);
    sb.Append($"Player coordinates - x: {playerX}, y: {playerY}");
    sb.Append(new string(' ', playgroundWidth - sb.Length));
    Console.Write(sb.ToString());
}

void RenderObstacle(Obstacle obstacle)
{
    Console.SetCursorPosition(obstacle.X, obstacle.Y);
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