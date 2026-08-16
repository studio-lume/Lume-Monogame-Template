using Microsoft.Xna.Framework.Graphics;
using Project_Template.Source.Core;

namespace Project_Template.Source.Data.Interfaces {
    public interface IActorInternal {
        public void CoreUpdateComponents(float deltaTime);
        public void CoreRegisterDrawPass(DrawPass drawPass);
    }

    public interface IActor {
        public void Start() {
        }

        public void End() {
        }

        public void Update(float deltaTime) {
        }

        public void Draw(SpriteBatch spriteBatch) {
        }
    }
}