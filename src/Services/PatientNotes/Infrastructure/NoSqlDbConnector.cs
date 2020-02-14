﻿using MongoDB.Driver;
using PatientNotes.Common.Interfaces;
using PatientNotes.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public async Task<T> InsertAsync(T entity)
        {
            await _mongoDatabase.GetCollection<T>(typeof(T).Name).InsertOneAsync(entity);
            return entity;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _mongoDatabase.GetCollection<T>(typeof(T).Name).Find(_ => true).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllByFilterAsync(Expression<Func<T, bool>> filter)
        {
            return await _mongoDatabase.GetCollection<T>(typeof(T).Name).Find(filter).ToListAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            await _mongoDatabase.GetCollection<T>(typeof(T).Name).ReplaceOneAsync(
                item => item.Id == entity.Id,
                entity,
                new ReplaceOptions { IsUpsert = false });
        }

        public async Task DeleteAsync(string id)
        {
            await _mongoDatabase.GetCollection<T>(typeof(T).Name).DeleteOneAsync(item => item.Id == new MongoDB.Bson.ObjectId(id));
        }
    }
}
