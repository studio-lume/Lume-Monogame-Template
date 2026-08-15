using Microsoft.Extensions.DependencyInjection;
using Project_Template.Source.Data.Interfaces;

namespace Project_Template.Source.Core.DependencyInjector {
    public class DependencyInjector {
        readonly ServiceCollection serviceCollection = new();
        bool hasActorService;

        public DependencyInjector AddService<TInterface, TClass>()
            where TInterface : class
            where TClass : class, TInterface {
            serviceCollection.AddSingleton<TInterface, TClass>();
            if (typeof(TInterface) == typeof(IActorService)) {
                hasActorService = true;
            }

            return this;
        }

        public DependencyInjector AddInstance<T>(T value)
            where T : class {
            serviceCollection.AddSingleton(value);
            return this;
        }

        public DependencyScope End() {
            var scope = new DependencyScope(serviceCollection);
            if (!hasActorService) {
                return scope;
            }

            var actorService = scope.GetService<IActorService>();
            actorService.Scope = scope;
            return scope;
        }
    }
}