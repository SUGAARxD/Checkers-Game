
using CheckersGame.View;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace CheckersGame.ViewModel
{
    internal class MenuVM : BaseVM
    {
        public MenuVM()
        {

        }

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
            MessageBoxResult result = MessageBox.Show(
                "Do you want to allow Multiple Jump?",
                "Multiple Jump", 
                MessageBoxButton.YesNoCancel);

            if (result == MessageBoxResult.Cancel)
                return;

            bool allowMultipleJump = result == MessageBoxResult.Yes;

            MenuWindow thisWindow = Application.Current.Windows.OfType<MenuWindow>().First();

            GameWindow gameWindow = new GameWindow(allowMultipleJump);
            gameWindow.Show();

            thisWindow.Close();

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
            LoadGameWindow loadGameWindow = new LoadGameWindow();
            loadGameWindow.Show();
        }

        private ICommand _howToPlayCommand;
        public ICommand HowToPlayCommand
        {
            get
            {
                if (_howToPlayCommand == null)
                    _howToPlayCommand = new RelayCommand(null);
                return _howToPlayCommand;
            }
        }

        private ICommand _settingsCommand;
        public ICommand SettingsCommand
        {
            get
            {
                if (_settingsCommand == null)
                    _settingsCommand = new RelayCommand(null);
                return _settingsCommand;
            }
        }

        private ICommand _creditsCommand;
        public ICommand CreditsCommand
        {
            get
            {
                if (_creditsCommand == null)
                    _creditsCommand = new RelayCommand(null);
                return _creditsCommand;
            }
        }

        #endregion

    }
}
