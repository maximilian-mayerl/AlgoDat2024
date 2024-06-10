# Foliensatz 8: Monte Carlo Tree Search

## Übung 1: State Encoding für Tic-Tac-Toe

```csharp
enum TicTacToeFieldState {
    Free,
    Cross,
    Circle
}
class TicTacToeState {
    // We can simply store the game state as a two-dimensional 3x3 array.
    // Everything else can be derived from this.
    public TicTacToeFieldState[,] State { get; set; }

    public TicTacToeState() {
        this.State = new TicTacToeFieldState[3, 3];
    }
    public TicTacToeState(TicTacToeFieldState[,] state) {
        this.State = state;
    }

    public TicTacToeFieldState NextPlayer {
        get {
            // Next player is the one that has fewer fields in the state,
            // or `Cross` if equal.
            var stateEntries = this.State.Cast<TicTacToeFieldState>();

            int numberCrosses = stateEntries.Count(s => s == TicTacToeFieldState.Cross);
            int numberCircles = stateEntries.Count(s => s == TicTacToeFieldState.Circle);

            return (numberCircles < numberCrosses) ? TicTacToeFieldState.Circle : TicTacToeFieldState.Cross;
        }
    }

    public TicTacToeFieldState Winner {
        get {
            // Winner is `Free` if the game is not over yet, or is a draw.
            // The game is over if any player has three in a row, or if the field is full.
            // There are eight possible rows that can take three of the same symbol.

            // First row.
            if (State[0, 0] != TicTacToeFieldState.Free && State[0, 0] == State[0, 1] && State[0, 1] == State[0, 2]) {
                return State[0, 0];
            }
            // Second row.
            if (State[1, 0] != TicTacToeFieldState.Free && State[1, 0] == State[1, 1] && State[1, 1] == State[1, 2]) {
                return State[1, 0];
            }
            // Third row.
            if (State[2, 0] != TicTacToeFieldState.Free && State[2, 0] == State[2, 1] && State[2, 1] == State[2, 2]) {
                return State[2, 0];
            }

            // First column.
            if (State[0, 0] != TicTacToeFieldState.Free && State[0, 0] == State[1, 0] && State[1, 0] == State[2, 0]) {
                return State[0, 0];
            }
            // Second column.
            if (State[0, 1] != TicTacToeFieldState.Free && State[0, 1] == State[1, 1] && State[1, 1] == State[2, 1]) {
                return State[0, 1];
            }
            // Third column.
            if (State[0, 2] != TicTacToeFieldState.Free && State[0, 2] == State[1, 2] && State[1, 2] == State[2, 2]) {
                return State[0, 2];
            }

            // First diagonal.
            if (State[0, 0] != TicTacToeFieldState.Free && State[0, 0] == State[1, 1] && State[1, 1] == State[2, 2]) {
                return State[0, 0];
            }
            // Second diagonal.
            if (State[0, 2] != TicTacToeFieldState.Free && State[0, 2] == State[1, 1] && State[1, 1] == State[2, 0]) {
                return State[0, 2];
            }

            // In all other cases, the game is not over yet.
            return TicTacToeFieldState.Free;
        }
    }

    public bool IsGameOver {
        get {
            // The game is over if
            // (1) the winner is someone other than `Free`
            // (2) the grid is full.
            var stateEntries = this.State.Cast<TicTacToeFieldState>();

            return this.Winner != TicTacToeFieldState.Free
                || stateEntries.Count(s => s == TicTacToeFieldState.Free) == 0;
        }
    }
}
```
