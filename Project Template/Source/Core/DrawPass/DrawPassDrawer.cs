using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project_Template.Source.Data.Enums;

namespace Project_Template.Source.Core.DrawPass {
    public sealed class Drawer(DrawPassPass pass) {
        public void Draw(
            DrawPassId drawPassId,
            Texture2D texture,
            Vector2 position,
            Color color) {
            Draw(
                drawPassId,
                texture,
                position,
                null,
                color,
                0f,
                Vector2.Zero,
                Vector2.One,
                SpriteEffects.None,
                0f);
        }

        public void Draw(
            DrawPassId drawPassId,
            Texture2D texture,
            Vector2 position,
            Rectangle? sourceRectangle,
            Color color) {
            Draw(
                drawPassId,
                texture,
                position,
                sourceRectangle,
                color,
                0f,
                Vector2.Zero,
                Vector2.One,
                SpriteEffects.None,
                0f);
        }

        public void Draw(
            DrawPassId drawPassId,
            Texture2D texture,
            Vector2 position,
            Rectangle? sourceRectangle,
            Color color,
            float rotation,
            Vector2 origin,
            float scale,
            SpriteEffects effects,
            float layerDepth) {
            Draw(
                drawPassId,
                texture,
                position,
                sourceRectangle,
                color,
                rotation,
                origin,
                new Vector2(scale),
                effects,
                layerDepth);
        }

        public void Draw(
            DrawPassId drawPassId,
            Texture2D texture,
            Vector2 position,
            Rectangle? sourceRectangle,
            Color color,
            float rotation,
            Vector2 origin,
            Vector2 scale,
            SpriteEffects effects,
            float layerDepth) {
            if (!pass.DrawOrder.TryGetValue(drawPassId, out var draws)) {
                draws = [];
                pass.DrawOrder.Add(drawPassId, draws);
            }

            draws.Add(new() {
                Texture2D = texture,
                Position = position,
                SourceRectangle = sourceRectangle,
                Color = color,
                Rotation = rotation,
                Origin = origin,
                Scale = scale,
                SpriteEffects = effects,
                DepthLayer = layerDepth
            });
        }

        public void Draw(
            DrawPassId drawPassId,
            Texture2D texture,
            Rectangle destinationRectangle,
            Color color) {
            Draw(
                drawPassId,
                texture,
                destinationRectangle,
                null,
                color,
                0f,
                Vector2.Zero,
                SpriteEffects.None,
                0f);
        }

        public void Draw(
            DrawPassId drawPassId,
            Texture2D texture,
            Rectangle destinationRectangle,
            Rectangle? sourceRectangle,
            Color color) {
            Draw(
                drawPassId,
                texture,
                destinationRectangle,
                sourceRectangle,
                color,
                0f,
                Vector2.Zero,
                SpriteEffects.None,
                0f);
        }

        public void Draw(
            DrawPassId drawPassId,
            Texture2D texture,
            Rectangle destinationRectangle,
            Rectangle? sourceRectangle,
            Color color,
            float rotation,
            Vector2 origin,
            SpriteEffects effects,
            float layerDepth) {
            if (!pass.DrawOrder.TryGetValue(drawPassId, out var draws)) {
                draws = [];
                pass.DrawOrder.Add(drawPassId, draws);
            }

            draws.Add(new() {
                Texture2D = texture,
                DestinationRectangle = destinationRectangle,
                SourceRectangle = sourceRectangle,
                Color = color,
                Rotation = rotation,
                Origin = origin,
                SpriteEffects = effects,
                DepthLayer = layerDepth
            });
        }
    }
}