using HuntTheWumpusCore.GameRules.MapGenerator;
using HuntTheWumpusCore.GameRules.Models;

namespace HuntTheWumpusCore.GameRules
{
    public class Game(IMapGenerator mapGenerator)
    {
        private Position _pit = mapGenerator.GetPitLocation();
        private Position _bats = mapGenerator.GetBatsLocation();
        private Position _wumpus = mapGenerator.GetWumpusLocation();
        private Position _player = mapGenerator.GetPlayerLocation();
        private int _mapSize = mapGenerator.GetMapSize();
        public bool GameOver = false;

        public CommandResponse ProcessCommand(Command command)
        {
            if (command.IsMove()) {
                var commandResponse = MovePlayer(command);
                    
                if (IsPlayerOn(_wumpus)) {
                    GameOver = true;
                    return CommandResponse.AteByWumpus;
                }

                if (IsPlayerOn(_bats)) {
                    return BatsMovePlayer();
                }

                if (IsPlayerOn(_pit)) {
                    GameOver = true;
                    return CommandResponse.FellInPit;
                }

                return commandResponse;
            }

            if (command.IsShoot()) {
                return Shoot(command);
            }

            return CommandResponse.InvalidCommand;
            
        }

        private CommandResponse BatsMovePlayer()
        {
            _bats.X = -1;
            _bats.Y = -1;

            var occupiedCells = new List<int[]>
            {
                new[] { _wumpus.X, _wumpus.Y },
                new[] { _pit.X, _pit.Y },
                new[] {_player.X, _player.Y} // Add the player, so you don't end up back on the same cell
            };

            var cell = SelectUnoccupiedCell(occupiedCells);
            _player.X = cell[0]; _player.Y = cell[1];

            return CommandResponse.MovedByBats;
        }

        public CurrentLocation GetCurrentLocation()
        {
            return new CurrentLocation(
                IsPlayerNextTo(_bats),
                IsPlayerNextTo(_wumpus),
                IsPlayerNextTo(_pit)
            );
        }

        private bool IsPlayerNextTo(Position coords) 
            => _player.IsNextTo(coords);

        private CommandResponse MovePlayer(Command command)
        {
            var isValidMove = false;

            switch (command)
            {
                case Command.MoveUp:
                    isValidMove = _player.Y != _mapSize;
                    if (isValidMove) {
                        _player.Y++;
                    }
                    break;

                case Command.MoveDown:
                    isValidMove = _player.Y != 0;
                    if (isValidMove) {
                        _player.Y--;
                    }
                    break;

                case Command.MoveLeft:
                    isValidMove = _player.X != 0;
                    if (isValidMove) {
                        _player.X--;
                    }
                    break;

                case Command.MoveRight:
                    isValidMove = _player.X != _mapSize;
                    if (isValidMove) {
                        _player.X++;
                    }
                    break;
            }

            return isValidMove ? CommandResponse.Moved : CommandResponse.FailedToMove;
        }

        private CommandResponse Shoot(Command command)
        {
            var isShotHit = IsPlayerAdjacentToWumpus(command);

            if (isShotHit)  
            {
                GameOver = true;
                return CommandResponse.ShotHit;
            }

            return CommandResponse.ShotMissed;
        }

        private bool IsPlayerAdjacentToWumpus(Command command)
        {
            switch (command)
            {
                case Command.ShootUp:
                    return _player.X == _wumpus.X && _player.Y + 1 == _wumpus.Y;

                case Command.ShootDown:
                    return _player.X == _wumpus.X && _player.Y - 1 == _wumpus.Y;

                case Command.ShootLeft:
                    return _player.Y == _wumpus.Y && _player.X - 1 == _wumpus.X;

                case Command.ShootRight:
                    return _player.Y == _wumpus.Y && _player.X + 1 == _wumpus.X;
            }

            return false; // Default case, should never be reached
        }

        private bool IsPlayerOn(Position coords) => _player.IsOn(coords);

        private int[] SelectUnoccupiedCell(List<int[]> occupiedCells)
        {
            var rnd = new Random();
            bool occupied;
            int[] selectedCell;

            do {
                var x = rnd.Next(_mapSize);
                var y = rnd.Next(_mapSize);
                selectedCell = [x, y];
                
                occupied = false;
                occupiedCells.ForEach(occupiedCell => {
                    // Check if selected cell matches any occupied cells
                    if (occupiedCell[0] == selectedCell[0] && occupiedCell[1] == selectedCell[1]) {
                        occupied = true;
                    }
                });
            } while (occupied);

            return selectedCell;
        }
    }
}