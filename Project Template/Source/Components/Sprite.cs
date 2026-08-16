using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project_Template.Source.Core.Behaviours;

namespace Project_Template.Source.Components {
    public class Sprite(string textureName) : ComponentBehaviour {
        Texture2D texture;

        public override void Initialize(ActorBehaviour actor) {
            texture = Global.ContentManager.Load<Texture2D>(textureName);
        }

        /// <summary>
        ///     Calculates a pixel-space origin point on the texture based on normalized coordinates
        ///     ranging from 0.0 (left/bottom) to 1.0 (right/top).
        /// </summary>
        /// <param name="normalizedX">The horizontal offset from 0.0 (left) to 1.0 (right).</param>
        /// <param name="normalizedY">The vertical offset from 0.0 (bottom) to 1.0 (top).</param>
        /// <returns>A <see cref="Vector2" /> containing the pixel position of the origin.</returns>
        /// <remarks>
        ///     Standard coordinate mapping:
        ///     <list type="bullet">
        ///         <item>
        ///             <description><c>(0, 0)</c> — Bottom-Left <c>(0, Height)</c></description>
        ///         </item>
        ///         <item>
        ///             <description><c>(0.5, 0.5)</c> — Center <c>(Width / 2, Height / 2)</c></description>
        ///         </item>
        ///         <item>
        ///             <description><c>(1, 1)</c> — Top-Right <c>(Width, 0)</c></description>
        ///         </item>
        ///     </list>
        /// </remarks>
        public Vector2 GetOrigin(float normalizedX, float normalizedY) =>
            new(normalizedX * texture.Width, (1f - normalizedY) * texture.Height);

        public Texture2D GetTexture() => texture;
    }
}