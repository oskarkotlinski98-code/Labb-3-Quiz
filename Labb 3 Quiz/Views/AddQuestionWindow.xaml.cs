using Labb_3_Quiz.QuizModel;
using Microsoft.Win32;
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
using System.Windows.Shapes;

namespace Labb_3_Quiz.Views
{
    /// <summary>
    /// Interaction logic for AddQuestionWindow.xaml
    /// </summary>
    public partial class AddQuestionWindow : Window
    {
        public Question NewQuestion { get; private set; }
        public AddQuestionWindow()
        {
            InitializeComponent();
        }

        public void BrowseImageClick(object sender, RoutedEventArgs e)
        {
            var chosenImage = new OpenFileDialog
            {
                Filter = "Image files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg"
            };

            if (chosenImage.ShowDialog() == true)
            {
                ImagePathBox.Text = chosenImage.FileName;
            }

        }

        private void AddClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(QuestionTextBox.Text) ||
               string.IsNullOrWhiteSpace(Answer1Box.Text) ||
               string.IsNullOrWhiteSpace(Answer2Box.Text) ||
               string.IsNullOrWhiteSpace(Answer3Box.Text) ||
               string.IsNullOrWhiteSpace(Answer4Box.Text))
            {
                MessageBox.Show("Please fill in all spaces");
                return;
            }

            int correctAnswer = CorrectAnswerBox.SelectedIndex;

            NewQuestion = new Question(QuestionTextBox.Text,
                new[] { Answer1Box.Text, Answer2Box.Text, Answer3Box.Text, Answer4Box.Text }, correctAnswer);
            if (!string.IsNullOrWhiteSpace(ImagePathBox.Text) && File.Exists(ImagePathBox.Text))
            {
                NewQuestion.ImagePath = ImagePathBox.Text;
            }

            DialogResult = true;
            Close();

        }
        private void CancelClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }


    }
}
