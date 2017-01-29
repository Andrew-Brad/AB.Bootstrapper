using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AB.Bootstrapper.Interfaces.Repository
{
    /// <summary>
    /// Common interface for my singleton repositories.  This design is a work in progress. 
    /// *** All inherited repositories should adhere to CRUD naming conventions.  This is the most logical, and often specific 
    /// database implementations should have class names following the template [Database][Domain]Repository.
    /// Ex: SqlCustomerRepository
    /// What can go here: Members that assist in understanding all transient repo interfaces, universally, regardless of database or data type.
    /// </summary>

    public interface ISingletonRepository
    {
    }
}
