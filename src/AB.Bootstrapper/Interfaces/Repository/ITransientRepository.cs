using System;

namespace AB.Bootstrapper.Interfaces.Repository
{
    /// <summary>
    /// Common interface for my transient repositories.  This design works well for me.  I don't like DB specific
    /// exceptions leaking up to my calling code, and these are generally small enough to even use in controller methods if needed.  
    /// State which is managed by the repo itself to indicate status.
    /// *** All inherited repositories should adhere to CRUD naming conventions.  This is the most logical, and often specific 
    /// database implementations should have class names following the template [Database][Domain]Repository.
    /// Ex: SqlCustomerRepository
    /// What can go here: Members that assist in understanding all transient repo interfaces, universally, regardless of database or data type.
    /// </summary>
    public interface ITransientRepository
    {
        Exception LastException { get; }
    }

    public enum RepositoryStatus
    {
        Initialized = 0,
        Error = 1,
        Success = 2,        
        DatabaseUnavailable = 3,
        DatabaseTimeout = 4,
        NoResultRecords = 5,
        DataConflict = 6
    }
}
