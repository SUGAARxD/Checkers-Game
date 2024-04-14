using System;
using System.Collections.Generic;

namespace CheckersGame.Model
{
    internal class SavedGameModel
    {
        public SavedGameModel()
        {
        }

        #region Properties and members

        public List<Tuple<PieceModel, int, int>> Pieces { get; set; }
        public bool AllowMultipleJump { get; set; }
        public string CurrentPlayerColor { get; set; }

        #endregion

        #region Methods

        public bool IsValid()
        {
            bool existsWhite = false;
            bool existsRed = false;
            bool allPiecesAreOnOddSqares = true;

            if (Pieces == null)
                return false;

            foreach (var item in Pieces)
            {
                var (piece, xPos, yPos) = item;

                if (piece.Type == PieceType.WhitePiece)
                    existsWhite = true;
                else
                    existsRed = true;
                if ((xPos + yPos) % 2 == 0)
                    allPiecesAreOnOddSqares = false;

            }

            return existsWhite 
                && existsRed 
                && allPiecesAreOnOddSqares
                && !string.IsNullOrEmpty(CurrentPlayerColor);
        }

        #endregion

    }
}
