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
    /// <summary>
    /// Interaction logic for EditQuizView.xaml
    /// </summary>
    public partial class EditQuizView : UserControl
    {
        private MainWindow _mainWindow;

        public EditQuizView(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            QuizList.ItemsSource = SaveQuizToJson.GetAllSavedQuizzes();
        }

        private void EditSelectedQuizClick(object sender, RoutedEventArgs e)
        {
            if(QuizList.SelectedItem is not string selectedTitle)
            {
                MessageBox.Show("Please select a quiz to edit!");
                return;
            }
            try
            {
                var quiz = SaveQuizToJson.LoadQuizFromFile(selectedTitle);
                _mainWindow.ShowView(new CreateQuizView(_mainWindow, quiz));
            }
            catch
            {
                MessageBox.Show("Failed to load");
            }
        }
        private void DeleteSelectedQuizClick(object sender, RoutedEventArgs e)
        {
            if (QuizList.SelectedItem is not string selectedTitle)
            {
                MessageBox.Show("Please select a quiz to delete!");
                return;
            }
            var confirm = MessageBox.Show($"Are you sure you want to delete {selectedTitle} ?", "Confirm Delete", MessageBoxButton.YesNo);
            if(confirm == MessageBoxResult.Yes)
            {
                try
                {
                    SaveQuizToJson.DeleteQuiz(selectedTitle);
                    MessageBox.Show($"Quiz '{selectedTitle}' deleted successfully!");
                    QuizList.ItemsSource =SaveQuizToJson.GetAllSavedQuizzes();

                }
                catch
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
