using AB.Bootstrapper.Interfaces.Repository;
using MongoDB.Bson;
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
        /// <summary>
        /// MongoCollection field.
        /// </summary>
        protected internal IMongoCollection<T> _collection;
        public MongoRepository(string connectionString, string databaseName)
        {
            _client = new MongoClient(connectionString);
            _db = _client.GetDatabase(databaseName);
            //this line could be considered "logic" - might improve or generalize in the future.  Collection name can also be parsed via connection string
            _collection = _db.GetCollection<T>(nameof(T));
        }

        public async Task<bool> DeleteById(Guid id)
        {
            var filter = Builders<T>.Filter.Eq("_id", id.ToString());//this needs to be validated and stress tested - i've had mysterious behaviour with guid id's/strings/ObjectId's before
            var result = await _collection.DeleteOneAsync(filter);
            return (result.DeletedCount == 1);//if object did not exist we get 0!
        }
        public async Task<bool> DeleteAll()
        {
            var filter = Builders<T>.Filter.Empty;
            var result = await _collection.DeleteManyAsync(filter);
            return true;
        }

        public async Task<IEnumerable<T>> ReadAll()
        {
            var filter = Builders<T>.Filter.Empty;
            var cursor = await _collection.FindAsync(filter);
            return await cursor.ToListAsync();
        }        

        public async Task<T> FindById(Guid id)
        {
            var filter = Builders<T>.Filter.Eq("_id", id.ToString());
            var cursor = await _collection.FindAsync(filter);
            return cursor.Single();
        }

        public async Task<ulong> GetCount()
        {
            var filter = Builders<T>.Filter.Empty;
            return (ulong) await _collection.CountAsync(filter);
        }
    }
}
