using ExpressDeliveryService.Services.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace ExpressDeliveryService.Data
{
    public sealed class DataManager : IDataManager
    {
        public DataManager(string database)
        {
            // Connect to Local
            var client = new MongoClient();

            db = client.GetDatabase(database);
        }

        public static string ActualNameDB { get; } = "ExpressDeliveryDB";

        private IMongoDatabase db;

        public void Create<T>(string table, T item)
        {
            var collection = db.GetCollection<T>(table);
            collection.InsertOne(item);
        }

        public List<T> GetAll<T>(string table)
        {
            var collection = db.GetCollection<T>(table);
            return collection.Find(new BsonDocument()).ToList();
        }

        public T Find<T>(string table, Guid id)
        {
            var collection = db.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("_id", id);

            return collection.Find(filter).First();
        }

        public void Edit<T>(string table, Guid id, T item)
        {
            var collection = db.GetCollection<T>(table);
            collection.ReplaceOne(new BsonDocument("_id", id), item, new UpdateOptions { IsUpsert = true });
        }

        public void Delete<T>(string table, Guid id)
        {
            var collection = db.GetCollection<T>(table); 
            var filter = Builders<T>.Filter.Eq("_id", id); 
            collection.DeleteOne(filter);
        }
    }
}
