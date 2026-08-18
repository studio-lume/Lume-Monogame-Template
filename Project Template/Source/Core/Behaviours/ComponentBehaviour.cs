using Project_Template.Source.Data.Interfaces;

namespace Project_Template.Source.Core.Behaviours {
    public abstract class ComponentBehaviour : IComponent {
        public virtual void Initialize(ActorBehaviour actor) {
        }

        public virtual void Update(float deltaTime) {
        }

        public virtual void End() {
        }
    }
}