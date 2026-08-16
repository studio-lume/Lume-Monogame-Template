using System;
using Microsoft.Xna.Framework;

namespace Project_Template.Source.Core {
    public readonly struct Vector2I : IEquatable<Vector2I> {
        public int X { get; }
        public int Y { get; }

        public Vector2I(float x, float y) {
            X = (int)MathF.Round(x);
            Y = (int)MathF.Round(y);
        }

        public Vector2I(int x, int y) {
            X = x;
            Y = y;
        }

        public static readonly Vector2I Zero = new(0, 0);
        public static readonly Vector2I One = new(1, 1);
        public static readonly Vector2I UnitX = new(1, 0);
        public static readonly Vector2I UnitY = new(0, 1);

        public int LengthSquared => X * X + Y * Y;

        public float Length => MathF.Sqrt(LengthSquared);

        public static implicit operator Vector2I(Vector2 value) =>
            new(
                (int)MathF.Round(value.X),
                (int)MathF.Round(value.Y)
            );

        public static implicit operator Vector2(Vector2I value) => new(value.X, value.Y);

        public static Vector2I operator +(Vector2I left, Vector2I right) => new(left.X + right.X, left.Y + right.Y);

        public static Vector2I operator -(Vector2I left, Vector2I right) => new(left.X - right.X, left.Y - right.Y);

        public static Vector2I operator -(Vector2I value) => new(-value.X, -value.Y);

        public static Vector2I operator *(Vector2I value, int scalar) => new(value.X * scalar, value.Y * scalar);

        public static Vector2I operator *(int scalar, Vector2I value) => value * scalar;

        public static Vector2I operator /(Vector2I value, int scalar) {
            if (scalar == 0) {
                throw new DivideByZeroException();
            }

            return new(value.X / scalar, value.Y / scalar);
        }

        public static bool operator ==(Vector2I left, Vector2I right) => left.X == right.X && left.Y == right.Y;

        public static bool operator !=(Vector2I left, Vector2I right) => !(left == right);

        public static Vector2I Min(Vector2I left, Vector2I right) =>
            new(
                Math.Min(left.X, right.X),
                Math.Min(left.Y, right.Y)
            );

        public static Vector2I Max(Vector2I left, Vector2I right) =>
            new(
                Math.Max(left.X, right.X),
                Math.Max(left.Y, right.Y)
            );

        public static Vector2I Abs(Vector2I value) =>
            new(
                Math.Abs(value.X),
                Math.Abs(value.Y)
            );

        public Vector2I Sign() =>
            new(
                Math.Sign(X),
                Math.Sign(Y)
            );

        public int ManhattanDistance(Vector2I other) => Math.Abs(X - other.X) + Math.Abs(Y - other.Y);

        public int DistanceSquared(Vector2I other) {
            var x = X - other.X;
            var y = Y - other.Y;

            return x * x + y * y;
        }

        public float Distance(Vector2I other) => MathF.Sqrt(DistanceSquared(other));

        public bool Equals(Vector2I other) => X == other.X && Y == other.Y;

        public override bool Equals(object obj) => obj is Vector2I other && Equals(other);

        public override int GetHashCode() => HashCode.Combine(X, Y);

        public override string ToString() => $"({X}, {Y})";
    }
}