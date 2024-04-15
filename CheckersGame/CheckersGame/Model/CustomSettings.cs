
using CheckersGame.Utilities;
using CheckersGame.ViewModel;
using System.Collections.Generic;

namespace CheckersGame.Model
{
    public class CustomSettings : BaseNotify
    {
        public CustomSettings()
        {
        }

        #region Properties and members

        public List<Theme> Themes { get; set; }

        private Theme _myTheme;
        public Theme MyTheme
        {
            get => _myTheme;
            set
            {
                _myTheme = value;
                NotifyPropertyChanged(nameof(MyTheme));
            }
        }

        private bool _allowMultipleJump;
        public bool AllowMultipleJump
        {
            get => _allowMultipleJump;
            set
            {
                _allowMultipleJump = value;
                NotifyPropertyChanged(nameof(AllowMultipleJump));
            }
        }

        #endregion

        #region Methods

        public void LoadTheme(int number)
        {
            MyTheme = Themes[number];
        }

        #endregion

    }
}
