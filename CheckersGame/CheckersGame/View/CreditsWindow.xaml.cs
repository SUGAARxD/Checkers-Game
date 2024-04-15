using CheckersGame.Model;
using CheckersGame.ViewModel;
using System.Windows;

namespace CheckersGame.View
{
    /// <summary>
    /// Interaction logic for CreditsWindow.xaml
    /// </summary>
    public partial class CreditsWindow : Window
    {
        public CreditsWindow(CustomSettings settings)
        {
            InitializeComponent();
            DataContext = new CreditsVM(settings);
        }
    }
}
