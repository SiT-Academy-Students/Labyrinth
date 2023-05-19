using Labyrinth.Console;
using LabyrinthConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinth.Console
{
    public class Playground
    {
        //For some reason I can't do this without System.Console
        public int Width => System.Console.LargestWindowWidth - 20;
        public int Height => System.Console.LargestWindowHeight - 6;
        public int SystemRows => 1;
    }
}
