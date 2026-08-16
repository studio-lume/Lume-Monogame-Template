using System;
using Microsoft.Xna.Framework;
using Project_Template.Source.Core;

namespace Project_Template.Source.Components.Transform {
    public class Transform
        : ComponentBase {
        public Vector2I WorldPosition { get; set; } = Vector2I.Zero;

        public Vector2I LocalPosition {
            get => WorldPosition;
            set {
                var radians = MathHelper.ToRadians(Rotation);

                var cos = MathF.Cos(radians);
                var sin = MathF.Sin(radians);

                var x = (int)(value.X * cos - value.Y * sin);
                var y = (int)(value.X * sin + value.Y * cos);

                WorldPosition += new Vector2I(x, y);
            }
        }

        public Vector2I Size { get; set; } = new(100, 100);
        public float Rotation { get; set; }

        public Rectangle Bounds =>
            new(
                WorldPosition.X,
                WorldPosition.Y,
                Size.X,
                Size.Y
            );
    }
}