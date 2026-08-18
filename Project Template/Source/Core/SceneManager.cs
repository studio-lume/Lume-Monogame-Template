using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Project_Template.Source.Data.Enums;
using Project_Template.Source.Data.Interfaces;
using Project_Template.Source.Services.LoggerService;

namespace Project_Template.Source.Core {
    public class SceneManager {
        readonly List<Type> activeSceneTypes = [];
        readonly List<IScene> activeScenes = [];

        /// <summary>
        ///     Creates, initializes, and loads a scene of the specified type.
        /// </summary>
        /// <remarks>
        ///     The scene is initialized before being added to the active scene collection.
        ///     Once registered, its <see cref="IScene.Load" /> method is invoked.
        /// </remarks>
        /// <typeparam name="T">The type of scene to create.</typeparam>
        public void LoadScene<T>() where T : class, IScene, new() {
            if (activeSceneTypes.Contains(typeof(T))) {
                new LoggerService().Log(
                    LogLevel.Error,
                    LogCategory.Scene,
                    "Scene has already been registered",
                    new LoggerContext()
                        .AddSection("Scene Information")
                        .Add("Scene Name", typeof(T).Name));
            }

            var scene = new T();

            scene.Initialize();
            activeScenes.Add(scene);
            activeSceneTypes.Add(typeof(T));
            scene.Load();
        }

        /// <summary>
        ///     Unloads the first active scene matching the specified type.
        /// </summary>
        /// <remarks>
        ///     The scene's <see cref="IScene.Unload" /> method is invoked before it is
        ///     removed from the active scene collection.
        ///     If multiple scenes of the same type are active, the most recently loaded
        ///     matching scene is unloaded.
        /// </remarks>
        /// <typeparam name="T">The type of scene to unload.</typeparam>
        public void UnloadScene<T>() where T : class, IScene {
            for (var i = activeScenes.Count - 1; i >= 0; i--) {
                if (activeScenes[i] is not T scene) {
                    continue;
                }

                scene.Unload();
                ((IScenePipelineInternal)scene.Scene).ClearDrawPasses();
                activeScenes.RemoveAt(i);
                activeSceneTypes.Remove(typeof(T));
                return;
            }
        }

        /// <summary>
        ///     Unloads all current active scenes
        /// </summary>
        public void UnloadAllScenes() {
            foreach (var scene in activeScenes.ToList()) {
                scene.Unload();
                ((IScenePipelineInternal)scene.Scene).ClearDrawPasses();
                activeScenes.Remove(scene);
                activeSceneTypes.Remove(scene.GetType());
            }
        }

        /// <summary>
        ///     Updates all the scenes within the manager and passes along a GameTime object.
        /// </summary>
        /// <remarks>
        ///     The scene itself is first updated.
        ///     Then after all the actors are updated
        /// </remarks>
        /// <param name="time">The GameTime from the current update loop</param>
        public void UpdateScenes(GameTime time) {
            foreach (var scene in activeScenes) {
                scene.Update(time);
                ((IScenePipelineInternal)scene.Scene).Update((float)time.ElapsedGameTime.TotalSeconds);
            }
        }

        /// <summary>
        ///     Draws the contents of all the scenes to the screen.
        /// </summary>
        public void DrawScenes() {
            foreach (var scene in activeScenes) {
                ((IScenePipelineInternal)scene.Scene).Draw();
            }
        }
    }
}