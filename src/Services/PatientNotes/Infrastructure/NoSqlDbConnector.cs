using MongoDB.Driver;
using PatientNotes.Common.Interfaces;
using PatientNotes.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientNotes.Infrastructure
{
    public class NoSqlDbConnector<T> : INoSqlDbConnector<T>
        where T : BaseEntity
    {
        private readonly IMongoDatabase _mongoDatabase;

        public NoSqlDbConnector(IMongoDatabase mongoDatabase)
        {
            _mongoDatabase = mongoDatabase;
        }

        public async Task<T> Insert(T entity)
        {
            await _mongoDatabase.GetCollection<T>(nameof(T)).InsertOneAsync(entity);
            return entity;
        }

        public async Task<IEnumerable<T>> GetAll(string id)
        {
            return await _mongoDatabase.GetCollection<T>(nameof(T)).Find(_ => true).ToListAsync();
        }

        // TODO Update, Delete, Filter
    }
}
