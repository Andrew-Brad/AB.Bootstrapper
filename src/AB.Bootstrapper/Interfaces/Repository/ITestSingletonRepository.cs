using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AB.Bootstrapper.Interfaces.Repository
{
    public interface ITestSingletonRepository<T, TId>
    {
        Task<T> FindById(TId id);
        Task<IEnumerable<T>> ReadAll();
        Task<ulong> GetCount();
        Task<bool> DeleteById(TId id);
        Task<bool> DeleteAll();
    }
}
