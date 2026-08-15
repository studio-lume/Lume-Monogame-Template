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
    }
}