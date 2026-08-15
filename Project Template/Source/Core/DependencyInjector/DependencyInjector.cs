using System;
using Microsoft.Extensions.DependencyInjection;
using Project_Template.Source.Data.Interfaces;

namespace Project_Template.Source.Core.DependencyInjector {
    public class DependencyInjector {
        readonly ServiceCollection serviceCollection = new();
        
        public DependencyInjector AddService<TInterface, TClass>()
            where TInterface : class
            where TClass : class, TInterface 
        {
            serviceCollection.AddSingleton<TInterface, TClass>();
            return this;
        }

        public DependencyInjector AddInstance<T>(T value)
            where T : class 
        {
            serviceCollection.AddSingleton(value);
            return this;
        }

        public DependencyScope End() => new(serviceCollection);
    }
}