using Microsoft.Xna.Framework;
using Project_Template.Source.Core.DependencyInjector;
using Project_Template.Source.Core.DrawPass;
using Project_Template.Source.Data.Interfaces;

namespace Project_Template.Source.Scenes {
    public abstract class SceneBehaviour : IScene {
        DrawPass drawPass;
        DependencyScope scope;

        public DependencyScope Scope {
            get => scope;
            set {
                if (scope is null) {
                    scope = value;
                } else {
                    throw new("Scope has already been defined");
                }
            }
        }

        public DrawPass DrawPass {
            get => drawPass;
            set {
                if (drawPass is null) {
                    drawPass = value;
                } else {
                    throw new("DrawPass has already been defined");
                }
            }
        }

        public void Initialize() {
            DrawPass = ConfigureDrawPass();
            Scope = RegisterDependencies();

            Scope.SetDrawPass(DrawPass);
        }


        public virtual DependencyScope RegisterDependencies() => new DependencyInjector().End();

        public virtual DrawPass ConfigureDrawPass() => new();

        public virtual void Load() {
        }

        public virtual void Unload() {
        }

        public virtual void Update(GameTime time) {
        }
    }
}