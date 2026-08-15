using System;
using Microsoft.Extensions.DependencyInjection;
using Project_Template.Source.Actors;

namespace Project_Template.Source.Core.DependencyInjector {
    public sealed class DependencyScope(ServiceCollection serviceCollection) {
        readonly ServiceProvider serviceProvider =
            serviceCollection.BuildServiceProvider();

        DrawPass.DrawPass drawPass;
        public void SetDrawPass(DrawPass.DrawPass drawPass) => this.drawPass = drawPass;

        public T Create<T>() where T : class {
            var instance = ActivatorUtilities.CreateInstance<T>(serviceProvider);
            // Really, really hacky fix,
            // But I have no other way of inserting the DrawPass into each actor,
            if (instance is ActorBehaviour actor) {
                actor.RegisterDrawPass(drawPass);
            }

            return instance;
        }

        public void Dispose() {
            serviceProvider.Dispose();
        }
    }
}