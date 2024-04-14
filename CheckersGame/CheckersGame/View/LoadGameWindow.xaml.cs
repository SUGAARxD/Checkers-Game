using CheckersGame.Model;
using CheckersGame.ViewModel;
using System.Windows;

namespace CheckersGame.View
{
    /// <summary>
    /// Interaction logic for LoadGameWindow.xaml
    /// </summary>
    public partial class LoadGameWindow : Window
    {
        public LoadGameWindow(CustomSettings settings)
        {
            InitializeComponent();
            DataContext = new LoadGameVM(settings);
        }
    }
}
