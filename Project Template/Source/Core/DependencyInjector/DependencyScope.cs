using System;
using Microsoft.Extensions.DependencyInjection;

namespace Project_Template.Source.Core.DependencyInjector {
    public sealed class DependencyScope(ServiceCollection serviceCollection) {
        readonly ServiceProvider serviceProvider =
            serviceCollection.BuildServiceProvider();

        public T Create<T>() where T : class => ActivatorUtilities.CreateInstance<T>(serviceProvider);
        
        public void Dispose() {
            serviceProvider.Dispose();
        }
    }
}