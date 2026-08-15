using Microsoft.Xna.Framework;
using Project_Template.Source.Actors.Test;
using Project_Template.Source.Core.DependencyInjector;
using Project_Template.Source.Core.DrawPass;
using Project_Template.Source.Data.Interfaces;
using Project_Template.Source.Services;

namespace Project_Template.Source.Scenes.TestScene {
    public class TestScene : SceneBehaviour {
        public override DependencyScope RegisterDependencies() => new DependencyInjector()
            .AddService<IActorService, ActorService>()
            .End();

        public override DrawPass ConfigureDrawPass() => base.ConfigureDrawPass();

        public override void Load() {
            for (var x = 0; x < 50; x++)
            for (var y = 0; y < 50; y++) {
                Scope.Create<TestActor>().Transform.Position = new(x * 100, y * 100);
            }
        }
    }
}