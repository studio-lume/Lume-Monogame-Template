using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Project_Template.Source.Data.Interfaces;

namespace Project_Template.Source.Core {
    public class SceneManager {
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
            var scene = new T();

            scene.Initialize();
            activeScenes.Add(scene);
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

                ((IScenePipelineInternal)scene.Scene).ClearDrawPasses();
                scene.Unload();
                activeScenes.RemoveAt(i);
                return;
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