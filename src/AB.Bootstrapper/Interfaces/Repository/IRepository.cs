using System;

namespace AB.Bootstrapper.Interfaces.Repository
{
    /// <summary>
    /// Common interface for all of my repository implementations.  This design works generally well for me.  I don't like DB specific
    /// exceptions leaking up to my calling code, so my repo's tend to be transient in lifetime, and have state which is managed by the repo itself to
    /// indicate status.
    /// *** All inherited repositories should adhere to CRUD naming conventions.  This is the most logical, and often specific 
    /// database implementations should have class names following the template [Database][Domain]Repository.
    /// Ex: SqlCustomerRepository
    /// What can go here: Members that assist in understanding all repo interfaces, universally, regardless of database or data type.
    /// </summary>
    public interface IRepository
    {
        Exception LastException { get; }
    }

    public enum RepositoryStatus
    {
        Error = 0,
        Success = 1,
        Initialized = 2,
        DatabaseUnavailable = 3,
        DatabaseTimeout = 4,
        NoResultRecords = 5,
        DataConflict = 6
    }
}
