using Project_Template.Source.Core.DependencyInjector;

namespace Project_Template.Source.Data.Interfaces {
    public interface IActorService {
        public T Create<T>() where T : class, IActor;
    }
}