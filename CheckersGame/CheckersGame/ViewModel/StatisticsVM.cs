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
            RedWinsLabel = $"Red won for {wins["red"]} times";
        }

        #region Properties and members

        public string WhiteWinsLabel { get; set; }
        public string RedWinsLabel { get; set; }

        #endregion

    }

}
