using System;
using Microsoft.Xna.Framework;
using Project_Template.Source.Core;

namespace Project_Template.Source.Components.Transform {
    public class Transform
        : ComponentBase {
        public Vector2I Position { get; set; } = Vector2I.Zero;
        public Vector2I Size { get; set; } = new(100, 100);
        public float Rotation { get; set; }

        public Rectangle Bounds =>
            new(
                Position.X,
                Position.Y,
                Size.X,
                Size.Y
            );

        public Vector2 Forward =>
            new(MathF.Sin(Rotation), -MathF.Cos(Rotation));

        public Vector2 Right =>
            new(MathF.Cos(Rotation), MathF.Sin(Rotation));

        public void Translate(Vector2I position) => Position += position;

        public void TranslateLocal(Vector2I movement, float? rotationOverwrite = null) {
            var rotation = rotationOverwrite ?? Rotation;

            var cos = MathF.Cos(rotation);
            var sin = MathF.Sin(rotation);

            var x = (int)MathF.Round(movement.X * cos - movement.Y * sin);
            var y = (int)MathF.Round(movement.X * sin + movement.Y * cos);

            Translate(new(x, y));
        }
    }
}