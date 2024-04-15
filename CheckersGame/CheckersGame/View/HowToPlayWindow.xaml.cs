using CheckersGame.Model;
using CheckersGame.ViewModel;
using System.Windows;

namespace CheckersGame.View
{
    /// <summary>
    /// Interaction logic for HowToPlayWindow.xaml
    /// </summary>
    public partial class HowToPlayWindow : Window
    {
        public HowToPlayWindow(CustomSettings settings)
        {
            InitializeComponent();
            DataContext = new HowToPlayVM(settings);
        }
    }
}
