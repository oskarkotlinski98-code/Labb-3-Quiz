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
    public partial class AddQuestionWindow : Window
    {
        public Question? CreatedQuestion { get; private set; }

        public AddQuestionWindow()
        {
            InitializeComponent();
            CorrectAnswerBox.SelectedIndex = 0; // Default = 1
        }

        private void BrowseImageClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Image Files|*.png;*.jpg;*.jpeg;*.bmp;*.gif";

            if (dlg.ShowDialog() == true)
            {
                ImagePathBox.Text = dlg.FileName;
            }
        }

        private void AddClick(object sender, RoutedEventArgs e)
        {
            string questionText = QuestionTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(questionText))
            {
                MessageBox.Show("You must enter a question.", "Validation Error",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string[] answers =
            {
                Answer1Box.Text.Trim(),
                Answer2Box.Text.Trim(),
                Answer3Box.Text.Trim(),
                Answer4Box.Text.Trim()
            };

            
            for (int i = 0; i < answers.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(answers[i]))
                {
                    MessageBox.Show($"Answer {i + 1} cannot be empty.",
                        "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }

            if (CorrectAnswerBox.SelectedIndex < 0)
            {
                MessageBox.Show("Select which answer is correct.",
                    "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int correctIndex = CorrectAnswerBox.SelectedIndex;

             
            CreatedQuestion = new Question
            {
                Statement = questionText,
                Answers = answers,
                CorrectAnswer = correctIndex,
                ImagePath = string.IsNullOrWhiteSpace(ImagePathBox.Text)
                    ? null
                    : ImagePathBox.Text
            };

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