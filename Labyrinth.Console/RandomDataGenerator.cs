using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinth.Console
{
    internal static class RandomDataGenerator
    {
        private static readonly RandomNumberGenerator _numberGenerator = RandomNumberGenerator.Create();

        internal static int NextInteger(int lowerBoundInclusive, int upperBoundExclusive)
        {
            if (lowerBoundInclusive >= upperBoundExclusive) throw new InvalidOperationException("Invalid bounds are provided.");

            byte[] numberSegments = new byte[4];

            _numberGenerator.GetBytes(numberSegments);
            int generationResult = BitConverter.ToInt32(numberSegments);

            int range = upperBoundExclusive - lowerBoundInclusive;
            int remainder = Math.Abs(generationResult) % range;
            return lowerBoundInclusive + remainder;
        }
    }
}
