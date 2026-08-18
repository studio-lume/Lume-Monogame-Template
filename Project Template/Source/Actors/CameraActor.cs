using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Project_Template.Source.Components;
using Project_Template.Source.Core;
using Project_Template.Source.Core.Behaviours;
using Project_Template.Source.Data.Enums;

namespace Project_Template.Source.Actors {
    public class CameraActor() : ActorBehaviour {
        Camera camera;
        Transform transform;

        public override void Start() {
            transform = AddComponent(new Transform());
            camera = AddComponent(new Camera(100));
        }

        public override void Update(float deltaTime) {
            var keyboard = Keyboard.GetState();
            var movement = Vector2.Zero;

            if (keyboard.IsKeyDown(Keys.W)) {
                movement.Y -= 1f;
            }

            if (keyboard.IsKeyDown(Keys.S)) {
                movement.Y += 1f;
            }

            if (keyboard.IsKeyDown(Keys.A)) {
                movement.X -= 1f;
            }

            if (keyboard.IsKeyDown(Keys.D)) {
                movement.X += 1f;
            }

            if (movement != Vector2.Zero) {
                movement.Normalize();
            }

            const float speed = 500f;
            movement = movement * speed * deltaTime;

            transform.Position += new Vector2I(movement.X, movement.Y);
        }
    }
}