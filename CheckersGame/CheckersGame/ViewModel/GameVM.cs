using CheckersGame.Model;
using CheckersGame.Utilities;
using CheckersGame.View;
using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace CheckersGame.ViewModel
{
    internal class GameVM : BaseNotify
    {
        public GameVM()
        {
        }

        public GameVM(CustomSettings settings)
        {
            _settings = settings;
            _game = new GameModel();
            Init();
            UpdateBoardPiecesPositions();
            _game.AllowMultipleJump = _settings.AllowMultipleJump;
        }

        public GameVM(object save, CustomSettings settings)
        {
            _settings = settings;
            _game = new GameModel((SavedGameModel)save);
            Init();
            UpdateBoardPiecesPositions();
        }

        #region Properties and members

        private CustomSettings _settings;
        public CustomSettings MySettings
        {
            get => _settings;
            set
            {
                _settings = value;
                NotifyPropertyChanged(nameof(MySettings));
            }
        }

        private readonly GameModel _game;

        public ObservableCollection<ObservableCollection<BoardCellModel>> Board { get; set; }

        public BoardCellModel SelectedPiece { get; set; }

        private List<Tuple<int, int>> _availableMoves;

        private int _numberOfWhitePieces;
        public int NumberOfWhitePieces
        {
            get => _numberOfWhitePieces;
            set
            {
                _numberOfWhitePieces = value;
                NotifyPropertyChanged(nameof(NumberOfWhitePieces));
            }
        }

        private int _numberOfRedPieces;
        public int NumberOfRedPieces
        {
            get => _numberOfRedPieces;
            set
            {
                _numberOfRedPieces = value;
                NotifyPropertyChanged(nameof(NumberOfRedPieces));
            }
        }

        private string _currentPieceTurn;
        public string CurrentPlayerTurn
        {
            get => _currentPieceTurn;
            set
            {
                _currentPieceTurn = value;
                NotifyPropertyChanged(nameof(CurrentPlayerTurn));
            }
        }

        private bool _canChangeTheColor;
        private bool _isMultipleJumpActive;

        #endregion

        #region Commands

        private ICommand _saveGameCommand;
        public ICommand SaveGameCommand
        {
            get
            {
                if (_saveGameCommand == null)
                    _saveGameCommand = new RelayCommand(ExecuteSaveGame);
                return _saveGameCommand;
            }
        }

        private void ExecuteSaveGame(object parameter)
        {
            string saveName;
            DialogBox dialogBox = new DialogBox("Enter save name", "Save");
            dialogBox.ShowDialog();
            saveName = dialogBox.GetText();

            if (string.IsNullOrEmpty(saveName))
                return;

            string pattern = @"^[a-zA-Z0-9]+(?:_[a-zA-Z0-9]+)*$";
            if (!Regex.IsMatch(saveName, pattern))
            {
                MessageBox.Show("Invalid name");
                return;
            }

            if (FileHelper.SaveExists(saveName))
            {
                if (MessageBox.Show("Save already exists!\nDo you want to overwrite it?", "Save", MessageBoxButton.YesNo) == MessageBoxResult.No)
                    return;
            }

            _game.SaveGame(saveName);
        }

        private ICommand _loadGameCommand;
        public ICommand LoadGameCommand
        {
            get
            {
                if (_loadGameCommand == null)
                    _loadGameCommand = new RelayCommand(ExecuteLoadGame);
                return _loadGameCommand;
            }
        }

        private void ExecuteLoadGame(object parameter)
        {
            LoadGameWindow loadGameWindow = new LoadGameWindow(_settings);
            loadGameWindow.ShowDialog();
        }

        private ICommand _settingsCommand;
        public ICommand SettingsCommand
        {
            get
            {
                if (_settingsCommand == null)
                    _settingsCommand = new RelayCommand(ExecuteSettings);
                return _settingsCommand;
            }
        }

        private void ExecuteSettings(object parameter)
        {
            SettingsWindow settingsWindow = new SettingsWindow(_settings);
            settingsWindow.ShowDialog();
            UpdateBoardPiecesPositions();
            UpdateBoardBackground();
            ColorAvailableMovesBorder();
            if (SelectedPiece != null)
                SelectedPiece.CellBorderColor = MySettings.MyTheme.SelectedCellBorderColor;
        }

        private ICommand _exitGameCommand;
        public ICommand ExitGameCommand
        {
            get
            {
                if (_exitGameCommand == null)
                    _exitGameCommand = new RelayCommand(ExecuteExitGame);
                return _exitGameCommand;
            }
        }

        private void ExecuteExitGame(object parameter)
        {
            MenuWindow menu = new MenuWindow();
            menu.Show();
            Application.Current.Windows.OfType<GameWindow>().First().Close();
        }

        #endregion

        #region Methods

        private void UpdateBoardBackground()
        {
            for (int index = 0; index < GameModel.numberOfLines; index++)
            {
                for (int jndex = 0; jndex < GameModel.numberOfColumns; jndex++)
                {
                    if ((index + jndex) % 2 == 0)
                    {
                        Board[index][jndex].BackgroundImage = FileHelper.GetImage(MySettings.MyTheme.WhiteSquareImagePath);
                    }
                    else
                    {
                        Board[index][jndex].BackgroundImage = FileHelper.GetImage(MySettings.MyTheme.BlackSquareImagePath);
                    }
                }
            }
        }

        private void Init()
        {
            Board = new ObservableCollection<ObservableCollection<BoardCellModel>>();
            for (int index = 0; index < GameModel.numberOfLines; index++)
            {
                ObservableCollection<BoardCellModel> line = new ObservableCollection<BoardCellModel>();
                for (int jndex = 0; jndex < GameModel.numberOfColumns; jndex++)
                {
                    BoardCellModel boardCell = new BoardCellModel(ExecuteClickAction)
                    {
                        XPos = index,
                        YPos = jndex
                    };

                    if ((index + jndex) % 2 == 0)
                    {
                        boardCell.BackgroundImage = FileHelper.GetImage(MySettings.MyTheme.WhiteSquareImagePath);
                    }
                    else
                    {
                        boardCell.BackgroundImage = FileHelper.GetImage(MySettings.MyTheme.BlackSquareImagePath);
                    }

                    boardCell.PieceImage = null;
                    boardCell.CellBorderColor = MySettings.MyTheme.NormalBorderColor;
                    line.Add(boardCell);
                }
                Board.Add(line);
            }
            _availableMoves = new List<Tuple<int, int>>();
            SelectedPiece = null;
            _canChangeTheColor = false;
            _isMultipleJumpActive = false;
            CurrentPlayerTurn = _game.CurrentPlayerColor;
        }

        private void UpdateBoardPiecesPositions()
        {
            NumberOfWhitePieces = 0;
            NumberOfRedPieces = 0;
            for (int index = 0; index < GameModel.numberOfLines; index++)
            {
                for (int jndex = 0; jndex < GameModel.numberOfColumns; jndex++)
                {
                    if (_game.PieceIsNull(index, jndex))
                        Board[index][jndex].PieceImage = null;
                    else
                    {
                        switch (_game.GetPieceTypeForPiece(index, jndex))
                        {
                            default:
                                break;

                            case PieceType.WhitePiece:

                                NumberOfWhitePieces++;

                                Board[index][jndex].Color = "white";

                                if (_game.IsPieceKing(index, jndex))
                                    Board[index][jndex].PieceImage = FileHelper.GetImage(MySettings.MyTheme.WhiteKingImagePath);
                                else
                                    Board[index][jndex].PieceImage = FileHelper.GetImage(MySettings.MyTheme.WhitePieceImagePath);

                                break;

                            case PieceType.RedPiece:

                                NumberOfRedPieces++;

                                Board[index][jndex].Color = "red";

                                if (_game.IsPieceKing(index, jndex))
                                    Board[index][jndex].PieceImage = FileHelper.GetImage(MySettings.MyTheme.RedKingImagePath);
                                else
                                    Board[index][jndex].PieceImage = FileHelper.GetImage(MySettings.MyTheme.RedPieceImagePath);

                                break;

                        }
                    }
                }
            }
        }

        private void ExecuteClickAction(object parameter)
        {
            if (!(parameter is BoardCellModel clickedCell))
                return;

            if (SelectedPiece == null && !_game.PieceIsNull(clickedCell.XPos, clickedCell.YPos))
            {
                if (clickedCell.Color == CurrentPlayerTurn)
                {
                    SelectedPiece = clickedCell;
                    SelectedPiece.CellBorderColor = MySettings.MyTheme.SelectedCellBorderColor;

                    _availableMoves = _game.AvailableMoves(SelectedPiece.XPos, SelectedPiece.YPos);
                    ColorAvailableMovesBorder();
                }
            }
            else
            {
                if (SelectedPiece == null)
                    return;

                if (IsInAvailableMoves(clickedCell))
                {
                    _game.MovePiece(Tuple.Create(SelectedPiece.XPos, SelectedPiece.YPos), Tuple.Create(clickedCell.XPos, clickedCell.YPos));
                    UpdateBoardPiecesPositions();
                    VerifyIfGameEnds();

                    if (_game.IsTwoCellsDifference(Tuple.Create(SelectedPiece.XPos, SelectedPiece.YPos), Tuple.Create(clickedCell.XPos, clickedCell.YPos)))
                    {
                        if (_game.AllowMultipleJump == true)
                        {
                            ResetBordersColor();
                            SelectedPiece = clickedCell;

                            _availableMoves = _game.AvailableMoves(SelectedPiece.XPos, SelectedPiece.YPos);
                            UpdateAvailableMovesForMultipleJumps();
                            if (_availableMoves.Count > 0)
                            {
                                SelectedPiece.CellBorderColor = MySettings.MyTheme.SelectedCellBorderColor;
                                ColorAvailableMovesBorder();
                                _isMultipleJumpActive = true;
                                return;
                            }
                            _isMultipleJumpActive = false;
                        }
                    }
                    _canChangeTheColor = true;

                }

                if (_isMultipleJumpActive == true)
                    return;

                SelectedPiece = null;
                ResetBordersColor();
                _availableMoves.Clear();
                if (_canChangeTheColor == true)
                {
                    _canChangeTheColor = false;
                    CurrentPlayerTurn = (CurrentPlayerTurn == "white") ? "red" : "white";
                    _game.CurrentPlayerColor = CurrentPlayerTurn;
                }
            }
        }

        private void ColorAvailableMovesBorder()
        {
            foreach (var position in _availableMoves)
            {
                Board[position.Item1][position.Item2].CellBorderColor = MySettings.MyTheme.AvailableMoveBorderColor;
            }
        }

        private void ResetBordersColor()
        {
            for (int index = 0; index < GameModel.numberOfLines; index++)
            {
                for (int jndex = 0; jndex < GameModel.numberOfColumns; jndex++)
                {
                    Board[index][jndex].CellBorderColor = MySettings.MyTheme.NormalBorderColor;
                }
            }
        }

        private bool IsInAvailableMoves(BoardCellModel piece)
        {
            foreach (var position in _availableMoves)
            {
                if (position.Item1 == piece.XPos && position.Item2 == piece.YPos)
                    return true;
            }
            return false;
        }

        private void UpdateAvailableMovesForMultipleJumps()
        {
            List<Tuple<int, int>> newAvailableMoves = new List<Tuple<int, int>>();

            foreach (var move in _availableMoves)
            {
                if (_game.IsTwoCellsDifference(Tuple.Create(SelectedPiece.XPos, SelectedPiece.YPos), Tuple.Create(move.Item1, move.Item2)))
                {
                    newAvailableMoves.Add(move);
                }
            }
            _availableMoves = newAvailableMoves;

        }

        private void VerifyIfGameEnds()
        {
            string winner = string.Empty;
            int numberOfWinnerPiecesLeft = 0;
            if (NumberOfWhitePieces == 0 || !_game.CanMakeAMove(PieceType.WhitePiece))
            {
                winner = "red";
                numberOfWinnerPiecesLeft = NumberOfRedPieces;
            }
            else if (NumberOfRedPieces == 0 || !_game.CanMakeAMove(PieceType.RedPiece))
            {
                winner = "white";
                numberOfWinnerPiecesLeft = NumberOfWhitePieces;
            }

            if (!string.IsNullOrEmpty(winner))
            {

                ResetBordersColor();

                FileHelper.UpdateWins(winner, numberOfWinnerPiecesLeft);

                if (winner == "white")
                {
                    MessageBox.Show("White Wins!", "End Game!");
                }
                else
                {
                    MessageBox.Show("Red Wins!", "End Game!");
                }

                MenuWindow menuWindow = new MenuWindow();
                menuWindow.Show();

                Application.Current.Windows.OfType<GameWindow>().First().Close();
            }
        }

        #endregion

    }
}
