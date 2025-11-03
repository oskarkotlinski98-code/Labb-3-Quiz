using Labb_3_Quiz.QuizModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb_3_Quiz.QuizModel
{
    public class Quiz
    {

        private IEnumerable<Question> _questions;
        private string _title = string.Empty;
        private static Random _random = new Random();

        
        public IEnumerable<Question> Questions
        {
            get => _questions;
            set => _questions = value;
        }
        public string Title => _title;

        public Quiz(string title)
        {
            _title = title;
            _questions = new List<Question>();
        }

        public Question GetRandomQuestion()
        {
            var list = _questions.ToList();
            if (list.Count == 0) return null!;
            int randomIndex = _random.Next(list.Count);
            return list[randomIndex];
        }

        public void AddQuestion(Question q)
        {
            var list = _questions.ToList();
            
            list.Add(q);
            _questions = list;
        }

        public void RemoveQuestion(int index)
        {
            var list = _questions.ToList();
            if (index >= 0 && index < list.Count)
                list.RemoveAt(index);
            _questions = list;
        }
    }
}