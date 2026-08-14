using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project_Template.Source.Actors;
using Project_Template.Source.Temp;

namespace Project_Template.Source.Components.Sprite {
    public class Sprite(string textureName) :
        ComponentBase {
        Texture2D texture;

        public override void Initialize(ActorBehaviour actor) {
            texture = ContentService.Service.Load<Texture2D>(textureName);
        }

        /// <summary>
        /// Calculates a pixel-space origin point on the texture based on normalized coordinates
        /// ranging from one side of the bounds [-1.0] to the other side of the bounds [1.0], where origin represents 0.
        /// </summary>
        /// <param name="normalizedX">The horizontal offset from -1.0 (left) to 1.0 (right). A value of 0.0 represents the center.</param>
        /// <param name="normalizedY">The vertical offset from -1.0 (bottom) to 1.0 (top). A value of 0.0 represents the center.</param>
        /// <returns>A <see cref="Vector2" /> containing the pixel position of the origin.</returns>
        /// <remarks>
        ///     Standard coordinate mapping:
        ///     <list type="bullet">
        ///         <item>
        ///             <description><c>(-1, -1)</c> — Bottom-Left <c>(0, Height)</c></description>
        ///         </item>
        ///         <item>
        ///             <description><c>(0, 0)</c> — Center <c>(Width / 2, Height / 2)</c></description>
        ///         </item>
        ///         <item>
        ///             <description><c>(1, 1)</c> — Top-Right <c>(Width, 0)</c></description>
        ///         </item>
        ///     </list>
        /// </remarks>
        public Vector2 GetOrigin(float normalizedX, float normalizedY) =>
            new((normalizedX + 1f) / 2f * texture.Width, (1f - normalizedY) / 2f * texture.Height);

        public Texture2D GetTexture() => texture;
    }
}