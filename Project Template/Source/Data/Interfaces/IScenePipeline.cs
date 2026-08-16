namespace Project_Template.Source.Data.Interfaces {
    public interface IScenePipelineInternal {
        public void Update(float deltaTime);
        public void Draw();
        public void ClearDrawPasses();
    }

    public interface IScenePipeline {
        public T Create<T>() where T : class;
    }
}