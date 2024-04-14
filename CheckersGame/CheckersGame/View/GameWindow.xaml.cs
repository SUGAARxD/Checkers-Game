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
        public GameWindow(CustomSettings settings)
        {
            InitializeComponent();
            DataContext = new GameVM(settings);
        }

        public GameWindow(object save, CustomSettings settings)
        {
            InitializeComponent();
            DataContext = new GameVM(save, settings);
        }

    }
}
