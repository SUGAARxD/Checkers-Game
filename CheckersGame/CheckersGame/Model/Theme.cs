
using CheckersGame.ViewModel;

namespace CheckersGame.Model
{
    public class Theme : BaseNotify
    {
        #region Properties and members

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

        #endregion

    }
}
