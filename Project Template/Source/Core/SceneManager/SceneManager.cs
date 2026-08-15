using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Project_Template.Source.Data.Interfaces;

namespace Project_Template.Source.Core.SceneManager {
    public class SceneManager {
        readonly List<IScene> activeScenes = [];

        public void LoadScene<T>() where T : class, IScene, new() {
            var scene = new T();

            scene.Initialize();
            activeScenes.Add(scene);
            scene.Load();
        }

        public void UnloadScene<T>() where T : class, IScene {
            for (var i = activeScenes.Count - 1; i >= 0; i--) {
                if (activeScenes[i] is not T scene) {
                    continue;
                }

                scene.Unload();
                activeScenes.RemoveAt(i);
                return;
            }
        }

        public void UpdateScenes(GameTime time) {
            foreach (var scene in activeScenes) {
                scene.DrawPass.Update((float)time.ElapsedGameTime.TotalSeconds);
                scene.Update(time);
            }
        }

        public void DrawScenes() {
            foreach (var scene in activeScenes) {
                scene.DrawPass.Draw();
            }
        }
    }
}