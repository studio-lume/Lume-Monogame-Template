using Project_Template.Source.Core.Behaviours;

namespace Project_Template.Source.Components {
    public class Camera : ComponentBehaviour {
        public override void Initialize(ActorBehaviour actor) {
            if (!actor.TryGetComponent(out Transform transform)) {
                
            }
        }
    }
}