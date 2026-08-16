using Microsoft.Extensions.DependencyInjection;

namespace Project_Template.Source.Core.DependencyInjector {
    public class DependencyInjector {
        readonly ServiceCollection serviceCollection = new();

        public DependencyInjector AddService<TInterface, TClass>()
            where TInterface : class
            where TClass : class, TInterface {
            serviceCollection.AddSingleton<TInterface, TClass>();
            return this;
        }

        public DependencyInjector AddInstance<T>(T value)
            where T : class {
            serviceCollection.AddSingleton(value);
            return this;
        }

        public DependencyScope End() {
            var scope = new DependencyScope(serviceCollection);
            return scope;
        }
    }
}