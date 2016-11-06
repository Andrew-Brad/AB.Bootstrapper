using AB.Bootstrapper.Interfaces.Repository;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AB.Bootstrapper.MongoDb
{
    public class MongoRepository<T>// : ICrudRepository<T,Guid>
    {
        /// <summary>
        /// From the docs:
        ///  Typically you only create one MongoClient instance for a given cluster and use it across your application. Creating multiple MongoClients will, however, still share the same pool of connections if and only if the connection strings are identical.
        ///  https://mongodb.github.io/mongo-csharp-driver/2.3/getting_started/quick_tour/
        /// </summary>
        private MongoClient _client;
        private IMongoDatabase _db;
        public MongoRepository(string connectionString, string databaseName)
        {
            _client = new MongoClient(connectionString);
            _db = _client.GetDatabase(databaseName);
        }

        public Task<bool> DeleteById(Guid id)
        {
            var collection = _db.GetCollection<T>(nameof(T)).AsQueryable();
            await collection.
        }

        public Task<IEnumerable<T>> FindAll()
        {
            throw new NotImplementedException();
        }

        public async Task<T> FindById(Guid id)
        {
            var collection = _db.GetCollection<T>(nameof(T));
            await collection.FindAsync()
        }

        public uint GetCount()
        {
            throw new NotImplementedException();
        }
    }
}
