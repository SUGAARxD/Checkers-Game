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

        public GameVM(bool allowMultipleJump)
        {
            _game = new GameModel();
            Init();
            UpdateBoard();
            _game.AllowMultipleJump = allowMultipleJump;
        }

        public GameVM(object game)
        {
            _game = (GameModel)game;
            Init();
            UpdateBoard();
        }

        #region Properties and members

        private GameModel _game;

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

        private readonly string _normalBorderColor = "#000000";
        private readonly string _availableMoveBorderColor = "#7e00de";
        private readonly string _selectedCellBorderColor = "#00f21d";

        private readonly string _whiteSquareImagePath = "..\\..\\Resources\\Images\\white_square.jpg";
        private readonly string _blackSquareImagePath = "..\\..\\Resources\\Images\\black_square.jpg";
        private readonly string _whitePieceImagePath = "..\\..\\Resources\\Images\\white_piece.png";
        private readonly string _whiteKingImagePath = "..\\..\\Resources\\Images\\white_king.png";
        private readonly string _redPieceImagePath = "..\\..\\Resources\\Images\\red_piece.png";
        private readonly string _redKingImagePath = "..\\..\\Resources\\Images\\red_king.png";

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
                        boardCell.BackgroundImage = GetImage(_whiteSquareImagePath);
                    }
                    else
                    {
                        boardCell.BackgroundImage = GetImage(_blackSquareImagePath);
                    }

                    boardCell.PieceImage = null;
                    boardCell.CellBorderColor = _normalBorderColor;
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
                                    Board[index][jndex].PieceImage = GetImage(_whiteKingImagePath);
                                else
                                    Board[index][jndex].PieceImage = GetImage(_whitePieceImagePath);

                                break;

                            case PieceType.RedPiece:

                                NumberOfRedPieces++;

                                if (_game.IsPieceKing(index, jndex))
                                    Board[index][jndex].PieceImage = GetImage(_redKingImagePath);
                                else
                                    Board[index][jndex].PieceImage = GetImage(_redPieceImagePath);

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
            BoardCellModel clickedCell = parameter as BoardCellModel;
            if (SelectedPiece == null && !_game.PieceIsNull(clickedCell.XPos, clickedCell.YPos))
            {
                SelectedPiece = clickedCell;
                SelectedPiece.CellBorderColor = _selectedCellBorderColor;

                _availableMoves = _game.AvailableMoves(SelectedPiece.XPos, SelectedPiece.YPos);
                ColorAvailableMovesBorder();
            }
            else
            {
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
                Board[position.Item1][position.Item2].CellBorderColor = _availableMoveBorderColor;
            }
        }

        private void Reset()
        {
            if (SelectedPiece != null)
            {
                SelectedPiece = null;
                for (int index = 0; index < GameModel.numberOfLines; index++)
                {
                    for (int jndex = 0; jndex < GameModel.numberOfColumns; jndex++)
                    {
                        Board[index][jndex].CellBorderColor = _normalBorderColor;
                    }
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
