using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinth.Console
{
    public class Icon
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool SetConsoleOutputCP(uint wCodePageID);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool SetConsoleCP(uint wCodePageID);

        public static char PlayerSymbol()
        {
            
                SetConsoleOutputCP(65001);
                SetConsoleCP(65001);

                bool isRunningInCmd = Environment.GetEnvironmentVariable("ComSpec") == null;
                return isRunningInCmd ? '*' : '♕';
            
        }
    }
}
