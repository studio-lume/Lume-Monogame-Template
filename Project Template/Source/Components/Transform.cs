using System;
using Microsoft.Xna.Framework;
using Project_Template.Source.Core;

namespace Project_Template.Source.Components {
    /// <summary>
    ///     Transform Component.
    ///     The transform component is a standard component applied to every actor when created.
    ///     This method is not a standard component, nor does it qualify as a component.
    ///     The component can be accessed by doing actor.Transform (not the usual actor.TryGetComponent() api).
    ///     Thus, it cannot be added manually by an actor.
    /// </summary>
    public class Transform {
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

        public Rectangle AABB {
            get {
                var cos = MathF.Abs(MathF.Cos(Rotation));
                var sin = MathF.Abs(MathF.Sin(Rotation));

                var width = Size.X * cos + Size.Y * sin;
                var height = Size.X * sin + Size.Y * cos;

                var centerX = Position.X;
                var centerY = Position.Y;

                return new(
                    (int)MathF.Floor(centerX - width * 0.5f),
                    (int)MathF.Floor(centerY - height * 0.5f),
                    (int)MathF.Ceiling(width),
                    (int)MathF.Ceiling(height)
                );
            }
        }

        public Vector2 Forward =>
            new(MathF.Sin(Rotation), -MathF.Cos(Rotation));

        public Vector2 Right =>
            new(MathF.Cos(Rotation), MathF.Sin(Rotation));

        public void Translate(Vector2I position) => Position += position;

        public void TranslateLocal(Vector2I movement, float? rotationOverwrite = null) {
            var rotation = rotationOverwrite ?? Rotation;
            var cos = MathF.Cos(rotation);
            var sin = MathF.Sin(rotation);

            Translate(new(
                movement.X * cos - movement.Y * sin,
                movement.X * sin + movement.Y * cos
            ));
        }
    }
}