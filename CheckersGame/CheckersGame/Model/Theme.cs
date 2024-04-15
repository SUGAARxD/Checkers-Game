
using CheckersGame.ViewModel;

namespace CheckersGame.Model
{
    public class Theme : BaseNotify
    {
        #region Properties and members

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                NotifyPropertyChanged(nameof(Name));
            }
        }

        private string _buttonBackgroundColor;
        public string ButtonBackgroundColor
        {
            get => _buttonBackgroundColor;
            set
            {
                _buttonBackgroundColor = value;
                NotifyPropertyChanged(nameof(ButtonBackgroundColor));
            }
        }

        private string _buttonBorderColor;
        public string ButtonBorderColor
        {
            get => _buttonBorderColor;
            set
            {
                _buttonBorderColor = value;
                NotifyPropertyChanged(nameof(ButtonBorderColor));
            }
        }

    private string _windowBackgroundColor;
        public string WindowBackgroundColor
        {
            get => _windowBackgroundColor;
            set
            {
                _windowBackgroundColor = value;
                NotifyPropertyChanged(nameof(WindowBackgroundColor));
            }
        }

        private string _normalBorderColor;
        public string NormalBorderColor
        {
            get => _normalBorderColor;
            set
            {
                _normalBorderColor = value;
                NotifyPropertyChanged(nameof(NormalBorderColor));
            }
        }

        private string _availableMoveBorderColor;
        public string AvailableMoveBorderColor
        {
            get => _availableMoveBorderColor;
            set
            {
                _availableMoveBorderColor = value;
                NotifyPropertyChanged(nameof(AvailableMoveBorderColor));
            }
        }

        private string _selectedCellBorderColor;
        public string SelectedCellBorderColor
        {
            get => _selectedCellBorderColor;
            set
            {
                _selectedCellBorderColor = value;
                NotifyPropertyChanged(nameof(SelectedCellBorderColor));
            }
        }

        private string _whiteSquareImagePath;
        public string WhiteSquareImagePath
        {
            get => _whiteSquareImagePath;
            set
            {
                _whiteSquareImagePath = value;
                NotifyPropertyChanged(nameof(WhiteSquareImagePath));
            }
        }

        private string _blackSquareImagePath;
        public string BlackSquareImagePath
        {
            get => _blackSquareImagePath;
            set
            {
                _blackSquareImagePath = value;
                NotifyPropertyChanged(nameof(BlackSquareImagePath));
            }
        }

        private string _whitePieceImagePath;
        public string WhitePieceImagePath
        {
            get => _whitePieceImagePath;
            set
            {
                _whitePieceImagePath = value;
                NotifyPropertyChanged(nameof(WhitePieceImagePath));
            }
        }

        private string _whiteKingImagePath;
        public string WhiteKingImagePath
        {
            get => _whiteKingImagePath;
            set
            {
                _whiteKingImagePath = value;
                NotifyPropertyChanged(nameof(WhiteKingImagePath));
            }
        }

        private string _redPieceImagePath;
        public string RedPieceImagePath
        {
            get => _redPieceImagePath;
            set
            {
                _redPieceImagePath = value;
                NotifyPropertyChanged(nameof(RedPieceImagePath));
            }
        }

        private string _redKingImagePath;
        public string RedKingImagePath
        {
            get => _redKingImagePath;
            set
            {
                _redKingImagePath = value;
                NotifyPropertyChanged(nameof(RedKingImagePath));
            }
        }

        private string _menuImagePath;
        public string MenuImagePath
        {
            get => _menuImagePath;
            set
            {
                _menuImagePath = value;
                NotifyPropertyChanged(nameof(MenuImagePath));
            }
        }

        #endregion

    }
}
