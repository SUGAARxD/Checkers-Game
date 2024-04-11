
using CheckersGame.ViewModel;

namespace CheckersGame.Model
{
    public class Theme : Base
    {
        #region Properties and members

        private string _normalBorderColor = "#00d5ff";
        public string NormalBorderColor
        {
            get => _normalBorderColor;
            set
            {
                _normalBorderColor = value;
                NotifyPropertyChanged(nameof(NormalBorderColor));
            }
        }

        private string _availableMoveBorderColor = "#7e00de";
        public string AvailableMoveBorderColor
        {
            get => _availableMoveBorderColor;
            set
            {
                _availableMoveBorderColor = value;
                NotifyPropertyChanged(nameof(AvailableMoveBorderColor));
            }
        }

        private string _selectedCellBorderColor = "#00f21d";
        public string SelectedCellBorderColor
        {
            get => _selectedCellBorderColor;
            set
            {
                _selectedCellBorderColor = value;
                NotifyPropertyChanged(nameof(SelectedCellBorderColor));
            }
        }

        private string _whiteSquareImagePath = "..\\..\\Resources\\Images\\white_square.jpg";
        public string WhiteSquareImagePath
        {
            get => _whiteSquareImagePath;
            set
            {
                _whiteSquareImagePath = value;
                NotifyPropertyChanged(nameof(WhiteSquareImagePath));
            }
        }

        private string _blackSquareImagePath = "..\\..\\Resources\\Images\\black_square.jpg";
        public string BlackSquareImagePath
        {
            get => _blackSquareImagePath;
            set
            {
                _blackSquareImagePath = value;
                NotifyPropertyChanged(nameof(BlackSquareImagePath));
            }
        }

        private string _whitePieceImagePath = "..\\..\\Resources\\Images\\white_piece.png";
        public string WhitePieceImagePath
        {
            get => _whitePieceImagePath;
            set
            {
                _whitePieceImagePath = value;
                NotifyPropertyChanged(nameof(WhitePieceImagePath));
            }
        }

        private string _whiteKingImagePath = "..\\..\\Resources\\Images\\white_king.png";
        public string WhiteKingImagePath
        {
            get => _whiteKingImagePath;
            set
            {
                _whiteKingImagePath = value;
                NotifyPropertyChanged(nameof(WhiteKingImagePath));
            }
        }

        private string _redPieceImagePath = "..\\..\\Resources\\Images\\red_piece.png";
        public string RedPieceImagePath
        {
            get => _redPieceImagePath;
            set
            {
                _redPieceImagePath = value;
                NotifyPropertyChanged(nameof(RedPieceImagePath));
            }
        }

        private string _redKingImagePath = "..\\..\\Resources\\Images\\red_king.png";
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
