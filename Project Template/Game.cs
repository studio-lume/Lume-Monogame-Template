using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Project_Quarry.Source.Actors.Test;
using Project_Quarry.Source.Temp;

namespace Project_Quarry {
    public class Game : Microsoft.Xna.Framework.Game {
        readonly GraphicsDeviceManager graphics;

        public Game() {
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            IsFixedTimeStep = false;

            graphics = new(this) {
                SynchronizeWithVerticalRetrace = false,
                // IsFullScreen = true,
                PreferredBackBufferWidth = 1920,
                PreferredBackBufferHeight = 1080
            };

            ContentService.Service = Content;
            PassService.Service = new();
        }

        protected override void Initialize() {
            DeviceService.Service = GraphicsDevice;

            for (var x = 0; x < 100; x++)
            for (var y = 0; y < 100; y++) {
                new TestActor().transform.Position = new(x * 100, y * 100);
            }

            base.Initialize();
        }

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape)) {
                Exit();
            }

            PassService.Service.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            Console.WriteLine($"FPS: {1 / gameTime.ElapsedGameTime.TotalSeconds}");

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.White);

            PassService.Service.Draw();

            base.Draw(gameTime);
        }
    }
}