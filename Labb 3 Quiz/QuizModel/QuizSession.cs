using Labb_3_Quiz.QuizModel;
using System;
using System.Collections.Generic;

namespace Labb_3_Quiz
{
    public class QuizSession
    {
        private readonly Quiz _quiz;
        private int _currentQuestionIndex = 0;

        public int Score { get; private set; }
        public Question CurrentQuestion => _quiz.Questions[_currentQuestionIndex];
        public int TotalQuestions => _quiz.Questions.Count;
        public int CurrentIndex => _currentQuestionIndex + 1;

        public QuizSession(Quiz quiz)
        {
            _quiz = quiz;
            Score = 0;
        }

        public bool HasMoreQuestions()
        {
            return _currentQuestionIndex < _quiz.Questions.Count;
        }

        public void SubmitAnswer(int selectedIndex)
        {
            if (selectedIndex == CurrentQuestion.CorrectAnswer)
                Score++;

            _currentQuestionIndex++;
        }
    }
}