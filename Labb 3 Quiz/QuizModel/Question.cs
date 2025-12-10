using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb_3_Quiz.QuizModel
{
    public class Question
    {

        public string Statement { get; set; } = string.Empty;
        public string[] Answers { get; set; } = new string[4];
        public int CorrectAnswer { get; set; }
        public string? ImagePath { get; set; }

        public Question() {}

        public Question(string statement, string[] answers, int correctAnswer, string? imagePath = null)
        {
            Statement = statement;
            Answers = answers;
            CorrectAnswer = correctAnswer;
            ImagePath = imagePath;
        }
    }
}
