using CheckersGame.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CheckersGame.Model
{
    internal class GameModel
    {
        public GameModel()
        {
            InitBoard();
        }

        public GameModel(SavedGameModel save)
        {
            ReconstructBoard(save.Pieces);
            CurrentPlayerColor = save.CurrentPlayerColor;
            AllowMultipleJump = save.AllowMultipleJump;
        }

        #region Properties and members

        private List<List<PieceModel>> _board;
        public bool AllowMultipleJump { get; set; }
        public string CurrentPlayerColor { get; set; } = "red";

        public static readonly int numberOfLines = 8;
        public static readonly int numberOfColumns = 8;

        #endregion

        #region Methods

        private void InitBoard()
        {
            _board = new List<List<PieceModel>>();
            for (int index = 0; index < numberOfLines; index++)
            {
                List<PieceModel> line = new List<PieceModel>();

                for (int jndex = 0; jndex < numberOfColumns; jndex++)
                {

                    if (index < 3 && (index + jndex) % 2 == 1)
                    {
                        PieceModel piece = new PieceModel(PieceType.WhitePiece);

                        line.Add(piece);
                        continue;
                    }

                    if (index > 4 && (index + jndex) % 2 == 1)
                    {
                        PieceModel piece = new PieceModel(PieceType.RedPiece);
                        line.Add(piece);
                        continue;
                    }

                    line.Add(null);
                }
                _board.Add(line);
            }
        }

        private void ReconstructBoard(List<Tuple<PieceModel, int, int>> pieces)
        {
            _board = Enumerable.Range(0, numberOfLines)
                .Select(_ => Enumerable.Repeat<PieceModel>(null, numberOfColumns).ToList()).ToList();
            foreach(var item in pieces)
            {
                var (piece, xPos, yPos) = item;

                _board[xPos][yPos] = piece;

            }
        }

        public void MovePiece(Tuple<int, int> prevPosition, Tuple<int, int> newPosition)
        {
            if (IsTwoCellsDifference(prevPosition, newPosition))
            {
                _board[(prevPosition.Item1 + newPosition.Item1) / 2][(prevPosition.Item2 + newPosition.Item2) / 2] = null;
            }

            _board[newPosition.Item1][newPosition.Item2] = _board[prevPosition.Item1][prevPosition.Item2];

            if (_board[newPosition.Item1][newPosition.Item2].Type == PieceType.WhitePiece && newPosition.Item1 == numberOfLines - 1)
                _board[newPosition.Item1][newPosition.Item2].IsKing = true;

            if (_board[newPosition.Item1][newPosition.Item2].Type == PieceType.RedPiece && newPosition.Item1 == 0)
                _board[newPosition.Item1][newPosition.Item2].IsKing = true;

            _board[prevPosition.Item1][prevPosition.Item2] = null;
        }

        public List<Tuple<int, int>> AvailableMoves(int xPos, int yPos)
        {
            PieceModel piece = _board[xPos][yPos];
            List<Tuple<int, int>> availableMoves = new List<Tuple<int, int>>();
            if (piece == null)
                return availableMoves;

            if (piece != null)
            {
                switch (piece.Type)
                {
                    default:
                        break;

                    case PieceType.WhitePiece:

                        DownAvailableMoves(Tuple.Create(xPos, yPos), PieceType.RedPiece, ref availableMoves);
                        if (piece.IsKing)
                            UpAvailableMoves(Tuple.Create(xPos, yPos), PieceType.RedPiece, ref availableMoves);

                        break;

                    case PieceType.RedPiece:

                        UpAvailableMoves(Tuple.Create(xPos, yPos), PieceType.WhitePiece, ref availableMoves);
                        if (piece.IsKing)
                            DownAvailableMoves(Tuple.Create(xPos, yPos), PieceType.WhitePiece, ref availableMoves);

                        break;
                }
            }

            return availableMoves;
        }

        private void UpAvailableMoves(Tuple<int, int> piece, PieceType enemyType, ref List<Tuple<int, int>> availableMoves)
        {
            if (piece.Item1 > 0)
            {
                Tuple<int, int> nextPiecePos;
                if (piece.Item2 > 0)
                {
                    nextPiecePos = Tuple.Create(piece.Item1 - 1, piece.Item2 - 1);
                    PieceModel nextPiece = _board[nextPiecePos.Item1][nextPiecePos.Item2];
                    if (nextPiece == null)
                        availableMoves.Add(Tuple.Create(nextPiecePos.Item1, nextPiecePos.Item2));
                    else if (nextPiece.Type == enemyType
                        && nextPiecePos.Item1 > 0
                        && nextPiecePos.Item2 > 0
                        && _board[nextPiecePos.Item1 - 1][nextPiecePos.Item2 - 1] == null)

                        availableMoves.Add(Tuple.Create(nextPiecePos.Item1 - 1, nextPiecePos.Item2 - 1));
                }

                if (piece.Item2 < numberOfColumns - 1)
                {
                    nextPiecePos = Tuple.Create(piece.Item1 - 1, piece.Item2 + 1);
                    PieceModel nextPiece = _board[nextPiecePos.Item1][nextPiecePos.Item2];
                    if (nextPiece == null)
                        availableMoves.Add(Tuple.Create(nextPiecePos.Item1, nextPiecePos.Item2));
                    else if (nextPiece.Type == enemyType
                        && nextPiecePos.Item1 > 0
                        && nextPiecePos.Item2 < numberOfColumns - 1
                        && _board[nextPiecePos.Item1 - 1][nextPiecePos.Item2 + 1] == null)

                        availableMoves.Add(Tuple.Create(nextPiecePos.Item1 - 1, nextPiecePos.Item2 + 1));
                }
            }
        }
        private void DownAvailableMoves(Tuple<int, int> piece, PieceType enemyType, ref List<Tuple<int, int>> availableMoves)
        {

            if (piece.Item1 < numberOfLines - 1)
            {
                Tuple<int, int> nextPiecePos;
                if (piece.Item2 > 0)
                {
                    nextPiecePos = Tuple.Create(piece.Item1 + 1, piece.Item2 - 1);
                    PieceModel nextPiece = _board[nextPiecePos.Item1][nextPiecePos.Item2];
                    if (nextPiece == null)
                        availableMoves.Add(Tuple.Create(nextPiecePos.Item1, nextPiecePos.Item2));
                    else if (nextPiece.Type == enemyType
                        && nextPiecePos.Item1 < numberOfLines - 1
                        && nextPiecePos.Item2 > 0
                        && _board[nextPiecePos.Item1 + 1][nextPiecePos.Item2 - 1] == null)

                        availableMoves.Add(Tuple.Create(nextPiecePos.Item1 + 1, nextPiecePos.Item2 - 1));
                }

                if (piece.Item2 < numberOfColumns - 1)
                {
                    nextPiecePos = Tuple.Create(piece.Item1 + 1, piece.Item2 + 1);
                    PieceModel nextPiece = _board[nextPiecePos.Item1][nextPiecePos.Item2];
                    if (nextPiece == null)
                        availableMoves.Add(Tuple.Create(nextPiecePos.Item1, nextPiecePos.Item2));
                    else if (nextPiece.Type == enemyType
                        && nextPiecePos.Item1 < numberOfLines - 1
                        && nextPiecePos.Item2 < numberOfColumns - 1
                        && _board[nextPiecePos.Item1 + 1][nextPiecePos.Item2 + 1] == null)

                        availableMoves.Add(Tuple.Create(nextPiecePos.Item1 + 1, nextPiecePos.Item2 + 1));
                }

            }
        }

        public bool PieceIsNull(int xPos, int yPos)
        {
            return _board[xPos][yPos] == null;
        }

        public PieceType GetPieceTypeForPiece(int xPos, int yPos)
        {
            return _board[xPos][yPos].Type;
        }

        public bool IsPieceKing(int xPos, int yPos)
        {
            return _board[xPos][yPos].IsKing;
        }

        public bool IsTwoCellsDifference(Tuple<int, int> prevPosition, Tuple<int, int> newPosition)
        {
            int xDifference = Math.Abs(prevPosition.Item1 - newPosition.Item1);
            int yDifference = Math.Abs(prevPosition.Item2 - newPosition.Item2);
            if (xDifference > 1 && yDifference > 1)
            {
                return true;
            }
            return false;
        }

        public bool CanMakeAMove(PieceType pieceType)
        {

            for (int index = 0; index < numberOfLines; index++)
            {
                for (int jndex = 0; jndex < numberOfColumns; jndex++)
                {
                    PieceModel piece = _board[index][jndex];
                    if (piece != null)
                        if (piece.Type == pieceType && AvailableMoves(index, jndex).Count != 0)
                            return true;
                }
            }

            return false;
        }

        private List<Tuple<PieceModel, int, int>> GetRemainingPieces()
        {
            List<Tuple<PieceModel, int, int>> pieces = new List<Tuple<PieceModel, int, int>>();

            for (int index = 0; index < numberOfLines; index++)
            {
                for (int jndex = 0; jndex < numberOfColumns; jndex++)
                {
                    PieceModel piece = _board[index][jndex];
                    if (piece != null)
                    {
                        pieces.Add(Tuple.Create(piece, index, jndex));
                    }
                }
            }

            return pieces;
        }

        public void SaveGame(string saveName)
        {
            SavedGameModel save = new SavedGameModel
            {
                CurrentPlayerColor = CurrentPlayerColor,
                AllowMultipleJump = AllowMultipleJump,
                Pieces = GetRemainingPieces()
            };
            FileHelper.SaveGame(save, saveName);
        }

        #endregion

    }
}
