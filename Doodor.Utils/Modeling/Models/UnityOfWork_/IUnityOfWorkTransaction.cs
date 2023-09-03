using System;

namespace Doodor.Utils.Modeling.Models
{
    public interface IUnityOfWorkTransaction : IDisposable
    {
        Guid? TransactionId { get; }
        void Commit();
        void Rollback();
    }
}