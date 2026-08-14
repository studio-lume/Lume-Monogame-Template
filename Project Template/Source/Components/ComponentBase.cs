using Project_Quarry.Source.Actors;
using Project_Quarry.Source.Data.Interfaces;

namespace Project_Quarry.Source.Components {
    public abstract class ComponentBase : IComponent {
        public virtual void Initialize(ActorBehaviour actor) {
        }

        public virtual void Update(float deltaTime) {
        }
    }
}