
using CheckersGame.Model;

namespace CheckersGame.ViewModel
{
    internal class HowToPlayVM
    {
        public HowToPlayVM()
        {
        }

        public HowToPlayVM(CustomSettings settings)
        {
            MySetting = settings;
            GameDescription = "   Checkers is a classic board game played between two players on an 8x8 grid board.\n\n " +
                "   The objective of checkers is to capture all of your opponent's pieces or to block them so they cannot move. " +
                "The game begins with the board set up so that each player has 12 pieces placed on the dark squares of the three rows closest to them.\n\n" +
                "   Players take turns moving one of their pieces diagonally forward to an adjacent unoccupied square.\n" +
                "   Pieces can only move forward diagonally, toward the opponent's side of the board.\n" +
                "   If a player's piece reaches the opponent's back row (the furthest row from the player), it becomes a \"king\" and can move both forwards and backwards.\n\n" +
                "   If one of your opponent's pieces is diagonally adjacent to one of your pieces and there is an empty square behind it, you can \"jump\" over the opponent's piece and capture it.\n" +
                "   If it's checked in the settings of the game, after you capture the opponent's piece you must continue to \"jump\" over the opponent's pieces until you no more can capture.\n\n" +
                "   The game ends when one player captures all of their opponent's pieces, or their opponent has no legal moves remaining.\n\n" +
                "   If a player cannot move any of their pieces, they lose the game.";
        }

        #region Properties and members

        public CustomSettings MySetting { get; set; }

        public string GameDescription { get; set; }

        #endregion

    }
}
