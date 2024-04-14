using CheckersGame.Model;
using CheckersGame.Utilities;
using CheckersGame.View;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace CheckersGame.ViewModel
{
    internal class MenuVM : BaseNotify
    {
        public MenuVM()
        {
            _settings = new CustomSettings();
            FileHelper.InitSettings(ref _settings);
        }

        #region Properties and members

        private CustomSettings _settings;
        public CustomSettings Settings
        {
            get => _settings;
            set
            {
                _settings = value;
                NotifyPropertyChanged(nameof(Settings));
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
            GameWindow gameWindow = new GameWindow(_settings);
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
            LoadGameWindow lastOpenedWindow= Application.Current.Windows.OfType<LoadGameWindow>().FirstOrDefault();
            if (lastOpenedWindow != null)
                lastOpenedWindow.Close();

            LoadGameWindow loadGameWindow = new LoadGameWindow(_settings);
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
            HowToPlayWindow howToPlayWindow = new HowToPlayWindow();
            howToPlayWindow.Show();

            Application.Current.Windows.OfType<MenuWindow>().First().Close();
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
            SettingsWindow settingsWindow = new SettingsWindow();
            settingsWindow.ShowDialog();
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
            StatisticsWindow lastOpenedWindow = Application.Current.Windows.OfType<StatisticsWindow>().FirstOrDefault();
            if (lastOpenedWindow != null)
                lastOpenedWindow.Close();

            StatisticsWindow statisticsWindow = new StatisticsWindow();
            statisticsWindow.Show();
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
            CreditsWindow lastOpenedWindow = Application.Current.Windows.OfType<CreditsWindow>().FirstOrDefault();
            if (lastOpenedWindow != null)
                lastOpenedWindow.Close();

            CreditsWindow creditsWindow = new CreditsWindow();
            creditsWindow.Show();
        }

        #endregion

    }
}
