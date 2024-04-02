using System.Windows;
using CheckersGame.ViewModel;

namespace CheckersGame.View
{
    /// <summary>
    /// Interaction logic for GameView.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        public GameWindow(bool allowMultipleJump)
        {
            InitializeComponent();
            DataContext = new GameVM(allowMultipleJump);
        }

        public GameWindow(object game)
        {
            InitializeComponent();
            DataContext = new GameVM(game);
        }

    }
}
