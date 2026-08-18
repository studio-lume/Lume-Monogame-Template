using Project_Template.Source.Core.Behaviours;

namespace Project_Template.Source.Data.Interfaces {
    public interface IComponent {
        public void Initialize(ActorBehaviour actor) {
        }

        public void Update(float deltaTime) {
        }

        public void End() {
        }
    }
}