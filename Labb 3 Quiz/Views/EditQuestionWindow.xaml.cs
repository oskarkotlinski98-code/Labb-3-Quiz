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

    public partial class EditQuestionWindow : Window
    {
        public Question EditedQuestion { get; private set; }
        private readonly Question _original;

        public EditQuestionWindow(Question questionToEdit)
        {
            InitializeComponent();

            _original = questionToEdit;

            LoadQuestionIntoFields(questionToEdit);
        }

        private void LoadQuestionIntoFields(Question q)
        {
           
            QuestionTextBox.Text = q.Statement;
            Answer1Box.Text = q.Answers[0];
            Answer2Box.Text = q.Answers[1];
            Answer3Box.Text = q.Answers[2];
            Answer4Box.Text = q.Answers[3];

            
            CorrectAnswerBox.SelectedIndex = q.CorrectAnswer;

            
            ImagePathBox.Text = q.ImagePath ?? "";
        }

        private void BrowseImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Image Files|*.png;*.jpg;*.jpeg;*.bmp;*.gif";

            if (dlg.ShowDialog() == true)
            {
                ImagePathBox.Text = dlg.FileName;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
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

            
            EditedQuestion = new Question
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

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}