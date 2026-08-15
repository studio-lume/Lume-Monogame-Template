using Microsoft.Extensions.DependencyInjection;

namespace Project_Template.Source.Core.DependencyInjector {
    public class DependencyInjector {
        readonly ServiceCollection serviceCollection = new();

        /// <summary>
        ///     Registers a service with the dependency injection container.
        ///     The service is registered under the specified interface and resolved
        ///     using the specified implementation type.
        /// </summary>
        /// <example>
        ///     <code>
        /// new DependencyInjector()
        ///     .AddService&lt;IExampleService, ExampleService&gt;()
        ///     .End();
        /// </code>
        /// </example>
        /// <typeparam name="TInterface">The service interface used for resolving the service.</typeparam>
        /// <typeparam name="TClass">The concrete implementation of the service.</typeparam>
        /// <returns>The current <see cref="DependencyInjector" /> instance for method chaining.</returns>
        public DependencyInjector AddService<TInterface, TClass>()
            where TInterface : class
            where TClass : class, TInterface {
            serviceCollection.AddSingleton<TInterface, TClass>();
            return this;
        }

        /// <summary>
        ///     Registers an existing instance with the dependency injection container.
        ///     The same instance will be returned whenever the registered type is resolved.
        ///     This is useful for registering objects that are created outside the container.
        /// </summary>
        /// <example>
        ///     <code>
        /// var drawPass = new DrawPass();
        /// 
        /// new DependencyInjector()
        ///     .AddInstance(drawPass)
        ///     .End();
        /// </code>
        /// </example>
        /// <param name="value">The instance to register with the container.</param>
        /// <typeparam name="T">The type under which the instance is registered.</typeparam>
        /// <returns>The current <see cref="DependencyInjector" /> instance for method chaining.</returns>
        public DependencyInjector AddInstance<T>(T value)
            where T : class {
            serviceCollection.AddSingleton(value);
            return this;
        }

        /// <summary>
        ///     Builds and returns a <see cref="DependencyScope" /> from the registered services.
        ///     After calling this method, additional services should not be registered with
        ///     this injector.
        /// </summary>
        /// <returns>A dependency scope containing the registered services.</returns>
        public DependencyScope End() => new(serviceCollection);
    }
}