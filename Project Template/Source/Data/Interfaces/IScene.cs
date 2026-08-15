using Microsoft.Xna.Framework;
using Project_Template.Source.Core.DependencyInjector;
using Project_Template.Source.Core.DrawPass;

namespace Project_Template.Source.Data.Interfaces {
    public interface IScene {
        public DependencyScope Scope { get; set; }
        public DrawPass DrawPass { get; set; }
        public virtual DependencyScope RegisterDependencies() => new DependencyInjector().End();
        public virtual DrawPass ConfigureDrawPass() => new DrawPass();
        
        public virtual void Load() {
            
        }

        public virtual void Unload() {
            
        }

        public virtual void Update(GameTime time) {
            
        }

        public virtual void Draw(GameTime time) {
            
        }
    }
}