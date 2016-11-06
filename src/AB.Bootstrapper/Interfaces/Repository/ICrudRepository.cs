using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AB.Bootstrapper.Interfaces.Repository
{
    /// <summary>
    /// A simple repository supporting all generic types and basic methods you'd expect all databases to support.  Great for PoC or simple projects.
    /// </summary>
    public interface ICrudRepository<T,TId>
    {
        Task<T> FindById(TId id);
        Task<IEnumerable<T>> FindAll();
        uint GetCount();
        Task<bool> DeleteById(TId id);
    }
}
