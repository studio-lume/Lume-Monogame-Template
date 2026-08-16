using Project_Template.Source.Core.DependencyInjector;
using Project_Template.Source.Data.Interfaces;

namespace Project_Template.Source.Core {
    public class ScenePipeline : IScenePipeline, IScenePipelineInternal {
        readonly DependencyScope dependencyScope;
        readonly DrawPass drawPass;

        public ScenePipeline(DependencyScope dependencyScope, DrawPass drawPass) {
            this.dependencyScope = dependencyScope;
            this.drawPass = drawPass;

            if (dependencyScope.TryGetService(out IActorService actorService)) {
                actorService.ScenePipeline = this;
            }
        }

        /// <summary>
        ///     Creates an instance of the specified type using the scene's dependency scope.
        ///     If the created instance is an actor, it is automatically registered with the
        ///     scene's draw pass.
        /// </summary>
        /// <remarks>
        ///     Constructor dependencies are resolved automatically by the scene's
        ///     <see cref="DependencyScope" />. Actors are registered with the draw pass
        ///     after they have been created.
        /// </remarks>
        /// <example>
        ///     <code>
        /// var actor = scenePipeline.Create&lt;ExampleActor&gt;();
        /// </code>
        /// </example>
        /// <typeparam name="T">The type of object to create.</typeparam>
        /// <returns>
        ///     A newly created instance of <typeparamref name="T" /> with its dependencies
        ///     resolved.
        /// </returns>
        public T Create<T>() where T : class {
            var instance = dependencyScope.Create<T>();
            if (instance is IActorInternal actor) {
                actor.CoreRegisterDrawPass(drawPass);
            }

            return instance;
        }

        /// <summary>
        ///     Attempts to retrieve a registered service of the specified type.
        /// </summary>
        /// <typeparam name="T">
        ///     The type of service to retrieve.
        /// </typeparam>
        /// <param name="service">
        ///     When this method returns, contains the registered service if found;
        ///     otherwise, the default value of <typeparamref name="T" />.
        /// </param>
        /// <returns>
        ///     <see langword="true" /> if a service of the specified type is registered;
        ///     otherwise, <see langword="false" />.
        /// </returns>
        public bool TryGetService<T>(out T service) where T : class {
            var state = dependencyScope.TryGetService<T>(out var scopeService);
            service = scopeService;
            return state;
        }

        void IScenePipelineInternal.ClearDrawPasses() => drawPass.ClearPasses();

        void IScenePipelineInternal.Update(float deltaTime) => drawPass.Update(deltaTime);

        void IScenePipelineInternal.Draw() => drawPass.Draw();
    }
}