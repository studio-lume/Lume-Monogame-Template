using Microsoft.Extensions.DependencyInjection;

namespace Project_Template.Source.Core.DependencyInjector {
    public sealed class DependencyScope(ServiceCollection serviceCollection) {
        readonly ServiceProvider serviceProvider =
            serviceCollection.BuildServiceProvider();

        /// <summary>
        ///     Creates an instance of the specified type using the dependency injection container.
        ///     Constructor dependencies are resolved automatically from the current dependency scope.
        /// </summary>
        /// <example>
        ///     <code>
        /// var actor = scope.Create&lt;ExampleActor&gt;();
        /// </code>
        /// </example>
        /// <typeparam name="T">The type of object to create.</typeparam>
        /// <returns>A new instance of the specified type with its dependencies injected.</returns>
        public T Create<T>() where T : class => ActivatorUtilities.CreateInstance<T>(serviceProvider);

        public void Dispose() {
            serviceProvider.Dispose();
        }
    }
}