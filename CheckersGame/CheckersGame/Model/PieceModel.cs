
namespace CheckersGame.Model
{
    internal class PieceModel
    {

        public PieceModel(PieceType pieceType)
        {
            Type = pieceType;
        }

        #region Properties and members

        public PieceType Type { get; set; }

        public int XPos { get; set; }
        public int YPos { get; set; }

        public bool IsKing { get; set; }

        #endregion

    }
}
