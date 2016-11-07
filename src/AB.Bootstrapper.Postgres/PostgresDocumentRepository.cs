using AB.Bootstrapper.Interfaces.Repository;
using Marten;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AB.Bootstrapper.MongoDb
{
    public class PostgresDocumentRepository<T> : ICrudRepository<T, Guid>
    {
        private readonly string _connectionString;
        private DocumentStore _docStore;

        public PostgresDocumentRepository(string connectionString, string databaseName)
        {
            //http://jasperfx.github.io/marten/documentation/documents/
            _connectionString = connectionString;
            _docStore = DocumentStore.For(options =>
            {
                // Turn this off in production 
                options.AutoCreateSchemaObjects = AutoCreate.All;

                // This is still mandatory
                options.Connection(_connectionString);

                // Override the JSON Serialization
                // _.Serializer<TestsSerializer>();
            });
        }

        public async Task<bool> DeleteById(Guid id)
        {
            using (var session = _docStore.OpenSession())
            {
                session.Delete<T>(id);
                await session.SaveChangesAsync();
                return true;
            }
        }
        public async Task<bool> DeleteAll()
        {
            //Might have to plug in a truncate here - no support outside of a Type specific LINQ expression
            //using (var session = _docStore.OpenSession())
            //{
            //    session.
            //    //await session.SaveChangesAsync();
            //    return true;
            //}
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> ReadAll()
        {
            using (var session = _docStore.LightweightSession())
            {
                return await session
                    .Query<T>()
                    .ToListAsync();
            }
        }

        public async Task<T> FindById(Guid id)
        {
            using (var session = _docStore.LightweightSession())
            {
                return await session.LoadAsync<T>(id);
            }
        }

        public async Task<ulong> GetCount()
        {
            using (var session = _docStore.LightweightSession())
            {
                return (ulong)await session
                    .Query<T>()
                    .CountAsync();                    
            }
        }
    }
}
