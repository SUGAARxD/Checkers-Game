using CheckersGame.Model;

namespace CheckersGame.ViewModel
{
    internal class GameVM
    {
        public GameVM ()
        {

        }

        public GameVM(bool allowMultipleJump)
        {

        }

        public GameVM(object game)
        {
            _game = (GameModel)game;
        }

        #region Properties and members

        private GameModel _game;

        #endregion

    }
}
