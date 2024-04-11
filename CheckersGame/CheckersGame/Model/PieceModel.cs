
namespace CheckersGame.Model
{
    internal class PieceModel
    {

        public PieceModel(PieceType pieceType)
        {
            Type = pieceType;
            IsKing = false;
        }

        #region Properties and members

        public PieceType Type { get; set; }
        public bool IsKing { get; set; }

        #endregion

    }
}
