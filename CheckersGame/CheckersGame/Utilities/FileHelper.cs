
using CheckersGame.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Media;

namespace CheckersGame.Utilities
{
    internal class FileHelper
    {

        #region Properties and members

        private static readonly string _allowMultipleJumpJsonPath = "..\\..\\Resources\\Settings\\AllowMultipleJump.json";
        private static readonly string _themeNumberJsonPath = "..\\..\\Resources\\Themes\\ThemeNumber.json";
        private static readonly string _themesJsonPath = "..\\..\\Resources\\Themes\\Themes.json";
        private static readonly string _statisticsJsonPath = "..\\..\\Resources\\Database\\Statistics.json";
        private static readonly string _fileExtension = ".json";
        private static readonly string _savesFolderPath = "..\\..\\Resources\\Saves";

        #endregion

        #region Methods

        public static void InitSettings(ref CustomSettings settings)
        {
            List<Theme> themes = new List<Theme>();
            ReadFromJson(ref themes, _themesJsonPath);
            settings.Themes = themes;

            int lastThemeUsedNumber = 0;
            ReadFromJson(ref lastThemeUsedNumber, _themeNumberJsonPath);
            settings.LoadTheme(lastThemeUsedNumber);

            bool allowMultipleJump = false;
            ReadFromJson(ref allowMultipleJump, _allowMultipleJumpJsonPath);
            settings.AllowMultipleJump = allowMultipleJump;
        }

        public static void UpdateSettings(bool allowMultipleJump, int number)
        {
            UpdateJson(allowMultipleJump, _allowMultipleJumpJsonPath);
            if (number != -1)
                UpdateJson(number, _themeNumberJsonPath);
        }

        public static void ReadFromJson<T>(ref T parameter, string path)
        {
            if (File.Exists(path))
            {
                string jsonString = File.ReadAllText(path);
                parameter = JsonConvert.DeserializeObject<T>(jsonString);
            }
        }

        public static void UpdateJson(object parameter, string path)
        {
            string json = JsonConvert.SerializeObject(parameter);
            File.WriteAllText(path, json);
        }

        public static void UpdateWins(string winnerColor, int numberOfWinnerPiecesLeft)
        {
            Dictionary<string, int> wins = GetStatistics();
            wins[winnerColor] += 1;
            if (winnerColor == "white")
                wins["MaxNumberOfWhitePiecesLeft"] = Math.Max(numberOfWinnerPiecesLeft, wins["MaxNumberOfWhitePiecesLeft"]);
            else
                wins["MaxNumberOfRedPiecesLeft"] = Math.Max(numberOfWinnerPiecesLeft, wins["MaxNumberOfRedPiecesLeft"]);
            UpdateJson(wins, _statisticsJsonPath);
        }

        public static Dictionary<string, int> GetStatistics()
        {
            Dictionary<string, int> wins = new Dictionary<string, int>();
            ReadFromJson(ref wins, _statisticsJsonPath);
            return wins;
        }

        public static ImageSource GetImage(string imagePath)
        {
            if (string.IsNullOrEmpty(imagePath) || !File.Exists(imagePath))
                return null;
            return new ImageSourceConverter()
                .ConvertFromString(System.IO.Path.GetFullPath(imagePath)) as ImageSource;
        }

        public static void SaveGame(SavedGameModel save, string saveName)
        {
            UpdateJson(save, _savesFolderPath + "\\" + saveName + _fileExtension);
        }

        public static void LoadGame(ref SavedGameModel save, string saveName)
        {
            ReadFromJson<SavedGameModel>(ref save, _savesFolderPath + "\\" + saveName + _fileExtension);
        }

        public static bool SaveExists(string saveName)
        {
            return File.Exists(_savesFolderPath + "\\" + saveName + _fileExtension);
        }

        public static void InitSavedGames(ObservableCollection<string> SavedGames)
        {
            if (Directory.Exists(_savesFolderPath))
            {
                string[] filesPath = Directory.GetFiles(_savesFolderPath);

                foreach (string filePath in filesPath)
                {
                    int lastBackSlashIndex = filePath.LastIndexOf('\\');
                    int lastDotIndex = filePath.LastIndexOf('.');

                    string fileName = filePath.Substring(lastBackSlashIndex + 1, lastDotIndex - lastBackSlashIndex - 1);
                    SavedGames.Add(fileName);
                }
            }
        }

        #endregion


    }
}
