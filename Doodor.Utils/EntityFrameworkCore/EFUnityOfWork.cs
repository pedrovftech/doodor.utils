using Doodor.Utils.Modeling.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static Doodor.Utils.ExceptionHandling.Exceptions;

namespace Doodor.Utils.EntityFrameworkCore
{
    public sealed class EFUnityOfWork : IUnityOfWork
    {
        public EFUnityOfWork(DbContext unityOfworkImpl) =>
            UnityOfWorkImpl = unityOfworkImpl ?? throw ArgNullEx(nameof(unityOfworkImpl));


        public DbContext UnityOfWorkImpl { get; private set; }

        public async Task<IUnityOfWorkTransaction> BeginTransactionAsync(CancellationToken cancellationToken) =>
            new EFUnityOfWorkTransaction(await UnityOfWorkImpl.Database.BeginTransactionAsync(cancellationToken));


        public Task<int> SaveChangesAsync(CancellationToken cancellationToken) =>
            UnityOfWorkImpl.SaveChangesAsync(cancellationToken);


        public void EnsureIsAttached<TEntity>(TEntity entity) where TEntity : Entity
        {
            if (entity == null) throw ArgNullEx(nameof(entity));

            var entityEntry = UnityOfWorkImpl.Entry(entity);

            if (entityEntry.State == EntityState.Detached)
                UnityOfWorkImpl.Attach(entity);
        }

        public void SetModified<TEntity>(TEntity entity) where TEntity : Entity
        {
            if (entity == null) throw ArgNullEx(nameof(entity));

            EnsureIsAttached(entity);
            var entityEntry = UnityOfWorkImpl.Entry(entity);
            entityEntry.State = EntityState.Modified;
        }

        public bool CheckSameTarget(IEnumerable<IUnityOfWork> others) =>
            (others ?? throw ArgNullEx(nameof(others)))
                .All(CheckSameTargetInternal);


        public bool CheckSameTarget(params IUnityOfWork[] others) =>
            CheckSameTarget(
                (IEnumerable<IUnityOfWork>)
                (others ?? throw ArgNullEx(nameof(others))));


        public void EnsureSameTarget(IEnumerable<IUnityOfWork> others)
        {
            if (others == null) throw ArgNullEx(nameof(others));

            if (!CheckSameTarget(others))
                throw DoodorUtilEx("UnityOfWorks com alvos diferentes.");
        }

        public void EnsureSameTarget(params IUnityOfWork[] others) =>
            EnsureSameTarget((IEnumerable<IUnityOfWork>)others);


        private bool CheckSameTargetInternal(IUnityOfWork other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));

            if (ReferenceEquals(this, other))
                return true;

            if ((other is EFUnityOfWork otherEfUoW) &&
                ReferenceEquals(UnityOfWorkImpl, otherEfUoW.UnityOfWorkImpl))
                return true;

            return false;
        }
    }
}