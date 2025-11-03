using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Labb_3_Quiz.QuizModel
{
    public static class SaveQuizToJson
    {
        private static readonly string Folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Labb3-NET22");

        
        static SaveQuizToJson()
        {
            Directory.CreateDirectory(Folder);
        }

        public static void SaveQuizJson(Quiz quiz)
        {
            string filePath = Path.Combine(Folder, $"{quiz.Title}.json");
            string jsonString = JsonSerializer.Serialize(quiz);
            File.WriteAllText(filePath, jsonString);
        }

        public static Quiz LoadQuizFromFile(string title)
        {
            string filePath = Path.Combine(Folder, $"{title}.json");
            string jsonString = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<Quiz>(jsonString)!;
        }

        public static IEnumerable<string> GetAllSavedQuizzes()
        {
            foreach (var file in Directory.GetFiles(Folder, "*.json"))
            {
                yield return Path.GetFileNameWithoutExtension(file);
            }

        }
        public static void DeleteQuiz(string title)
        {
            string path = Path.Combine(Folder, $"{title}.json");
            if (File.Exists(path))
                File.Delete(path);
        }
    }
}

