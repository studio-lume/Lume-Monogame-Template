using Microsoft.Xna.Framework;
using Project_Template.Source.Core.DependencyInjector;
using Project_Template.Source.Data.Interfaces;

namespace Project_Template.Source.Core.Behaviours {
    public abstract class SceneBehaviour : IScene {
        public ScenePipeline Scene { get; set; }
        DrawPass drawPass;
        DependencyScope scope;

        public void Initialize() {
            drawPass = ConfigureDrawPass();
            scope = RegisterDependencies();
            Scene = new(scope, drawPass);
        }

        /// <summary>
        ///     Creates and configures the dependency scope used by this scene.
        /// </summary>
        /// <remarks>
        ///     Override this method to register scene-specific services and dependencies.
        /// </remarks>
        /// <returns>The dependency scope used by the scene.</returns>
        public virtual DependencyScope RegisterDependencies() => new DependencyInjector.DependencyInjector().End();

        /// <summary>
        ///     Creates and configures the draw pass used by this scene.
        /// </summary>
        /// <remarks>
        ///     Override this method to configure batches and draw passes for the scene.
        /// </remarks>
        /// <returns>The draw pass used by the scene.</returns>
        public virtual DrawPass ConfigureDrawPass() => new();

        public virtual void Load() {
        }

        public virtual void Unload() {
        }

        public virtual void Update(GameTime time) {
        }
    }
}