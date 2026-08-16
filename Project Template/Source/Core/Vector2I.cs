using System;
using Microsoft.Xna.Framework;

namespace Project_Template.Source.Core {
    public readonly struct Vector2I : IEquatable<Vector2I> {
        public int X { get; }
        public int Y { get; }

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

        public static implicit operator Vector2I(Vector2 value) {
            return new(
                (int)MathF.Round(value.X),
                (int)MathF.Round(value.Y)
            );
        }

        public static implicit operator Vector2(Vector2I value) {
            return new(value.X, value.Y);
        }

        public static Vector2I operator +(Vector2I left, Vector2I right) {
            return new(left.X + right.X, left.Y + right.Y);
        }

        public static Vector2I operator -(Vector2I left, Vector2I right) {
            return new(left.X - right.X, left.Y - right.Y);
        }

        public static Vector2I operator -(Vector2I value) {
            return new(-value.X, -value.Y);
        }

        public static Vector2I operator *(Vector2I value, int scalar) {
            return new(value.X * scalar, value.Y * scalar);
        }

        public static Vector2I operator *(int scalar, Vector2I value) {
            return value * scalar;
        }

        public static Vector2I operator /(Vector2I value, int scalar) {
            if (scalar == 0)
                throw new DivideByZeroException();

            return new(value.X / scalar, value.Y / scalar);
        }

        public static bool operator ==(Vector2I left, Vector2I right) {
            return left.X == right.X && left.Y == right.Y;
        }

        public static bool operator !=(Vector2I left, Vector2I right) {
            return !(left == right);
        }

        public static Vector2I Min(Vector2I left, Vector2I right) {
            return new(
                Math.Min(left.X, right.X),
                Math.Min(left.Y, right.Y)
            );
        }

        public static Vector2I Max(Vector2I left, Vector2I right) {
            return new(
                Math.Max(left.X, right.X),
                Math.Max(left.Y, right.Y)
            );
        }

        public static Vector2I Abs(Vector2I value) {
            return new(
                Math.Abs(value.X),
                Math.Abs(value.Y)
            );
        }

        public Vector2I Sign() {
            return new(
                Math.Sign(X),
                Math.Sign(Y)
            );
        }

        public int ManhattanDistance(Vector2I other) {
            return Math.Abs(X - other.X) + Math.Abs(Y - other.Y);
        }

        public int DistanceSquared(Vector2I other) {
            int x = X - other.X;
            int y = Y - other.Y;

            return x * x + y * y;
        }

        public float Distance(Vector2I other) {
            return MathF.Sqrt(DistanceSquared(other));
        }

        public bool Equals(Vector2I other) {
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object obj) {
            return obj is Vector2I other && Equals(other);
        }

        public override int GetHashCode() {
            return HashCode.Combine(X, Y);
        }

        public override string ToString() {
            return $"({X}, {Y})";
        }
    }
}