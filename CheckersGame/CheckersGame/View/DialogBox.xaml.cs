using CheckersGame.ViewModel;
using System.Windows;

namespace CheckersGame.View
{
    /// <summary>
    /// Interaction logic for DialogBox.xaml
    /// </summary>
    public partial class DialogBox : Window
    {
        public DialogBox(string message, string buttonContent)
        {
            InitializeComponent();
            _dialog = new DialogBoxVM(message, buttonContent);
            DataContext = _dialog;
        }

        private readonly DialogBoxVM _dialog;

        public string GetText()
        {
            return _dialog.TextBoxContent;
        }
    }
}
