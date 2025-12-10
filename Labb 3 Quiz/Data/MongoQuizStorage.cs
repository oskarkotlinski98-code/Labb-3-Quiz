using Labb_3_Quiz.QuizModel;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb_3_Quiz.Data
{
    public class MongoQuizStorage
    {
        private readonly IMongoCollection<Quiz> _quizCollection;

        public MongoQuizStorage()
        {
            var db = new MongoDbContext();
            _quizCollection = db.Quizzes;
        }

        
        public async Task<List<Quiz>> GetAllQuizzesAsync()
        {
            return await _quizCollection.Find(_ => true).ToListAsync();
        }

       
        public async Task<Quiz?> GetQuizByIdAsync(string id)
        {
            return await _quizCollection
                .Find(q => q.Id == id)
                .FirstOrDefaultAsync();
        }

      
        public async Task<Quiz?> GetQuizByTitleAsync(string title)
        {
            return await _quizCollection
                .Find(q => q.Title == title)
                .FirstOrDefaultAsync();
        }

      
        public async Task CreateQuizAsync(Quiz quiz)
        {
            await _quizCollection.InsertOneAsync(quiz);
        }

        
        public async Task UpdateQuizAsync(Quiz quiz)
        {
            await _quizCollection
                .ReplaceOneAsync(q => q.Id == quiz.Id, quiz);
        }

        public async Task DeleteQuizAsync(string id)
        {
            await _quizCollection.DeleteOneAsync(q => q.Id == id);
        }
    


        private Quiz CreatePremadeQuiz()
            {

            var quiz = new Quiz("Premade Quiz");

            List<Question> questions = new List<Question>() {
             new Question("Where did Mahjong originate?", new[] { "Japan", "China", "Korea", "Vietnam" }, 1),
            new Question("How many tiles are in a standard Mahjong set?",new[] { "120", "136", "144", "160" }, 2),

            new Question("What are the three suits in Mahjong?", new[] { "Circles, Bamboos, Characters", "Hearts, Clubs, Spades", "Stars, Moons, Suns", "Dragons, Winds, Flowers" }, 0),

            new Question("Which tile is NOT a Wind tile?", new[] { "East", "South", "North", "Center" }, 3),

            new Question("What is a 'Pong' in Mahjong?", new[] { "Three identical tiles", "A straight of three tiles", "Four identical tiles", "Two pairs" }, 0),

            new Question("What is a 'Chow' in Mahjong?", new[] { "Three tiles in a sequence", "Three identical tiles", "Four identical tiles", "A pair" }, 0),

            new Question("What is a 'Kong' in Mahjong?", new[] { "Four identical tiles", "Three consecutive tiles", "Two identical tiles", "A full set" }, 0),

            new Question("How many Wind tiles are there in a standard set?", new[] { "12", "16", "20", "24" }, 1),

            new Question("Which of these is NOT a Dragon tile?", new[] { "Red", "Green", "White", "Blue" }, 3),
            new Question("What is the minimum number of tiles needed to declare Mahjong (a winning hand)?",new[] { "12", "13", "14", "15" }, 2),

            new Question("Which direction traditionally starts the play in Mahjong?",new[] { "East", "West", "South", "North" }, 0),

            new Question("In Japanese Riichi Mahjong, what is a 'Dora'?", new[] { "A bonus tile", "A penalty tile", "A wild card", "A special wind" }, 0),

            new Question("How many flower tiles are in a standard Mahjong set?", new[] { "2", "4", "6", "8" }, 3),

            new Question("What happens when all four players discard the same wind tile on their first turn?", new[] { "Round restarts", "Draw game", "East wins automatically", "Each player loses points" }, 1),

            new Question("What is the goal of Mahjong?", new[] { "To collect as many tiles as possible", "To form a winning hand of sets and a pair", "To finish with no tiles left", "To have the most Dragon tiles" }, 1),
                };



            foreach (var question in questions)
            {
                quiz.AddQuestion(question);
            }
            return quiz;
        }
    }
}