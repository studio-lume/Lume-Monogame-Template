using Project_Template.Source.Data.Interfaces;

namespace Project_Template.Source.Services {
    /// <summary>
    ///     Provides actor management operations from within actors.
    /// </summary>
    /// <remarks>
    ///     The <see cref="ScenePipeline" /> is not directly accessible from actors.
    ///     This service provides access to actor management operations, such as
    ///     creating new actors through the current Scene Pipeline.
    /// </remarks>
    /// <example>
    ///     <code>
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
    public class ActorService : IActorService {
        public IScenePipeline ScenePipeline { private get; set; }

        public T Create<T>() where T : class => ScenePipeline.Create<T>();
    }
}