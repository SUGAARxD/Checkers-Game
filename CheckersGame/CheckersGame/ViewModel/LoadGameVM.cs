using CheckersGame.View;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.IO;
using CheckersGame.Utilities;
using CheckersGame.Model;

namespace CheckersGame.ViewModel
{
    internal class LoadGameVM : BaseNotify
    {
        public LoadGameVM()
        {

        }
        public LoadGameVM(CustomSettings settings)
        {
            _settings = settings;
            InitListBox();
        }

        #region Properties and members

        public ObservableCollection<string> Games { get; set; }

        public string SelectedListBoxItem { get; set; }

        private CustomSettings _settings;
        public CustomSettings MySettings
        {
            get => _settings;
            set
            {
                _settings = value;
                NotifyPropertyChanged(nameof(MySettings));
            }
        }

        #endregion

        #region Commands

        private ICommand _cancelCommand;
        public ICommand CancelCommand
        {
            get
            {
                if (_cancelCommand == null)
                    _cancelCommand = new RelayCommand(ExecuteCancel);
                return _cancelCommand;
            }
        }

        private void ExecuteCancel(object parameter)
        {
            Application.Current.Windows.OfType<LoadGameWindow>().First().Close();
        }

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
            if (string.IsNullOrEmpty(SelectedListBoxItem))
                return;

            SavedGameModel save = new SavedGameModel();
            FileHelper.LoadGame(ref save, SelectedListBoxItem);

            if (save.IsValid())
            {

                MenuWindow lastOpenedMenuWindow = Application.Current.Windows.OfType<MenuWindow>().FirstOrDefault();
                GameWindow laseOpenedGameWindow = Application.Current.Windows.OfType<GameWindow>().FirstOrDefault();

                GameWindow gameWindow = new GameWindow(save, _settings);
                gameWindow.Show();

                if (lastOpenedMenuWindow != null)
                    lastOpenedMenuWindow.Close();

                if (laseOpenedGameWindow != null)
                    laseOpenedGameWindow.Close();

                Application.Current.Windows.OfType<LoadGameWindow>().First().Close();
                
                MessageBox.Show("Load game succesfull!");
            }
            else
            {
                MessageBox.Show("Failed to load game!");
            }
        }

        #endregion

        #region Methods

        private void InitListBox()
        {
            Games = new ObservableCollection<string>();
            if (Directory.Exists(FileHelper._savesFolderPath))
            {
                string[] filesPath = Directory.GetFiles(FileHelper._savesFolderPath);

                foreach (string filePath in filesPath)
                {
                    int lastBackSlashIndex = filePath.LastIndexOf('\\');
                    int lastDotIndex = filePath.LastIndexOf('.');

                    string fileName = filePath.Substring(lastBackSlashIndex + 1, lastDotIndex - lastBackSlashIndex - 1);
                    Games.Add(fileName);
                }
            }
        }

        #endregion

    }
}
