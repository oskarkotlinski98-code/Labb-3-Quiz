using Labb_3_Quiz.QuizModel;
using System;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Labb_3_Quiz.Views
{
    public partial class PlayQuizView : UserControl, INotifyPropertyChanged
    {
        private readonly MainWindow _mainWindow;
        private QuizSession _session;

        private Question? _currentQuestion;
        public Question? CurrentQuestion
        {
            get => _currentQuestion;
            private set
            {
                _currentQuestion = value;
                OnPropertyChanged(nameof(CurrentQuestion));
            }
        }

        public PlayQuizView()
        {
            InitializeComponent();
            DataContext = this;
        }

        public PlayQuizView(MainWindow mainWindow) : this()
        {
            _mainWindow = mainWindow;
        }


        public PlayQuizView(MainWindow mainWindow, Quiz quiz) : this(mainWindow)
        {
            LoadQuiz(quiz);
        }

        public void LoadQuiz(Quiz quiz)
        {
            if (quiz == null) throw new ArgumentNullException(nameof(quiz));
            if (quiz.Questions == null || quiz.Questions.Count == 0)
            {
                MessageBox.Show("The selected quiz has no questions.", "Empty quiz", MessageBoxButton.OK, MessageBoxImage.Warning);
                _mainWindow?.ShowView(new MainMenuView(_mainWindow));
                return;
            }

            _session = new QuizSession(quiz);


            ShowNextQuestion();
        }

        private void ShowNextQuestion()
        {

            if (_session == null || !_session.HasMoreQuestions())
            {
                FinishQuiz();
                return;
            }


            CurrentQuestion = _session.CurrentQuestion;


            LoadQuestionImage(CurrentQuestion?.ImagePath);


            UpdateScoreDisplay();


            EnableAnswerButtons(true);
        }

        private void FinishQuiz()
        {
            if (_session == null)
            {
                _mainWindow?.ShowView(new MainMenuView(_mainWindow));
                return;
            }

            int total = _session.TotalQuestions;
            int score = _session.Score;
            double pct = total == 0 ? 0 : (double)score * 100.0 / total;

            MessageBox.Show($"Quiz finished!\n\nScore: {score}/{total} ({pct:F1}%)", "Finished", MessageBoxButton.OK, MessageBoxImage.Information);

            
            _mainWindow?.ShowView(new MainMenuView(_mainWindow));
        }

        private void EnableAnswerButtons(bool enable)
        {
            AnswerOneButton.IsEnabled = enable;
            AnswerTwoButton.IsEnabled = enable;
            AnswerThreeButton.IsEnabled = enable;
            AnswerFourButton.IsEnabled = enable;
        }

        private void LoadQuestionImage(string? imagePath)
        {
            QuestionImage.Source = null;

            if (string.IsNullOrWhiteSpace(imagePath))
                return;

            try
            {
                string resolved = imagePath!;
                if (!Path.IsPathRooted(resolved))
                {

                    string appImages = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Labb3-NET22", "Images");
                    resolved = Path.Combine(appImages, resolved);
                }

                if (!File.Exists(resolved))
                    return;

                var bmp = new BitmapImage();
                bmp.BeginInit();
                bmp.UriSource = new Uri(resolved, UriKind.Absolute);
                bmp.CacheOption = BitmapCacheOption.OnLoad;
                bmp.EndInit();
                QuestionImage.Source = bmp;
            }
            catch
            {

                QuestionImage.Source = null;
            }
        }

        private async Task HandleAnswerAsync(int selectedIndex)
        {
            if (_session == null || CurrentQuestion == null)
                return;


            EnableAnswerButtons(false);

            bool wasCorrect = selectedIndex == CurrentQuestion.CorrectAnswer;


            _session.SubmitAnswer(selectedIndex);


            string feedback = wasCorrect ? "Correct!" : $"Wrong! Correct: {CurrentQuestion.Answers[CurrentQuestion.CorrectAnswer]}";
            MessageBox.Show(feedback, "Answer", MessageBoxButton.OK, wasCorrect ? MessageBoxImage.Information : MessageBoxImage.Exclamation);


            UpdateScoreDisplay();


            await Task.Delay(200);


            ShowNextQuestion();
        }


        private async void AnswerOne_Click(object sender, RoutedEventArgs e) => await HandleAnswerAsync(0);
        private async void SubmitTwo_Click(object sender, RoutedEventArgs e) => await HandleAnswerAsync(1);
        private async void AnswerThree_Click(object sender, RoutedEventArgs e) => await HandleAnswerAsync(2);
        private async void AnswerFour_Click(object sender, RoutedEventArgs e) => await HandleAnswerAsync(3);

        private void UpdateScoreDisplay()
        {
            if (_session == null)
            {
                ScoreTextBlock.Text = "0 / 0 (0%)";
                return;
            }

            int total = _session.TotalQuestions;
            int score = _session.Score;
            double pct = total == 0 ? 0 : (double)score * 100.0 / total;
            ScoreTextBlock.Text = $"{score} / {total} ({pct:F0}%)";
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        #endregion
    }
}