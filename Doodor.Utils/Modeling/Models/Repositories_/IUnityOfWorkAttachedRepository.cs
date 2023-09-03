namespace Doodor.Utils.Modeling.Models
{
    public interface IUnityOfWorkAttachedRepository : IRepository
    {
        IUnityOfWork UnityOfWork { get; }
    }
}