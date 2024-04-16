using CheckersGame.View;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace CheckersGame.ViewModel
{
    internal class DialogBoxVM
    {
        public DialogBoxVM()
        { 
        }

        public DialogBoxVM(string message, string buttonContent)
        {
            DialogBoxMessage = message;
            ButtonContent = buttonContent;
        }

        #region Properties and members

        public string TextBoxContent { get; set; }
        public string DialogBoxMessage { get; set; }
        public string ButtonContent { get; set; }

        #endregion

        #region Commands

        private ICommand _closeWindowCommand;
        public ICommand CloseWindowCommand
        {
            get
            {
                if (_closeWindowCommand == null)
                    _closeWindowCommand = new RelayCommand(ExecuteClose);
                return _closeWindowCommand;
            }
        }

        private void ExecuteClose(object parameter)
        {
            Application.Current.Windows.OfType<DialogBox>().First().Close();
        }

        #endregion

    }
}
