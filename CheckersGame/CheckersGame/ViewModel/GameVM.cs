using CheckersGame.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace CheckersGame.ViewModel
{
    internal class GameVM : Base
    {
        public GameVM()
        {
        }

        public GameVM(bool allowMultipleJump,  Theme theme)
        {
            _theme = theme;
            _game = new GameModel();
            Init();
            UpdateBoard();
            _game.AllowMultipleJump = allowMultipleJump;
        }

        public GameVM(object game,  Theme theme)
        {
            _theme = theme;
            _game = (GameModel)game;
            Init();
            UpdateBoard();
        }

        #region Properties and members

        private Theme _theme;
        public Theme MyTheme
        {
            get => _theme;
            set
            {
                _theme = value;
                NotifyPropertyChanged(nameof(MyTheme));
            }
        }

        private readonly GameModel _game;

        public ObservableCollection<ObservableCollection<BoardCellModel>> Board { get; set; }

        public BoardCellModel SelectedPiece { get; set; }

        private List<Tuple<int, int>> _availableMoves;

        private int _numberOfWhitePieces;
        public int NumberOfWhitePieces
        {
            get => _numberOfWhitePieces;
            set
            {
                _numberOfWhitePieces = value;
                NotifyPropertyChanged(nameof(NumberOfWhitePieces));
            }
        }
        private int _numberOfRedPieces;
        public int NumberOfRedPieces
        {
            get => _numberOfRedPieces;
            set
            {
                _numberOfRedPieces = value;
                NotifyPropertyChanged(nameof(NumberOfRedPieces));
            }
        }

        #endregion

        #region Methods

        private void Init()
        {
            Board = new ObservableCollection<ObservableCollection<BoardCellModel>>();
            for (int index = 0; index < GameModel.numberOfLines; index++)
            {
                ObservableCollection<BoardCellModel> line = new ObservableCollection<BoardCellModel>();
                for (int jndex = 0; jndex < GameModel.numberOfColumns; jndex++)
                {
                    BoardCellModel boardCell = new BoardCellModel(ExecuteClickAction)
                    {
                        XPos = index,
                        YPos = jndex
                    };

                    if ((index + jndex) % 2 == 0)
                    {
                        boardCell.BackgroundImage = GetImage(_theme.WhiteSquareImagePath);
                    }
                    else
                    {
                        boardCell.BackgroundImage = GetImage(_theme.BlackSquareImagePath);
                    }

                    boardCell.PieceImage = null;
                    boardCell.CellBorderColor = _theme.NormalBorderColor;
                    line.Add(boardCell);
                }
                Board.Add(line);
            }
            _availableMoves = new List<Tuple<int, int>>();
            SelectedPiece = null;
        }

        private void UpdateBoard()
        {
            NumberOfWhitePieces = 0;
            NumberOfRedPieces = 0;
            for (int index = 0; index < GameModel.numberOfLines; index++)
            {
                for (int jndex = 0; jndex < GameModel.numberOfColumns; jndex++)
                {
                    if (_game.PieceIsNull(index, jndex))
                        Board[index][jndex].PieceImage = null;
                    else
                    {
                        switch (_game.GetPieceTypeForPiece(index, jndex))
                        {
                            default:
                                break;

                            case PieceType.WhitePiece:

                                NumberOfWhitePieces++;

                                if (_game.IsPieceKing(index, jndex))
                                    Board[index][jndex].PieceImage = GetImage(_theme.WhiteKingImagePath);
                                else
                                    Board[index][jndex].PieceImage = GetImage(_theme.WhitePieceImagePath);

                                break;

                            case PieceType.RedPiece:

                                NumberOfRedPieces++;

                                if (_game.IsPieceKing(index, jndex))
                                    Board[index][jndex].PieceImage = GetImage(_theme.RedKingImagePath);
                                else
                                    Board[index][jndex].PieceImage = GetImage(_theme.RedPieceImagePath);

                                break;

                        }
                    }
                }
            }
        }

        private ImageSource GetImage(string imagePath)
        {
            if (string.IsNullOrEmpty(imagePath))
                return null;
            return new ImageSourceConverter()
                .ConvertFromString(System.IO.Path.GetFullPath(imagePath)) as ImageSource;
        }

        private void ExecuteClickAction(object parameter)
        {
            if (!(parameter is BoardCellModel clickedCell))
                return;

            if (SelectedPiece == null && !_game.PieceIsNull(clickedCell.XPos, clickedCell.YPos))
            {
                SelectedPiece = clickedCell;
                SelectedPiece.CellBorderColor = _theme.SelectedCellBorderColor;

                _availableMoves = _game.AvailableMoves(SelectedPiece.XPos, SelectedPiece.YPos);
                ColorAvailableMovesBorder();
            }
            else
            {
                if (SelectedPiece == null)
                    return;

                if (IsInAvailableMoves(clickedCell))
                {
                    _game.MovePiece(Tuple.Create(SelectedPiece.XPos, SelectedPiece.YPos), Tuple.Create(clickedCell.XPos, clickedCell.YPos));
                    UpdateBoard();
                }
                Reset();
            }
        }

        private void ColorAvailableMovesBorder()
        {
            foreach (var position in _availableMoves)
            {
                Board[position.Item1][position.Item2].CellBorderColor = _theme.AvailableMoveBorderColor;
            }
        }

        private void Reset()
        {
            SelectedPiece = null;
            for (int index = 0; index < GameModel.numberOfLines; index++)
            {
                for (int jndex = 0; jndex < GameModel.numberOfColumns; jndex++)
                {
                    Board[index][jndex].CellBorderColor = _theme.NormalBorderColor;
                }
            }
        }

        private bool IsInAvailableMoves(BoardCellModel piece)
        {
            foreach (var position in _availableMoves)
            {
                if (position.Item1 == piece.XPos && position.Item2 == piece.YPos)
                    return true;
            }
            return false;
        }

        #endregion

    }
}
