using Labb_3_Quiz.QuizModel;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb_3_Quiz.QuizModel
{
    public class Quiz
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Title {get;set;}
        public List<Question> Questions { get;set; } = new List<Question>();
        public Quiz() { }

        
        public Quiz(string title)
        {
            Title = title;
            Questions = new List<Question>();
        }


        public void AddQuestion(Question q)
        {
            Questions.Add(q);
        }

       
        public void RemoveQuestion(int index)
        {
            if (index >= 0 && index < Questions.Count)
                Questions.RemoveAt(index);
        }

        
        public void UpdateQuestion(int index, Question updated)
        {
            if (index >= 0 && index < Questions.Count)
                Questions[index] = updated;
        }



        private static readonly Random _rng = new Random();
        public Question? GetRandomQuestion()
        {
            if (Questions.Count == 0) return null;
            return Questions[_rng.Next(Questions.Count)];
        }

      

        
    }
}