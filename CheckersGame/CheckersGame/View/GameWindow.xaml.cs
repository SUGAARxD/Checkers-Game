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
        public GameWindow(bool allowMultipleJump, CustomSettings settings)
        {
            InitializeComponent();
            DataContext = new GameVM(allowMultipleJump, settings);
        }

        public GameWindow(object game, CustomSettings settings)
        {
            InitializeComponent();
            DataContext = new GameVM(game, settings);
        }

    }
}
