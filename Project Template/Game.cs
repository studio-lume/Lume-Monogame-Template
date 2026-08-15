using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Project_Template.Source.Actors.Test;
using Project_Template.Source.Core.DependencyInjector;
using Project_Template.Source.Data.Interfaces;
using Project_Template.Source.Services;

namespace Project_Template {
    public class Game : Microsoft.Xna.Framework.Game {
        public Game() {
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            IsFixedTimeStep = false;

            Global.ContentManager = Content;
            Global.GraphicsDeviceManager = new(this) {
                SynchronizeWithVerticalRetrace = false,
                // IsFullScreen = true,
                PreferredBackBufferWidth = 1920,
                PreferredBackBufferHeight = 1080
            };
            Global.DrawPass = new();
        }

        protected override void Initialize() {
            Global.GraphicsDevice = GraphicsDevice;

            var scope = new DependencyInjector()
                .AddService<IActorService, ActorService>()
                .End();
            
            for (var x = 0; x < 50; x++)
            for (var y = 0; y < 50; y++) {
                scope.Create<TestActor>().Transform.Position = new(x * 100, y * 100);
            }

            base.Initialize();
        }

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape)) {
                Exit();
            }

            Global.DrawPass.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            Console.WriteLine($"FPS: {1 / gameTime.ElapsedGameTime.TotalSeconds}");

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.White);

            Global.DrawPass.Draw();

            base.Draw(gameTime);
        }
    }
}