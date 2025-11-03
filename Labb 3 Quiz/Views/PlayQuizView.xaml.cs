using Labb_3_Quiz.QuizModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Labb_3_Quiz.Views
{
    /// <summary>
    /// Interaction logic for PlayQuizView.xaml
    /// </summary>
    public partial class PlayQuizView : UserControl
    {
       
            private readonly MainWindow _mainWindow;
            private QuizSession _session;
            private Question _currentQuestion;

            public PlayQuizView(MainWindow mainWindow)
            {
                InitializeComponent();
                _mainWindow = mainWindow;
            }

            public void LoadQuiz(Quiz quiz)
            {
                _session = new QuizSession(quiz);
                ShowNextQuestion();
            }

            private void ShowNextQuestion()
            {
                _currentQuestion = _session.GetNextQuestion();
                if (_currentQuestion == null)
                {
                    MessageBox.Show($"🎉 Finished!\nScore: {_session.GetScorePercentage():F1}%");
                    _mainWindow.ShowView(new MainMenuView(_mainWindow));
                    return;
                }

                Questionblock.Text = _currentQuestion.Statement;
                AnswerOneButton.Content = _currentQuestion.Answers[0];
                AnswerTwoButton.Content = _currentQuestion.Answers[1];
                AnswerThreeButton.Content = _currentQuestion.Answers[2];
                AnswerFourButton.Content = _currentQuestion.Answers[3];

            if (!string.IsNullOrEmpty(_currentQuestion.ImagePath) && File.Exists(_currentQuestion.ImagePath))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new Uri(_currentQuestion.ImagePath, UriKind.Absolute);
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.EndInit();

                QuestionImage.Source = image;
            }
            else
            {
                QuestionImage.Source = null;
            }
        }

            private void SubmitAnswer(int index)
            {
                bool correct = _session.SubmitAnswer(_currentQuestion, index);
                MessageBox.Show(correct ? "✅ Correct!" : "❌ Wrong!");
                ShowNextQuestion();
            }



        private void SubmitAnswer1(object sender, RoutedEventArgs e)
        {
        SubmitAnswer(0);
        }

        private void SubmitAnswer2(object sender, RoutedEventArgs e)
        {
        SubmitAnswer(1);
        }

        private void SubmitAnswer3(object sender, RoutedEventArgs e) 
        {
        SubmitAnswer(2);
        }

        private void SubmitAnswer4(object sender, RoutedEventArgs e)
        {
        SubmitAnswer(3);
        } 

       
    } 
}
