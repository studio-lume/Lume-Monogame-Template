using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Project_Template.Source.Data.Interfaces;

namespace Project_Template.Source.Core.SceneManager {
    public class SceneManager {
        readonly List<IScene> activeScenes = [];

        public void LoadScene<T>() where T : class, IScene, new() {
            var scene = new T();
            var drawPass = scene.ConfigureDrawPass();
            scene.DrawPass = drawPass;
            
            var scope = scene.RegisterDependencies();
            scope.SetDrawPass(drawPass);
            scene.Scope = scope;
            
            activeScenes.Add(scene);
            scene.Load();
        }

        public void UpdateScenes(float deltaTime) {
            foreach (var scene in activeScenes) {
                scene.DrawPass.Update(deltaTime);
            }
        }

        public void DrawScenes() {
            foreach (var scene in activeScenes) {
                scene.DrawPass.Draw();
            }
        }
    }
}