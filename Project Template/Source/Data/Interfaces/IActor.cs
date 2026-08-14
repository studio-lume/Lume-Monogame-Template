using Microsoft.Xna.Framework.Graphics;

namespace Project_Quarry.Source.Data.Interfaces {
    public interface IActor {
        public void UpdateComponents(float deltaTime);

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