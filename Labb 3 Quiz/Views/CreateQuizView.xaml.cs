using Labb_3_Quiz.Data;
using Labb_3_Quiz.QuizModel;
using Labb_3_Quiz.Views;
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

namespace Labb_3_Quiz
{
    public partial class CreateQuizView : UserControl
    {
        private readonly MainWindow _mainWindow;
        private readonly Data.MongoQuizStorage _storage = new Data.MongoQuizStorage();

        
        private List<Question> _questions = new List<Question>();

        
        private Quiz? _editingQuiz;

       
        private static string ImagesFolder =>
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Labb3-NET22", "Images");

        public CreateQuizView()
        {
            InitializeComponent();
        }

        
        public CreateQuizView(MainWindow mainWindow) : this()
        {
            _mainWindow = mainWindow;
        }

        
        public CreateQuizView(MainWindow mainWindow, Quiz existingQuiz) : this(mainWindow)
        {
            if (existingQuiz == null) throw new ArgumentNullException(nameof(existingQuiz));
            _editingQuiz = existingQuiz;
            QuizTitleBox.Text = existingQuiz.Title;
            _questions = existingQuiz.Questions != null ? new List<Question>(existingQuiz.Questions) : new List<Question>();
            RefreshQuestionList();
        }

        private void RefreshQuestionList()
        {
            QuestionList.ItemsSource = null;
            QuestionList.ItemsSource = _questions.Select((q, idx) => $"{idx + 1}. {q.Statement}").ToList();
        }

        
        private async void AddQuestionClick(object sender, RoutedEventArgs e)
        {
            var dlg = new AddQuestionWindow();
            dlg.Owner = Window.GetWindow(this);
            bool? result = dlg.ShowDialog();
            if (result == true && dlg.CreatedQuestion != null)
            {
                var q = dlg.CreatedQuestion;

                
                if (!string.IsNullOrWhiteSpace(q.ImagePath) && File.Exists(q.ImagePath))
                {
                    
                    try
                    {
                        string copied = await CopyImageToAppDataAsync(q.ImagePath);
                        q.ImagePath = Path.GetFileName(copied);
                    }
                    catch
                    {
                        
                    }
                }

                _questions.Add(q);
                RefreshQuestionList();
            }
        }

       
        private async void QuestionListMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (QuestionList.SelectedIndex < 0 || QuestionList.SelectedIndex >= _questions.Count) return;

            int idx = QuestionList.SelectedIndex;
            var existing = _questions[idx];

            var dlg = new EditQuestionWindow(existing);
            dlg.Owner = Window.GetWindow(this);
            bool? res = dlg.ShowDialog();
            if (res == true && dlg.EditedQuestion != null)
            {
                var edited = dlg.EditedQuestion;

                if (!string.IsNullOrWhiteSpace(edited.ImagePath) && Path.IsPathRooted(edited.ImagePath) && File.Exists(edited.ImagePath))
                {
                    try
                    {
                        string copied = await CopyImageToAppDataAsync(edited.ImagePath);
                        edited.ImagePath = Path.GetFileName(copied);
                    }
                    catch
                    {
                        
                    }
                }

                _questions[idx] = edited;
                RefreshQuestionList();
            }
        }

        
        private async void SaveQuizClick(object sender, RoutedEventArgs e)
        {
            try
            {
                string title = QuizTitleBox.Text?.Trim() ?? string.Empty;
                if (string.IsNullOrWhiteSpace(title))
                {
                    MessageBox.Show("Please enter a quiz title.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                
                Quiz quizToSave = _editingQuiz ?? new Quiz();
                quizToSave.Title = title;
                quizToSave.Questions = new List<Question>(_questions);

                if (string.IsNullOrWhiteSpace(quizToSave.Id))
                {
                   
                    await _storage.CreateQuizAsync(quizToSave);
                    MessageBox.Show("Quiz created successfully.", "Saved", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    
                    await _storage.UpdateQuizAsync(quizToSave);
                    MessageBox.Show("Quiz updated successfully.", "Saved", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                
                _mainWindow?.ShowView(new MainMenuView(_mainWindow));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to save quiz:\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        
        private void BackClick(object sender, RoutedEventArgs e)
        {
            _mainWindow?.ShowView(new MainMenuView(_mainWindow));
        }

        
        private static async Task<string> CopyImageToAppDataAsync(string sourcePath)
        {
            if (string.IsNullOrWhiteSpace(sourcePath)) throw new ArgumentNullException(nameof(sourcePath));
            if (!File.Exists(sourcePath)) throw new FileNotFoundException("Image not found", sourcePath);

            Directory.CreateDirectory(ImagesFolder);

            string destFileName = Path.GetFileName(sourcePath);
            string destPath = Path.Combine(ImagesFolder, destFileName);

            
            int i = 1;
            string baseName = Path.GetFileNameWithoutExtension(destFileName);
            string ext = Path.GetExtension(destFileName);
            while (File.Exists(destPath))
            {
                destFileName = $"{baseName}_{i}{ext}";
                destPath = Path.Combine(ImagesFolder, destFileName);
                i++;
            }

            await Task.Run(() => File.Copy(sourcePath, destPath, overwrite: false));

            return destPath;
        }
    }
}