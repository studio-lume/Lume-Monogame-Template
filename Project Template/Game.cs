using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Project_Template.Source.Scenes;

namespace Project_Template {
    public class Game : Microsoft.Xna.Framework.Game {
        public Game() {
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            IsFixedTimeStep = false;

            Global.SceneManager = new();
            Global.ContentManager = Content;
            Global.GraphicsDeviceManager = new(this) {
                SynchronizeWithVerticalRetrace = false,
                IsFullScreen = true,
                PreferredBackBufferWidth = 1920,
                PreferredBackBufferHeight = 1080
            };
        }

        protected override void Initialize() {
            Global.GraphicsDevice = GraphicsDevice;

            // starter scene
            Global.SceneManager.LoadScene<TestScene>();

            base.Initialize();
        }

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape)) {
                Exit();
            }

            Global.SceneManager.UpdateScenes(gameTime);
            Console.WriteLine($"Fps: {1 / (float)gameTime.ElapsedGameTime.TotalSeconds}");

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.White);

            Global.SceneManager.DrawScenes();

            base.Draw(gameTime);
        }
    }
}