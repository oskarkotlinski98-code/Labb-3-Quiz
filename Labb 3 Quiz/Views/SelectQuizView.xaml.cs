using Labb_3_Quiz.QuizModel;
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

namespace Labb_3_Quiz.Views
{
    /// <summary>
    /// Interaction logic for SelectQuizView.xaml
    /// </summary>
    public partial class SelectQuizView : UserControl
    {
        private readonly MainWindow _mainWindow;

        public SelectQuizView(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;

            LoadQuizListAsync();
        }


        private async Task LoadQuizListAsync()
        {
            try
            {
                var quizzes = await SaveQuizToJson.GetAllSavedQuizzes();
                QuizList.ItemsSource = quizzes;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load quiz list:\n\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        private async void PlaySelectedClick(object sender, RoutedEventArgs e)
        {
            if (QuizList.SelectedItem is not string selectedTitle)
            {
                MessageBox.Show("Please select a quiz first!");
                return;
            }

            try
            {
                var quiz = await SaveQuizToJson.LoadQuizFromFile(selectedTitle);
                
                
                var playView = new PlayQuizView(_mainWindow);
                playView.LoadQuiz(quiz);
                _mainWindow.ShowView(new PlayQuizView(_mainWindow, quiz));
            }
            catch
            {
                MessageBox.Show("Could not load quiz file. It may be missing or corrupted.");
            }
           
        }

        private void BackClick(object sender, RoutedEventArgs e)
        {
            _mainWindow.ShowView(new MainMenuView(_mainWindow));
        }
    }
}