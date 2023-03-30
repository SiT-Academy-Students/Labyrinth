using Labyrinth.Console;
using System;
using System.Text;

// 1. Fix the game screen.
// 1.1. Add check for the resolution of the screen. Or dynamicaly adjust the settings.
int playgroundWidth = Console.LargestWindowWidth - 20, playgroundHeight = Console.LargestWindowHeight - 6, systemRows = 1;

Console.SetWindowSize(playgroundWidth, playgroundHeight);
Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.CursorVisible = false;

Console.SetCursorPosition(0, 3);
Console.WriteLine("═╗");
Console.WriteLine(" ║");
// TODO: Center the playground:
// Console.SetWindowPosition(10, 3);

// TODO: If cmd is used, our player should be rendered as a '*'. Else, use some unicode figure.
// TODO: Extract the X and Y coordinates into a common structure/class.
int playerX = 0, playerY = systemRows;
RenderPlayer();

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

    // Comment
    sb.Append($"Player coordinates - x: {playerX}, y: {playerY}");
    sb.Append(new string(' ', playgroundWidth - sb.Length));
    Console.Write(sb.ToString());
}

void PrintDebugInfo()
{
    Console.WriteLine($"Largest Width: {Console.LargestWindowWidth}; Largest Height: {Console.LargestWindowHeight}");
    
    Console.WriteLine($"Buffer Width: {Console.BufferWidth}; Buffer Height: {Console.BufferHeight}");
}