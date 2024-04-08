﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

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
                        line.Add(new PieceModel(PieceType.WhitePawn));
                        continue;
                    }

                    if (index > 4 && (index + jndex) % 2 == 1)
                    {
                        line.Add(new PieceModel(PieceType.RedPawn));
                        continue;
                    }

                    line.Add(null);
                }
                _board.Add(line);
            }

        }

        #region Properties and members

        private List<List<PieceModel>> _board;

        public static readonly int numberOfLines = 8;
        private static readonly int numberOfColumns = 8;

        #endregion

        #region Methods

        public List<List<PieceModel>> GetBoard()
        {
            return _board;
        }

        public void MovePiece(PieceModel piece, Tuple<int, int> newPosition)
        {
            switch(piece.Type)
            {
                default:
                    break;
                case PieceType.WhitePawn:



                    break;
                case PieceType.WhiteKing:



                    break; 
                case PieceType.RedPawn:



                    break;
                case PieceType.RedKing:



                    break;
            }
        }

        public List<Tuple<int, int>> AvailableMoves(PieceModel piece)
        {
            List<Tuple<int, int>> availableMoves = new List<Tuple<int, int>>();

            switch (piece.Type)
            {
                default:
                    break;
                case PieceType.WhitePawn:

                    if (piece.XPos < numberOfLines - 1)
                    {

                        if (piece.YPos > 0)
                        {
                            PieceModel nextPiece = _board[piece.XPos + 1][piece.YPos - 1];
                            if (nextPiece == null)
                                availableMoves.Add(Tuple.Create(nextPiece.XPos, nextPiece.YPos));
                            else if (nextPiece.Type == PieceType.RedPawn
                                || nextPiece.Type == PieceType.RedKing
                                && nextPiece.YPos > 0
                                && nextPiece.XPos < numberOfLines - 1
                                && _board[nextPiece.XPos + 1][nextPiece.YPos - 1] == null)

                                availableMoves.Add(Tuple.Create(nextPiece.XPos + 1, nextPiece.YPos - 1));
                        }

                        if (piece.YPos < numberOfColumns - 1)
                        {
                            PieceModel nextPiece = _board[piece.XPos + 1][piece.YPos + 1];
                            if (nextPiece == null)
                                availableMoves.Add(Tuple.Create(nextPiece.XPos, nextPiece.YPos));
                            else if (nextPiece.Type == PieceType.RedPawn
                                || nextPiece.Type == PieceType.RedKing
                                && nextPiece.YPos < numberOfColumns - 1
                                && nextPiece.XPos < numberOfLines - 1
                                && _board[nextPiece.XPos + 1][nextPiece.YPos + 1] == null)

                                availableMoves.Add(Tuple.Create(nextPiece.XPos + 1, nextPiece.YPos + 1));
                        }

                    }
                    break;
                case PieceType.WhiteKing:



                    break;
                case PieceType.RedPawn:

                    if (piece.XPos > 0)
                    {
                        if (piece.YPos > 0)
                        {
                            PieceModel nextPiece = _board[piece.XPos - 1][piece.YPos - 1];
                            if (nextPiece == null)
                                availableMoves.Add(Tuple.Create(nextPiece.XPos, nextPiece.YPos));
                            else if (nextPiece.Type == PieceType.WhitePawn
                                || nextPiece.Type == PieceType.WhiteKing
                                && nextPiece.YPos > 0
                                && nextPiece.XPos > 0
                                && _board[nextPiece.XPos - 1][nextPiece.YPos - 1] == null)

                                availableMoves.Add(Tuple.Create(nextPiece.XPos - 1, nextPiece.YPos - 1));
                        }

                        if (piece.YPos < numberOfColumns - 1)
                        {
                            PieceModel nextPiece = _board[piece.XPos - 1][piece.YPos + 1];
                            if (nextPiece == null)
                                availableMoves.Add(Tuple.Create(nextPiece.XPos, nextPiece.YPos));
                            else if (nextPiece.Type == PieceType.WhitePawn
                                || nextPiece.Type == PieceType.WhiteKing
                                && nextPiece.YPos < numberOfColumns - 1
                                && nextPiece.XPos > 0
                                && _board[nextPiece.XPos - 1][nextPiece.YPos + 1] == null)

                                availableMoves.Add(Tuple.Create(nextPiece.XPos - 1, nextPiece.YPos + 1));
                        }
                    }
                    break;
                case PieceType.RedKing:



                    break;
            }

            return availableMoves;
        }

        #endregion

    }
}
