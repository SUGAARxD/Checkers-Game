
using CheckersGame.Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace CheckersGame.Utilities
{
    internal class FileHelper
    {

        #region Properties and members

        private static List<Theme> _themes;

        private static readonly string _themeNumberJsonPath = @"..\..\Resources\Themes\ThemeNumber.json";
        private static readonly string _themesJsonPath = @"..\..\Resources\Themes\Themes.json";

        #endregion

        #region Methods

        public static void InitFileHelper()
        {
            _themes = new List<Theme>();
            ReadFromJson<List<Theme>>(ref _themes, _themesJsonPath);
        }
        public static void InitTheme(ref Theme theme)
        {
            int themeNumber = 0;
            ReadFromJson<int>(ref themeNumber, _themeNumberJsonPath);
            theme = _themes[themeNumber];
        }

        public static void LoadTheme(ref Theme theme, int number)
        {
            UpdateJson(number, _themeNumberJsonPath);
            InitTheme(ref theme);
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

        #endregion


    }
}
