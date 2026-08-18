using Project_Template.Source.Actors;
using Project_Template.Source.Core;
using Project_Template.Source.Core.Behaviours;
using Project_Template.Source.Core.DependencyInjector;
using Project_Template.Source.Data.Enums;
using Project_Template.Source.Data.Interfaces;
using Project_Template.Source.Services.ActorService;
using Project_Template.Source.Services.LoggerService;

namespace Project_Template.Source.Scenes {
    public class TestScene : SceneBehaviour {
        public override DependencyScope RegisterDependencies() => new DependencyInjector()
            .AddService<IActorService, ActorService>()
            .AddService<ILoggerService, LoggerService>()
            .End();

        public override DrawPass ConfigureDrawPass() => new DrawPass()
            .NewBatch()
            .AddPass(DrawPassId.Test)
            .AddPass(DrawPassId.Test2)
            .AddPass(DrawPassId.Blocks);

        public override void Load() {
            for (var x = 0; x < 20; x++)
            for (var y = 0; y < 20; y++) {
                Scene.Create<TestActor>().Transform.Position = new(x * 100, y * 100);
            }
        }
    }
}