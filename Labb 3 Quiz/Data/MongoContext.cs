using Labb_3_Quiz.QuizModel;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb_3_Quiz.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        
        private const string DatabaseName = "QuizApp";

        
        private const string ConnectionString ="mongodb://localhost:27017";
        

        public MongoDbContext()
        {
            var client = new MongoClient(ConnectionString);
            _database = client.GetDatabase(DatabaseName);
        }

        public IMongoCollection<Quiz> Quizzes =>
            _database.GetCollection<Quiz>("Quizzes");
    }
}