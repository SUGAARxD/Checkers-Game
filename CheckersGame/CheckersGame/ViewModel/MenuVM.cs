using CheckersGame.Model;
using CheckersGame.Utilities;
using CheckersGame.View;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace CheckersGame.ViewModel
{
    internal class MenuVM : BaseNotify
    {
        public MenuVM()
        {
            _mySettings = new CustomSettings();
            FileHelper.InitSettings(ref _mySettings);
            MySettings = _mySettings;
        }

        #region Properties and members

        private CustomSettings _mySettings;
        public CustomSettings MySettings
        {
            get => _mySettings;
            set
            {
                _mySettings = value;
                MenuImage = FileHelper.GetImage(MySettings.MyTheme.MenuImagePath);
                NotifyPropertyChanged(nameof(MySettings));
            }
        }

        private ImageSource _menuImage;
        public ImageSource MenuImage
        {
            get => _menuImage;
            set
            {
                _menuImage = value;
                NotifyPropertyChanged(nameof(MenuImage));
            }
        }

        #endregion

        #region Commands

        private ICommand _newGameCommand;
        public ICommand NewGameCommand
        {
            get
            {
                if (_newGameCommand == null)
                    _newGameCommand = new RelayCommand(ExecuteNewGame);
                return _newGameCommand;
            }
        }

        private void ExecuteNewGame(object parameter)
        {
            GameWindow gameWindow = new GameWindow(_mySettings);
            gameWindow.Show();

            Application.Current.Windows.OfType<MenuWindow>().First().Close();
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
            LoadGameWindow loadGameWindow = new LoadGameWindow(_mySettings);
            loadGameWindow.ShowDialog();
        }

        private ICommand _howToPlayCommand;
        public ICommand HowToPlayCommand
        {
            get
            {
                if (_howToPlayCommand == null)
                    _howToPlayCommand = new RelayCommand(ExecuteHowToPlay);
                return _howToPlayCommand;
            }
        }

        private void ExecuteHowToPlay(object parameter)
        {
            HowToPlayWindow howToPlayWindow = new HowToPlayWindow(_mySettings);
            howToPlayWindow.ShowDialog();
        }

        private ICommand _settingsCommand;
        public ICommand SettingsCommand
        {
            get
            {
                if (_settingsCommand == null)
                    _settingsCommand = new RelayCommand(ExecuteSettings);
                return _settingsCommand;
            }
        }

        private void ExecuteSettings(object parameter)
        {
            SettingsWindow settingsWindow = new SettingsWindow(_mySettings);
            settingsWindow.ShowDialog();
            MySettings = _mySettings;
        }

        private ICommand _statisticsCommand;
        public ICommand StatisticsCommand
        {
            get
            {
                if (_statisticsCommand == null)
                    _statisticsCommand = new RelayCommand(ExecuteStatistics);
                return _statisticsCommand;
            }
        }

        private void ExecuteStatistics(object parameter)
        {
            StatisticsWindow statisticsWindow = new StatisticsWindow(_mySettings);
            statisticsWindow.ShowDialog();
        }

        private ICommand _creditsCommand;
        public ICommand CreditsCommand
        {
            get
            {
                if (_creditsCommand == null)
                    _creditsCommand = new RelayCommand(ExecuteCredits);
                return _creditsCommand;
            }
        }

        private void ExecuteCredits(object parameter)
        {
            CreditsWindow creditsWindow = new CreditsWindow(_mySettings);
            creditsWindow.ShowDialog();
        }

        #endregion

    }
}
