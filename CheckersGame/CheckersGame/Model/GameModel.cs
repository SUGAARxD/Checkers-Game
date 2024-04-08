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
                        line.Add(new PieceModel(/*white*/));
                        continue;
                    }

                    if (index > 4 && (index + jndex) % 2 == 1)
                    {
                        line.Add(new PieceModel(/*red*/));
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

        #endregion

    }
}
