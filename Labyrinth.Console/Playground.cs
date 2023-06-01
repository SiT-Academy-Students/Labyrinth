using Labyrinth.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinth.Console
{
    public class Playground
    {
        public Playground() 
        {

        }

        //For some reason I can't do this without System.Console
        public Coordinates Width => new Coordinates() { X = System.Console.LargestWindowWidth - 20 };
        public Coordinates Height => new Coordinates() { Y = System.Console.LargestWindowHeight - 6 };
        public Coordinates SystemRows => new Coordinates() { Y = 1 };
        public Coordinates PlayAreaLeftBorder => new Coordinates() { X = 33 };
    }
}
