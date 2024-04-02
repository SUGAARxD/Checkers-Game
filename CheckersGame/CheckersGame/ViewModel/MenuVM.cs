

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
                    _newGameCommand = new RelayCommand(null);
                return _newGameCommand;
            }
        }

        private ICommand _loadGameCommand;
        public ICommand LoadGameCommand
        {
            get
            {
                if (_loadGameCommand == null)
                    _loadGameCommand = new RelayCommand(null);
                return _loadGameCommand;
            }
        }

        private ICommand _customGameCommand;
        public ICommand CustomGameCommand
        {
            get
            {
                if (_customGameCommand == null)
                    _customGameCommand = new RelayCommand(null);
                return _customGameCommand;
            }
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
