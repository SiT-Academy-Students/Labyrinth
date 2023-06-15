using System;
using System.Collections.Generic;

namespace Labyrinth.Console
{
    public struct Coordinates : IEquatable<Coordinates>
    {
        public int X { get; init; }
        public int Y { get; init; }

        public override bool Equals(object obj) => obj is Coordinates c && this.Equals(c);

        public bool Equals(Coordinates other) => this.X == other.X && this.Y == other.Y;

        public override int GetHashCode() => HashCode.Combine(this.X, this.Y);

        public static bool operator ==(Coordinates left, Coordinates right) => EqualityComparer<Coordinates>.Default.Equals(left, right);

        public static bool operator !=(Coordinates left, Coordinates right) => !(left == right);
    }
}
