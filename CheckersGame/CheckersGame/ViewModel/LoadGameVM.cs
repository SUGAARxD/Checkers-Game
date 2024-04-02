using CheckersGame.Model;
using CheckersGame.View;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.IO;

namespace CheckersGame.ViewModel
{
    internal class LoadGameVM
    {
        public LoadGameVM()
        {
            Games = new ObservableCollection<string>();
            if (Directory.Exists(_savesFolderPath))
            {
                string[] filesPath = Directory.GetFiles(_savesFolderPath);

                foreach (string filePath in filesPath)
                {
                    int lastBackSlashIndex = filePath.LastIndexOf('\\');
                    int lastDotIndex = filePath.LastIndexOf('.');

                    string fileName = filePath.Substring(lastBackSlashIndex + 1, lastDotIndex - lastBackSlashIndex - 1);
                    Games.Add(fileName);
                }
            }
        }

        #region Properties and members

        private readonly string _savesFolderPath = "..\\..\\Saves";
        public ObservableCollection<string> Games { get; set; }

        #endregion

        #region Commands

        private ICommand _loadGameCommand;
        public ICommand LoadGameCommand
        {
            get
            {
                if (_loadGameCommand == null)
                    _loadGameCommand = new RelayCommand(ExecuteLoadGame);
                return _loadGameCommand;
            }
        }

        private void ExecuteLoadGame(object parameter)
        {





            bool a = false;
            if (a /*to be completed, this is only for replacement*/)
                Application.Current.Windows.OfType<MenuWindow>().First().Close();
        }

        #endregion

    }
}
