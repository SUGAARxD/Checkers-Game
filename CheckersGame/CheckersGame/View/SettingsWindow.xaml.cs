using CheckersGame.Model;
using CheckersGame.ViewModel;
using System.Windows;

namespace CheckersGame.View
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow(CustomSettings settings)
        {
            InitializeComponent();
            DataContext = new SettingsVM(settings);
        }
    }
}
