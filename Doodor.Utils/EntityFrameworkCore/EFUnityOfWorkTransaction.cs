using Doodor.Utils.Modeling.Models;
using Microsoft.EntityFrameworkCore.Storage;
using System;

namespace Doodor.Utils.EntityFrameworkCore
{
    public sealed class EFUnityOfWorkTransaction : IUnityOfWorkTransaction
    {
        private readonly IDbContextTransaction _efTransaction;

        public EFUnityOfWorkTransaction(IDbContextTransaction efTransaction) =>
            _efTransaction = efTransaction;

        public Guid? TransactionId => _efTransaction.TransactionId;

        public void Commit() => _efTransaction.Commit();
        public void Rollback() => _efTransaction.Rollback();
        public void Dispose() => _efTransaction.Dispose();
    }
}