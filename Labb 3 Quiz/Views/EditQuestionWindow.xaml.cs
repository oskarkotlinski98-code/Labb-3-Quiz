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
        public Question? EditedQuestion { get; private set; }

        public EditQuestionWindow(Question question)
        {
            InitializeComponent();
            LoadQuestion(question);
        }

        private void LoadQuestion(Question q)
        {
            QuestionTextBox.Text = q.Statement;
            Answer1Box.Text = q.Answers[0];
            Answer2Box.Text = q.Answers[1];
            Answer3Box.Text = q.Answers[2];
            Answer4Box.Text = q.Answers[3];

            CorrectAnswerBox.SelectedIndex = q.CorrectAnswer;
            ImagePathBox.Text = q.ImagePath;
        }

        private void BrowseImage_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Image files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg"
            };

            if (openFileDialog.ShowDialog() == true)
                ImagePathBox.Text = openFileDialog.FileName;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            EditedQuestion = new Question(
                QuestionTextBox.Text,
                new[] { Answer1Box.Text, Answer2Box.Text, Answer3Box.Text },
                CorrectAnswerBox.SelectedIndex
            );

            if (!string.IsNullOrWhiteSpace(ImagePathBox.Text) && File.Exists(ImagePathBox.Text))
                EditedQuestion.ImagePath = ImagePathBox.Text;

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