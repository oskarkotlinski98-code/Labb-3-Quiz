using Labb_3_Quiz.QuizModel;
using Labb_3_Quiz.Views;
using System;
using System.Collections.Generic;
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

namespace Labb_3_Quiz
{
    /// <summary>
    /// Interaction logic for CreateQuizView.xaml
    /// </summary>
    public partial class CreateQuizView : UserControl
    {
        private readonly MainWindow _mainWindow;
        private readonly List<Question> _questions = new();

        public CreateQuizView(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
        }

        public CreateQuizView(MainWindow mainWindow,Quiz quizToEdit)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            QuizTitleBox.Text = quizToEdit.Title;
            _questions = quizToEdit.Questions.ToList();
            foreach (var question in _questions)
            {
                QuestionList.Items.Add(question.Statement);
            }
        }

        private void AddQuestionClick(object sender, RoutedEventArgs e)
        {
            var addWindow = new AddQuestionWindow();
            if (addWindow.ShowDialog() == true && addWindow.NewQuestion != null)
            {
                _questions.Add(addWindow.NewQuestion);
                QuestionList.Items.Add(addWindow.NewQuestion.Statement);
            }
        }

        private void SaveQuizClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(QuizTitleBox.Text))
            {
                MessageBox.Show("Please enter a quiz title.");
                return;
            }

            var quiz = new Quiz(QuizTitleBox.Text);
            foreach (var q in _questions)
            {
                quiz.AddQuestion(q);
            }
            SaveQuizToJson.SaveQuizJson(quiz);
            MessageBox.Show("Quiz saved successfully!");
        }

        private void BackClick(object sender, RoutedEventArgs e)
        {
            _mainWindow.ShowView(new MainMenuView(_mainWindow));
        }

        private void QuestionListMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (QuestionList.SelectedIndex < 0) return;

            var selectedQuestion = _questions[QuestionList.SelectedIndex];
            var editWindow = new EditQuestionWindow(selectedQuestion);

            if (editWindow.ShowDialog() == true && editWindow.EditedQuestion != null)
            {
                _questions[QuestionList.SelectedIndex] = editWindow.EditedQuestion;
                QuestionList.Items[QuestionList.SelectedIndex] = editWindow.EditedQuestion.Statement;
            }
        }
    }
}
    
