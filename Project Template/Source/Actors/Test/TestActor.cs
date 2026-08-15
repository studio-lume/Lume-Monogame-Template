using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project_Template.Source.Components.Sprite;
using Project_Template.Source.Components.Transform;
using Project_Template.Source.Data.Enums;
using Project_Template.Source.Data.Interfaces;

namespace Project_Template.Source.Actors.Test {
    public class TestActor(IActorService service) : ActorBehaviour(DrawPassId.Test) {
        public Transform Transform;
        public Sprite Sprite;

        public override void Start() {
            Transform = AddComponent(new Transform());
            Sprite = AddComponent(new Sprite("sample_texture"));
            
            service.Create<BActor>();
        }

        public override void Update(float deltaTime) {
            Transform.Rotation += 0.01f;
        }

        public override void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(
                Sprite.GetTexture(),
                Transform.Bounds,
                null,
                Color.White,
                Transform.Rotation,
                Sprite.GetOrigin(0f, 0f),
                SpriteEffects.None,
                0f
            );
        }
    }
}