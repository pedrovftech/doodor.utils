using Doodor.Utils.Modeling.Models;
using Microsoft.EntityFrameworkCore;

namespace Doodor.Utils.EntityFrameworkCore
{
    public class EFUnityOfWorkAttachedRepository : IUnityOfWorkAttachedRepository
    {
        public EFUnityOfWorkAttachedRepository(EFUnityOfWork efUnityOfWork)
        {
            UnityOfWork = efUnityOfWork;
            EfUnityOfWorkImpl = efUnityOfWork.UnityOfWorkImpl;
        }

        protected DbContext EfUnityOfWorkImpl { get; private set; }
        public IUnityOfWork UnityOfWork { get; private set; }
    }
}