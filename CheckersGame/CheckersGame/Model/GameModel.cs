using System;
using System.Collections.Generic;

namespace CheckersGame.Model
{
    internal class GameModel
    {
        public GameModel()
        {

            _board = new List<List<PieceModel>>();
            for (int index = 0; index < numberOfLines; index++)
            {
                List<PieceModel> line = new List<PieceModel>();

                for (int jndex = 0; jndex < numberOfColumns; jndex++)
                {

                    if (index < 3 && (index + jndex) % 2 == 1)
                    {
                        PieceModel piece = new PieceModel(PieceType.WhitePawn)
                        {
                            XPos = index,
                            YPos = jndex,
                            IsKing = false
                        };

                        line.Add(piece);
                        continue;
                    }

                    if (index > 4 && (index + jndex) % 2 == 1)
                    {
                        PieceModel piece = new PieceModel(PieceType.RedPawn)
                        {
                            XPos = index,
                            YPos = jndex,
                            IsKing = false
                        };
                        line.Add(piece);
                        continue;
                    }

                    line.Add(null);
                }
                _board.Add(line);
            }

        }

        #region Properties and members

        private List<List<PieceModel>> _board;
        public bool AllowMultipleJump { get; set; }

        public static readonly int numberOfLines = 8;
        public static readonly int numberOfColumns = 8;

        #endregion

        #region Methods

        public List<List<PieceModel>> GetBoard()
        {
            return _board;
        }

        public void MovePiece(Tuple<int, int> prevPosition, Tuple<int, int> newPosition)
        {
            int xDifference = Math.Abs(prevPosition.Item1 - newPosition.Item1);
            int yDifference = Math.Abs(prevPosition.Item2 - newPosition.Item2);
            if (xDifference > 1 && yDifference > 1)
            {
                _board[(prevPosition.Item1 + newPosition.Item1) / 2][(prevPosition.Item2 + newPosition.Item2) / 2] = null;
            }

            _board[newPosition.Item1][newPosition.Item2] = GetPiece(prevPosition.Item1, prevPosition.Item2);
            _board[newPosition.Item1][newPosition.Item2].XPos = newPosition.Item1;
            _board[newPosition.Item1][newPosition.Item2].YPos = newPosition.Item2;
            if (_board[newPosition.Item1][newPosition.Item2].Type == PieceType.WhitePawn && newPosition.Item1 == numberOfLines - 1)
                _board[newPosition.Item1][newPosition.Item2].IsKing = true;
            if (_board[newPosition.Item1][newPosition.Item2].Type == PieceType.RedPawn && newPosition.Item1 == 0)
                _board[newPosition.Item1][newPosition.Item2].IsKing = true;
            _board[prevPosition.Item1][prevPosition.Item2] = null;
        }

        public List<Tuple<int, int>> AvailableMoves(PieceModel piece)
        {
      
            List<Tuple<int, int>> availableMoves = new List<Tuple<int, int>>();
            if (piece != null)
            {
                switch (piece.Type)
                {
                    default:
                        break;

                    case PieceType.WhitePawn:

                        DownAvailableMoves(piece, PieceType.RedPawn, ref availableMoves);
                        if (piece.IsKing)
                            UpAvailableMoves(piece, PieceType.RedPawn, ref availableMoves);

                        break;

                    case PieceType.RedPawn:

                        UpAvailableMoves(piece, PieceType.WhitePawn, ref availableMoves);
                        if (piece.IsKing)
                            DownAvailableMoves(piece, PieceType.WhitePawn, ref availableMoves);

                        break;
                }
            }

            return availableMoves;
        }

        private void UpAvailableMoves(PieceModel piece, PieceType enemyType, ref List<Tuple<int,int>> availableMoves)
        {
            if (piece.XPos > 0)
            {
                if (piece.YPos > 0)
                {
                    PieceModel nextPiece = _board[piece.XPos - 1][piece.YPos - 1];
                    if (nextPiece == null)
                        availableMoves.Add(Tuple.Create(piece.XPos - 1, piece.YPos - 1));
                    else if (nextPiece.Type == enemyType
                        && nextPiece.YPos > 0
                        && nextPiece.XPos > 0
                        && _board[nextPiece.XPos - 1][nextPiece.YPos - 1] == null)

                        availableMoves.Add(Tuple.Create(nextPiece.XPos - 1, nextPiece.YPos - 1));
                }

                if (piece.YPos < numberOfColumns - 1)
                {
                    PieceModel nextPiece = _board[piece.XPos - 1][piece.YPos + 1];
                    if (nextPiece == null)
                        availableMoves.Add(Tuple.Create(piece.XPos - 1, piece.YPos + 1));
                    else if (nextPiece.Type == enemyType
                        && nextPiece.YPos < numberOfColumns - 1
                        && nextPiece.XPos > 0
                        && _board[nextPiece.XPos - 1][nextPiece.YPos + 1] == null)

                        availableMoves.Add(Tuple.Create(nextPiece.XPos - 1, nextPiece.YPos + 1));
                }
            }
        }
        private void DownAvailableMoves(PieceModel piece, PieceType enemyType, ref List<Tuple<int, int>> availableMoves)
        {

            if (piece.XPos < numberOfLines - 1)
            {

                if (piece.YPos > 0)
                {
                    PieceModel nextPiece = _board[piece.XPos + 1][piece.YPos - 1];
                    if (nextPiece == null)
                        availableMoves.Add(Tuple.Create(piece.XPos + 1, piece.YPos - 1));
                    else if (nextPiece.Type == enemyType
                        && nextPiece.YPos > 0
                        && nextPiece.XPos < numberOfLines - 1
                        && _board[nextPiece.XPos + 1][nextPiece.YPos - 1] == null)

                        availableMoves.Add(Tuple.Create(nextPiece.XPos + 1, nextPiece.YPos - 1));
                }

                if (piece.YPos < numberOfColumns - 1)
                {
                    PieceModel nextPiece = _board[piece.XPos + 1][piece.YPos + 1];
                    if (nextPiece == null)
                        availableMoves.Add(Tuple.Create(piece.XPos + 1, piece.YPos + 1));
                    else if (nextPiece.Type == enemyType
                        && nextPiece.YPos < numberOfColumns - 1
                        && nextPiece.XPos < numberOfLines - 1
                        && _board[nextPiece.XPos + 1][nextPiece.YPos + 1] == null)

                        availableMoves.Add(Tuple.Create(nextPiece.XPos + 1, nextPiece.YPos + 1));
                }

            }
        }

        public PieceModel GetPiece(int xPos, int yPos)
        {
            return _board[xPos][yPos];
        }

        #endregion

    }
}
