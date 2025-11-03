using Labb_3_Quiz.QuizModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb_3_Quiz.QuizModel
{
    public class QuizSession
    {
        private readonly Quiz _quiz;
        private readonly List<Question> _shuffledQuestions;
        private int _currentIndex = 0;

        public int CorrectAnswers { get; private set; }
        public int TotalAnswered { get; private set; }

        public QuizSession(Quiz quiz)
        {
            _quiz = quiz ?? throw new ArgumentNullException(nameof(quiz));
            _shuffledQuestions = _quiz.Questions.OrderBy(q => Guid.NewGuid()).ToList();
        }

        public Question GetNextQuestion()
        {
            if (_currentIndex >= _shuffledQuestions.Count)
            {
                return null;

            }
            return _shuffledQuestions[_currentIndex++];
        }

        public bool SubmitAnswer(Question q, int selectedIndex)
        {
            TotalAnswered++;
            if (q.CorrectAnswer == selectedIndex)
            {
                CorrectAnswers++;
                return true;
            }
            return false;
        }

        public double GetScorePercentage()
        {
            if (TotalAnswered == 0) return 0;
            return (double)CorrectAnswers / TotalAnswered * 100;
        }

        public bool IsFinished => _currentIndex >= _shuffledQuestions.Count;
    }
}