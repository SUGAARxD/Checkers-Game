
using CheckersGame.ViewModel;
using System;
using System.Windows.Input;
using System.Windows.Media;

namespace CheckersGame.Model
{
    internal class BoardCellModel : Base
    {
        public BoardCellModel()
        {
        }

        public BoardCellModel(Action<object> func)
        {
            execute = func;
        }

        #region Properties and members

        public int XPos { get; set; }
        public int YPos { get; set; }

        private ImageSource _backgroundImage;
        public ImageSource BackgroundImage 
        { 
            get => _backgroundImage;
            set
            {
                _backgroundImage = value;
                NotifyPropertyChanged(nameof(BackgroundImage));
            }
        }
        private ImageSource _pieceImage;
        public ImageSource PieceImage
        {
            get => _pieceImage;
            set
            {
                _pieceImage = value;
                NotifyPropertyChanged(nameof(PieceImage));
            }
        }
        private string _cellBorderColor;
        public string CellBorderColor
        {
            get => _cellBorderColor;
            set
            {
                _cellBorderColor = value;
                NotifyPropertyChanged(nameof(CellBorderColor));
            }
        }

        private readonly Action<object> execute;

        #endregion

        #region Commands

        private ICommand _onClickCommand;
        public ICommand OnClickCommand
        {
            get
            {
                if (_onClickCommand == null)
                    _onClickCommand = new RelayCommand(execute);
                return _onClickCommand;
            }
        }

        #endregion

    }
}
