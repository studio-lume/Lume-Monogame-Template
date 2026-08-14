using Project_Quarry.Source.Actors;

namespace Project_Quarry.Source.Data.Interfaces {
    public interface IComponent {
        public void Initialize(ActorBehaviour actor) {
        }

        public void Update(float deltaTime) {
        }
    }
}