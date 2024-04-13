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
        public string StarterPlayerColor { get; set; }

        #endregion
    }
}
