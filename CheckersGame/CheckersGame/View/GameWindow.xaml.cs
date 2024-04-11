using System.Windows;
using CheckersGame.ViewModel;
using CheckersGame.Model;

namespace CheckersGame.View
{
    /// <summary>
    /// Interaction logic for GameView.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        public GameWindow(bool allowMultipleJump, Theme theme)
        {
            InitializeComponent();
            DataContext = new GameVM(allowMultipleJump, theme);
        }

        public GameWindow(object game, Theme theme)
        {
            InitializeComponent();
            DataContext = new GameVM(game, theme);
        }

    }
}
