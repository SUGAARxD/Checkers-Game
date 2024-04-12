
using CheckersGame.Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Windows.Media;

namespace CheckersGame.Utilities
{
    internal class FileHelper
    {

        #region Properties and members

        private static readonly string _settingsJsonPath = "..\\..\\Resources\\Settings\\Settings.json";
        private static readonly string _themeNumberJsonPath = "..\\..\\Resources\\Themes\\ThemeNumber.json";
        private static readonly string _themesJsonPath = "..\\..\\Resources\\Themes\\Themes.json";
        private static readonly string _statisticsJsonPath = "..\\..\\Resources\\Database\\Statistics.json";

        #endregion

        #region Methods

        public static void InitSettings(ref CustomSettings settings)
        {
            ReadFromJson(ref settings, _settingsJsonPath);
            ReadFromJson(ref settings._themes, _themesJsonPath);
            int lastThemeUsedNumber = 0;
            ReadFromJson(ref lastThemeUsedNumber, _themeNumberJsonPath);
            settings.LoadTheme(lastThemeUsedNumber);
        }

        public static void UpdateSettings(CustomSettings settings)
        {
            UpdateJson(settings, _settingsJsonPath);
        }

        public static void UpdateLastThemeUsedTheme(int number)
        {
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

        public static void UpdateWins(string winnerColor)
        {
            Dictionary<string, int> wins = GetStatistics();
            wins[winnerColor] += 1;
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
            if (string.IsNullOrEmpty(imagePath))
                return null;
            return new ImageSourceConverter()
                .ConvertFromString(System.IO.Path.GetFullPath(imagePath)) as ImageSource;
        }

        #endregion


    }
}
