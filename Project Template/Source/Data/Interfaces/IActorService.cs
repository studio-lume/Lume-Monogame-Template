namespace Project_Template.Source.Data.Interfaces {
    public interface IActorService {
        public IScenePipeline ScenePipeline { set; }
        public T Create<T>() where T : class;
    }
}