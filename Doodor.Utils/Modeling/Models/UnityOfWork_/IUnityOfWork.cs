using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Doodor.Utils.Modeling.Models
{
    public interface IUnityOfWork
    {
        Task<IUnityOfWorkTransaction> BeginTransactionAsync(CancellationToken cancellationToken);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        void EnsureIsAttached<TEntity>(TEntity entity) where TEntity : Entity;
        void SetModified<TEntity>(TEntity entity) where TEntity : Entity;

        bool CheckSameTarget(IEnumerable<IUnityOfWork> others);
        bool CheckSameTarget(params IUnityOfWork[] others);

        void EnsureSameTarget(IEnumerable<IUnityOfWork> others);
        void EnsureSameTarget(params IUnityOfWork[] others);
    }
}