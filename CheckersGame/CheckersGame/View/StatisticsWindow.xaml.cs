using CheckersGame.Model;
using CheckersGame.ViewModel;
using System.Windows;

namespace CheckersGame.View
{
    /// <summary>
    /// Interaction logic for StatisticsWindow.xaml
    /// </summary>
    public partial class StatisticsWindow : Window
    {
        public StatisticsWindow(CustomSettings settings)
        {
            InitializeComponent();
            DataContext = new StatisticsVM(settings);
        }
    }
}
