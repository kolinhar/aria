using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Aria.DB
{
    public class MongoDb
    {
        public static MongoClient Mongo;

        public MongoDb()
        {
            Mongo = new MongoClient("mongodb://x18srhrgcb7:27017");
        }

        public static IMongoCollection<T> GetCollection<T>(string collection)
        {
            return Mongo.GetDatabase("conversations").GetCollection<T>(collection);
        }

        public void RegisterNewMessage(string name, DateTime timestamp, string message)
        {
            var filtre = Builders<Conversation>.Filter.Eq("daystamp", $"{DateTime.Now.Day}{DateTime.Now.Month}{DateTime.Now.Year}");
            var push = Builders<Conversation>.Update.Push("Messages", MessageFactory(name, timestamp, message));
            GetCollection<Conversation>("conversations").UpdateOneAsync(filtre, push);
        }

        private Message MessageFactory(string name, DateTime timestamp, string message)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(message))
                throw new Exception("Nom ou Contenu absent");
            if (timestamp == null || timestamp == DateTime.MinValue)
                throw new Exception("Date invalide");

            return new Message()
            {
                Username = name,
                Content = message,
                DateTime = timestamp
            };
        }

    }

    public class Conversation
    {
        public ObjectId _id { get; set; }
        public string daystamp { get; set; }
        public List<string> Messages { get; set; }
    }

    public class Message
    {
        public string Username { get; set; }
        public DateTime DateTime { get; set; }
        public string Content { get; set; }
    }

}