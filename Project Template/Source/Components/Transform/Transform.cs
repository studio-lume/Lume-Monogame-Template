using Microsoft.Xna.Framework;

namespace Project_Quarry.Source.Components.Transform {
    public class Transform
        : ComponentBase {
        public Vector2 Position { get; set; } = Vector2.Zero;
        public Vector2 Size { get; set; } = new(100, 100);
        public float Rotation { get; set; }

        public Rectangle Bounds =>
            new(
                (int)Position.X,
                (int)Position.Y,
                (int)Size.X,
                (int)Size.Y
            );
    }
}