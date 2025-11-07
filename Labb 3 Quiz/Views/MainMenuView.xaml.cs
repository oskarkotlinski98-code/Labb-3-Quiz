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
    /// Interaction logic for MainMenuView.xaml
    /// </summary>
    public partial class MainMenuView : UserControl
    {
        private readonly MainWindow _mainWindow;

        public MainMenuView(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
        }



        private void NewQuizClick(object sender, RoutedEventArgs e)
        {
            _mainWindow.ShowView(new CreateQuizView(_mainWindow));
        }

        private void PlayQuizClick(object sender, RoutedEventArgs e)
        {
            try { 
            _mainWindow.ShowView(new SelectQuizView(_mainWindow));
        }
    catch (Exception ex)
    {
        MessageBox.Show($"Error loading quiz view:\n\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
    }
}

        private void EditQuizClick(object sender, RoutedEventArgs e)
        {
            _mainWindow.ShowView(new EditQuizView(_mainWindow));
        }
    }
}