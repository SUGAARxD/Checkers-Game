using CheckersGame.Model;
using CheckersGame.Utilities;
using CheckersGame.View;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace CheckersGame.ViewModel
{
    internal class SettingsVM :BaseNotify
    {
        public SettingsVM()
        {

        }
        public SettingsVM(CustomSettings settings)
        {
            _mySettings = settings;
            Init();
        }

        #region Properties and members

        private CustomSettings _mySettings;
        public CustomSettings MySettings
        {
            get => _mySettings;
            set
            {
                _mySettings = value;
                NotifyPropertyChanged(nameof(MySettings));
            }
        }

        private bool _isCheckBoxChecked;
        public bool IsCheckBoxChecked
        {
            get => _isCheckBoxChecked;
            set
            {
                _isCheckBoxChecked = value;
                NotifyPropertyChanged(nameof(IsCheckBoxChecked));
            }
        }

        public ObservableCollection<string> ThemeList { get; set; }

        private string _selectedTheme;
        public string SelectedTheme
        {
            get => _selectedTheme;
            set
            {
                _selectedTheme = value;
                NotifyPropertyChanged(nameof(SelectedTheme));
            }
        }

        private string _initialTheme;

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
            Application.Current.Windows.OfType<SettingsWindow>().First().Close();
        }

        private ICommand _saveChangesCommand;
        public ICommand SaveChangesCommand
        {
            get
            {
                if (_saveChangesCommand == null)
                    _saveChangesCommand = new RelayCommand(ExecuteSaveChanges);
                return _saveChangesCommand;
            }
        }

        private void ExecuteSaveChanges(object parameter)
        {

            int themeNumber = -1;

            if (SelectedTheme != _initialTheme)
            {
                for (int index = 0; index < _mySettings.Themes.Count; index++)
                {
                    Theme theme = _mySettings.Themes[index];
                    if (theme.Name == SelectedTheme)
                    {
                        themeNumber = index;
                        break;
                    }
                }

            }
            if (themeNumber != -1)
                _mySettings.LoadTheme(themeNumber);
            MySettings.AllowMultipleJump = IsCheckBoxChecked;
            FileHelper.UpdateSettings(IsCheckBoxChecked, themeNumber);
            Application.Current.Windows.OfType<SettingsWindow>().First().Close();
        }

        #endregion

        #region Methods

        private void Init()
        {
            IsCheckBoxChecked = _mySettings.AllowMultipleJump;
            SelectedTheme = _mySettings.MyTheme.Name;
            _initialTheme = _mySettings.MyTheme.Name;

            ThemeList = new ObservableCollection<string>();

            foreach (var theme in _mySettings.Themes)
            {
                ThemeList.Add(theme.Name);
            }
        }

        #endregion

    }
}
