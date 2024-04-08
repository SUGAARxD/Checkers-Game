using CheckersGame.Model;
using System.Collections.ObjectModel;

namespace CheckersGame.ViewModel
{
    internal class GameVM
    {
        public GameVM ()
        {
        }

        public GameVM(bool allowMultipleJump)
        {
            _game = new GameModel();
            Board = new ObservableCollection<ObservableCollection<PieceModel>>();
            UpdateBoard();
        }

        public GameVM(object game)
        {
            _game = (GameModel)game;
            Board = new ObservableCollection<ObservableCollection<PieceModel>>();
            UpdateBoard();
        }

        #region Properties and members

        private GameModel _game;
        public ObservableCollection<ObservableCollection<PieceModel>> Board;

        #endregion

        #region Methods

        private void UpdateBoard()
        {
            var board = _game.GetBoard();
            Board.Clear();
            for (int index = 0; index < board.Count; index++)
            {
                Board.Add(new ObservableCollection<PieceModel>(board[index]));
            }
        }

        #endregion

    }
}
