using AB.Bootstrapper.Interfaces.Repository;
using ArangoDB.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AB.Bootstrapper.Arango
{
    public class ArangoGenericSingletonRepository<T,TId> : ITestSingletonRepository<T, TId>
    {
        private readonly string _connectionString;
        private readonly ArangoDatabase _db;
        public ArangoGenericSingletonRepository(string connectionString, string databaseName = null)
        {
            _connectionString = connectionString;
            if (string.IsNullOrEmpty(databaseName)) databaseName = typeof(T).Name;
            _db = new ArangoDatabase(connectionString, databaseName);
        }

        public Task<bool> DeleteAll()
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteById(TId id)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindById(TId id)
        {
            throw new NotImplementedException();
        }

        public Task<ulong> GetCount()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> ReadAll()
        {
            throw new NotImplementedException();
        }
    }
}
