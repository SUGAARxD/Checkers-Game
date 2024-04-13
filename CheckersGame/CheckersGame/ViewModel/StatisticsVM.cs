using CheckersGame.Utilities;
using System.Collections.Generic;

namespace CheckersGame.ViewModel
{
    internal class StatisticsVM
    {
        public StatisticsVM()
        {
            Dictionary<string, int> wins = FileHelper.GetStatistics();

            WhiteWinsLabel = $"White won for {wins["white"]} times";
            MaxNumberOfWhitePiecesLeftLabel = $"Max number of white pieces left: { wins["MaxNumberOfWhitePiecesLeft"]}";
            RedWinsLabel = $"Red won for {wins["red"]} times";
            MaxNumberOfRedPiecesLeftLabel = $"Max number of red pieces left: {wins["MaxNumberOfRedPiecesLeft"]}";
        }

        #region Properties and members

        public string WhiteWinsLabel { get; set; }
        public string MaxNumberOfWhitePiecesLeftLabel { get; set; }
        public string RedWinsLabel { get; set; }
        public string MaxNumberOfRedPiecesLeftLabel { get; set; }

        #endregion

    }

}
