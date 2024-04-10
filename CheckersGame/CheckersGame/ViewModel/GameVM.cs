using CheckersGame.Model;
using System.Collections.Generic;
using System;
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
            InitBoard();
            UpdateBoard();
            _game.AllowMultipleJump = allowMultipleJump;
        }

        public GameVM(object game)
        {
            _game = (GameModel)game;
            InitBoard();
            UpdateBoard();
        }

        #region Properties and members

        private GameModel _game;

        public ObservableCollection<ObservableCollection<BoardCellModel>> Board { get; set; }

        public BoardCellModel SelectedPiece { get; set; } 

        private List<Tuple<int, int>> _availableMoves;

        private readonly string _normalBorderColor = "#000000";
        private readonly string _possibleMoveBorderColor = "#7e00de";
        private readonly string _selectedCellBorderColor = "#00f21d";

        private readonly string _whiteSquareImagePath = "..\\..\\Resources\\Images\\white_square.jpg";
        private readonly string _blackSquareImagePath = "..\\..\\Resources\\Images\\black_square.jpg";
        private readonly string _whitePawnImagePath = "..\\..\\Resources\\Images\\white_pawn.png";
        private readonly string _whiteKingImagePath = "..\\..\\Resources\\Images\\white_king.png";
        private readonly string _redPawnImagePath = "..\\..\\Resources\\Images\\red_pawn.png";
        private readonly string _redKingImagePath = "..\\..\\Resources\\Images\\red_king.png";

        #endregion

        #region Methods

        private void InitBoard()
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
            var board = _game.GetBoard();
            for (int index = 0; index < GameModel.numberOfLines; index++)
            {
                for (int jndex = 0; jndex < GameModel.numberOfColumns; jndex++)
                {
                    PieceModel piece = board[index][jndex];

                    if (piece == null)
                        Board[index][jndex].PieceImage = null;
                    else
                    {
                        switch (piece.Type)
                        {
                            default:
                                break;

                            case PieceType.WhitePawn:

                                if (piece.IsKing)
                                    Board[index][jndex].PieceImage = GetImage(_whiteKingImagePath);
                                else
                                    Board[index][jndex].PieceImage = GetImage(_whitePawnImagePath);

                                break;

                            case PieceType.RedPawn:

                                if (piece.IsKing)
                                    Board[index][jndex].PieceImage = GetImage(_redKingImagePath);
                                else
                                    Board[index][jndex].PieceImage = GetImage(_redPawnImagePath);

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
            PieceModel currentPiece = _game.GetPiece(clickedCell.XPos, clickedCell.YPos);
            if (currentPiece == null)
            {
                if (SelectedPiece != null && IsInAvailableMoves(clickedCell))
                {
                    _game.MovePiece(Tuple.Create(SelectedPiece.XPos, SelectedPiece.YPos), Tuple.Create(clickedCell.XPos, clickedCell.YPos));
                    UpdateBoard();
                    Reset();
                    return;
                }

                Reset();
                return;
            }
            else if (SelectedPiece != null)
            {
                Reset();
                return;
            }
            SelectedPiece = clickedCell;
            SelectedPiece.CellBorderColor = _selectedCellBorderColor;

            _availableMoves = _game.AvailableMoves(currentPiece);
            foreach (var position in _availableMoves)
            {
                Board[position.Item1][position.Item2].CellBorderColor = _possibleMoveBorderColor;
            }
        }

        private void Reset()
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
