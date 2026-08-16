using Microsoft.Xna.Framework;
using Project_Template.Source.Core;

namespace Project_Template.Source.Data.Interfaces {
    public interface IScene {
        public ScenePipeline Scene { get; set; }

        public void Initialize();
        public void Load();
        public void Unload();
        public void Update(GameTime time);
    }
}