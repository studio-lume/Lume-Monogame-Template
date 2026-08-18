using Microsoft.Extensions.DependencyInjection;

namespace Project_Template.Source.Core.DependencyInjector {
    public sealed class DependencyScope(ServiceCollection serviceCollection) {
        readonly ServiceProvider serviceProvider =
            serviceCollection.BuildServiceProvider();

        public T Create<T>() where T : class {
            var instance = ActivatorUtilities.CreateInstance<T>(serviceProvider);
            return instance;
        }

        public bool TryGetService<T>(out T service) where T : class {
            service = serviceProvider.GetService<T>();
            return service is not null;
        }

        public void Dispose() {
            serviceProvider.Dispose();
        }
    }
}