using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project_Template.Source.Core.DrawPass {
    public readonly struct DrawInstance {
        public Texture2D Texture2D { get; init; }
        public Vector2 Position { get; init; }
        public Rectangle DestinationRectangle { get; init; }
        public Rectangle? SourceRectangle { get; init; }
        public Color Color { get; init; }
        public float Rotation { get; init; }
        public Vector2 Origin { get; init; }
        public Vector2 Scale { get; init; }
        public SpriteEffects SpriteEffects { get; init; }
        public float DepthLayer { get; init; }
    }
}