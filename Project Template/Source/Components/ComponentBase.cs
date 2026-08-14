using Project_Template.Source.Actors;
using Project_Template.Source.Data.Interfaces;

namespace Project_Template.Source.Components {
    public abstract class ComponentBase : IComponent {
        public virtual void Initialize(ActorBehaviour actor) {
        }

        public virtual void Update(float deltaTime) {
        }
    }
}