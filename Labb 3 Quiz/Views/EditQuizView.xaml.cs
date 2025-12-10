using Labb_3_Quiz.Data;
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
using Path = System.IO.Path;

namespace Labb_3_Quiz.Views
{
   
    public partial class EditQuizView : UserControl
    {
        private MainWindow _mainWindow;

        public EditQuizView(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            _ = LoadQuizListAsync();
        }


        private async Task LoadQuizListAsync()
        {
            MongoQuizStorage storage = new MongoQuizStorage();
            var quizzes = await storage.GetAllQuizzesAsync();
            
            QuizList.ItemsSource = quizzes;
        }

        private async void EditSelectedQuizClick(object sender, RoutedEventArgs e)
        {
            if(QuizList.SelectedItem is not Quiz selectedQuiz)
            {
                MessageBox.Show("Please select a quiz to edit!");
                return;
            }
            try
            {
                MongoQuizStorage storage = new MongoQuizStorage();
                var quiz = await storage.GetQuizByTitleAsync(selectedQuiz.Title);
                _mainWindow.ShowView(new CreateQuizView(_mainWindow, quiz));
            }
            catch
            {
                MessageBox.Show("Failed to load");
            }
        }
        private async void DeleteSelectedQuizClick(object sender, RoutedEventArgs e)
        {
            if (QuizList.SelectedItem is not Quiz selectedQuiz)
            {
                MessageBox.Show("Please select a quiz to delete!");
                return;
            }
            var confirm = MessageBox.Show($"Are you sure you want to delete {selectedQuiz.Title} ?", "Confirm Delete", MessageBoxButton.YesNo);
            if (confirm == MessageBoxResult.Yes)
            {
                try
                {
                    MongoQuizStorage storage = new MongoQuizStorage();
                    await storage.DeleteQuizAsync(selectedQuiz.Title);
                    MessageBox.Show($"Quiz '{selectedQuiz.Title}' deleted successfully!");
                    QuizList.ItemsSource = await storage.GetAllQuizzesAsync();

                }
                catch (Exception x)
                {
                    MessageBox.Show("Failed to delete the quiz.");
                }

            }
        }
        private void BackClick(object sender, RoutedEventArgs e)
        {
            _mainWindow.ShowView(new MainMenuView(_mainWindow));

        }
    }
}
