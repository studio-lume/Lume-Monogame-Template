using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Project_Template.Source.Actors.Test;
using Project_Template.Source.Core.DependencyInjector;
using Project_Template.Source.Core.DrawPass;
using Project_Template.Source.Core.SceneManager;
using Project_Template.Source.Data.Interfaces;
using Project_Template.Source.Scenes.TestScene;
using Project_Template.Source.Services;

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
                // IsFullScreen = true,
                PreferredBackBufferWidth = 1920,
                PreferredBackBufferHeight = 1080
            };
        }

        protected override void Initialize() {
            Global.GraphicsDevice = GraphicsDevice;
            
            Global.SceneManager.LoadScene<TestScene>();
            
            base.Initialize();
        }

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape)) {
                Exit();
            }
            
            Global.SceneManager.UpdateScenes((float)gameTime.ElapsedGameTime.TotalSeconds);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.White);
            
            Global.SceneManager.DrawScenes();
            
            base.Draw(gameTime);
        }
    }
}