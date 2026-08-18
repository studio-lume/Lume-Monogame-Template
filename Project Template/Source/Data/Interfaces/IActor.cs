using Project_Template.Source.Core.DrawPass;

namespace Project_Template.Source.Data.Interfaces {
    public interface IActorInternal {
        public void CoreUpdateComponents(float deltaTime);
        public void CoreRegisterDrawPass(DrawPass drawPass);
        public void CoreEndComponents();
    }

    public interface IActor {
        public void Start() {
        }

        public void End() {
        }

        public void Update(float deltaTime) {
        }

        public void Draw(Drawer drawer) {
        }
    }
}