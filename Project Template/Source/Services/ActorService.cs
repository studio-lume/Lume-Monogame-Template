using System;
using Microsoft.Extensions.DependencyInjection;
using Project_Template.Source.Data.Interfaces;
using Project_Template.Source.Core.DependencyInjector;

namespace Project_Template.Source.Services {
    /// <summary>
    /// Provides actor management operations from within actors.
    /// </summary>
    /// <remarks>
    /// The <see cref="DependencyScope"/> is not directly accessible from actors.
    /// This service provides access to actor management operations, such as
    /// creating new actors through the current dependency scope.
    /// </remarks>
    /// <example>
    /// <code>
    /// public class ExampleActor(IActorService actorService)
    ///     : ActorBehaviour(DrawPassId.Example)
    /// {
    ///     public override void Start()
    ///     {
    ///         actorService.Create&lt;ExampleActor&gt;();
    ///     }
    /// }
    /// </code>
    /// </example>
    public class ActorService(IServiceProvider serviceProvider) : IActorService {
        /// <summary>
        /// Creates a new actor using the current dependency injection container.
        /// Constructor dependencies are resolved automatically.
        /// </summary>
        /// <example>
        /// <code>
        /// var actor = actorService.Create&lt;ExampleActor&gt;();
        /// </code>
        /// </example>
        /// <typeparam name="T">The type of actor to create.</typeparam>
        /// <returns>A new instance of the specified actor with its dependencies injected.</returns>
        public T Create<T>() where T : class, IActor =>
            ActivatorUtilities.CreateInstance<T>(serviceProvider);
    }
}