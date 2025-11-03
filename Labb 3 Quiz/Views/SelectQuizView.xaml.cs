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

            // Load all quizzes from storage
            QuizList.ItemsSource = SaveQuizToJson.GetAllSavedQuizzes();
        }

        private void PlaySelectedClick(object sender, RoutedEventArgs e)
        {
            if (QuizList.SelectedItem is not string selectedTitle)
            {
                MessageBox.Show("Please select a quiz first!");
                return;
            }

            try
            {
                var quiz = SaveQuizToJson.LoadQuizFromFile(selectedTitle);
                
                
                var playView = new PlayQuizView(_mainWindow);
                playView.LoadQuiz(quiz);
                _mainWindow.ShowView(playView);
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