using System;

namespace CoreLibraryApi.Infrastructure.Interfaces
{
    public interface IDbTransaction : IDisposable
    {
        void Commit();
        void Rollback();
    }
}
