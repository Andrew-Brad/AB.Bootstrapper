using AB.Bootstrapper.Interfaces.Repository;
using ArangoDB.Client;
using ArangoDB.Client.Collection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AB.Bootstrapper.Arango
{
    public class ArangoGenericSingletonRepository<T> : ITestSingletonRepository<T, string>
    {
        private readonly DatabaseSharedSetting _settings;
        private readonly ILogger<ArangoGenericSingletonRepository<T>> _logger;
        //Arango Domain class mappings: http://stackoverflow.com/questions/38844008/design-entities-with-union-architecture-concepts
        public ArangoGenericSingletonRepository(DatabaseSharedSetting settings,ILogger<ArangoGenericSingletonRepository<T>> logger = null)
        {
            if (logger != null) _logger = logger;
            //if (string.IsNullOrEmpty(databaseName)) databaseName = typeof(T).Name;
            _logger.LogDebug("{repository} successfully instantiated using {settings}.  Pointing Arango Repo to {url}",this,settings,settings.Url);
        }

        public async Task<bool> DeleteAll()
        {
            using (ArangoDatabase db = new ArangoDatabase(_settings))
            {
                //this was grabbed from the docs and is not using the clean truncate way to do it, but I can't find truncate in the c# driver
                //remove products transactionally and return removed results
                IAqlModifiable<T> removedResults = db.Query<T>().Remove();
                _logger.LogDebug("{repository} ran DeleteAll and returned {removedResults}",this,removedResults);
                return true;
            }
        }

        public async Task<bool> DeleteById(string id)
        {
            using (ArangoDatabase db = new ArangoDatabase(_settings))
            {
                IDocumentIdentifierResult removedResults = await db.Collection<T>().RemoveByIdAsync(id);
                _logger.LogDebug("{repository} ran DeleteById and returned {removedResults}", this, removedResults);
                return true;
            }
        }

        public async Task<T> FindById(string id)
        {
            using (ArangoDatabase db = new ArangoDatabase(_settings))
            {
                var obj = await db.DocumentAsync<T>(id);
                _logger.LogDebug("{repository} ran FindById and returned {obj}", this, obj);
                return obj;
            }
        }

        public async Task<ulong> GetCount()
        {
            using (ArangoDatabase db = new ArangoDatabase(_settings))
            {
                long? count = db.Query<T>().Statistics.Count;
                _logger.LogDebug("{repository} ran FindById and returned {count}", this, count);
                if (count.HasValue) return (ulong)count.Value;
                else return 0;
            }
        }

        public async Task<IEnumerable<T>> ReadAll()
        {
            using (ArangoDatabase db = new ArangoDatabase(_settings))
            {
                var all = await db.All<T>().ToListAsync();
                _logger.LogDebug("{repository} ran ReadAll and returned {all}", this, all);
                return all;
            }
        }
    }
}
