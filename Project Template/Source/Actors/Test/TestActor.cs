using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project_Quarry.Source.Components.Sprite;
using Project_Quarry.Source.Components.Transform;
using Project_Quarry.Source.Data.Enums;

namespace Project_Quarry.Source.Actors.Test {
    public class TestActor() : ActorBehaviour(DrawPassId.Test) {
        public Transform transform;
        public Sprite sprite;

        public override void Start() {
            transform = AddComponent(new Transform());
            sprite = AddComponent(new Sprite("sample_texture"));
        }

        public override void Update(float deltaTime) {
            transform.Rotation += 0.01f;
        }

        public override void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(
                sprite.GetTexture(),
                transform.Bounds,
                null,
                Color.White,
                transform.Rotation,
                sprite.GetOrigin(0f, 0f),
                SpriteEffects.None,
                0f
            );
        }
    }
}