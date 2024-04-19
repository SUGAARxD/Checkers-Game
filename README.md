# Checkers-Game

This is a C# project utilizing WPF (.NET Framework) and MVVM (Model-View-ViewModel) design pattern implementing a checkers game.

## Description

The Checkers game features two sets of pieces: white and red. The game board is 8x8. The player with the red pieces makes the first move, followed by alternating moves from each player. The application visually indicates which player's turn it is and displays the number of remaining pieces for each player.

### Types of Moves:

- **Simple Move**: A piece moves one square diagonally forward. If a piece reaches the opponent's back row, it becomes a "king" and can move both forward and backward diagonally.
- **Capture**: If a player's piece can capture an opponent's piece by jumping over it diagonally, the captured piece is removed from the board. The player must capture if possible.
- **Multiple Jumps**: If a player captures an opponent's piece and there's another capture opportunity nearby, the player must continue capturing until no more captures are possible.

### End of Game:
The game ends when one player has no more pieces left on the board or has no possible moves that can make. The opponent is declared the winner.

## Main Features

- Display of the game board and current player.
- Visual representation of the board and pieces.
- Ability to save and load game states (Memento design pattern -> Snapshot).
- Option for multiple jumps.
- Statistics tracking for winners and maximum remaining pieces.

## Menu Options

- **New Game**: Starts a new game with standard settings.
- **Load Game**: Opens a previously saved game state.
- **How to play**: Displays a brief description of the game.
- **Settings**: Has a toggle for multiple jumps and possibility to change the game theme.
- **Statistics**: Displays statistics of white and red winners, as well as the maximum remaining pieces at the end of a game.
- **Credits**: Displays information about the creator of the program, institutional email address and group.

 ## Aditional Features
 
 Ability to change the theme of the application, option found in settings.
 
![image](https://github.com/SUGAARxD/Checkers-Game/assets/80158909/5f8f9650-f99f-4a79-bce0-3d0c2bca3a4e)
![image](https://github.com/SUGAARxD/Checkers-Game/assets/80158909/ec1bd5b5-9a51-4f6c-b489-3f099d1aece9)
![image](https://github.com/SUGAARxD/Checkers-Game/assets/80158909/815f8f00-0840-4f99-97a2-d3c25048857a)
