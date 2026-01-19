using HuntTheWumpusCore.GameRules.MapGenerator;

namespace HuntTheWumpusCore.GameRules
{
    public class Game(IMapGenerator mapGenerator)
    {
        private int[] _pit = mapGenerator.GetPitLocation();
        private int[] _bats = mapGenerator.GetBatsLocation();
        private int[] _wumpus = mapGenerator.GetWumpusLocation();
        private int[] _player = mapGenerator.GetPlayerLocation();
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
            _bats[0] = -1;
            _bats[1] = -1;

            var occupiedCells = new List<int[]>
            {
                new[] { _wumpus[0], _wumpus[1] },
                new[] { _pit[0], _pit[1] },
                new[] {_player[0], _player[1]} // Add the player, so you don't end up back on the same cell
            };

            var cell = SelectUnoccupiedCell(occupiedCells);
            _player[0] = cell[0]; _player[1] = cell[1];

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

        private bool IsPlayerNextTo(int[] coords)
        {
            // Do x coords match and y off by one?
            if (coords[0] == _player[0] && AreCoordsNextToEachOther(_player[1], coords[1])) {
                return true;
            }

            // Do y coords match and x off by one?
            if (coords[1] == _player[1] && AreCoordsNextToEachOther(_player[0], coords[0])) {
                return true;
            }

            return false;
        }

        private bool AreCoordsNextToEachOther(int x, int y) 
            => x - y == 1 || y - x == 1;

        private CommandResponse MovePlayer(Command command)
        {
            var isValidMove = false;

            switch (command)
            {
                case Command.MoveUp:
                    isValidMove = _player[1] != _mapSize;
                    if (isValidMove) {
                        _player[1]++;
                    }
                    break;

                case Command.MoveDown:
                    isValidMove = _player[1] != 0;
                    if (isValidMove) {
                        _player[1]--;
                    }
                    break;

                case Command.MoveLeft:
                    isValidMove = _player[0] != 0;
                    if (isValidMove) {
                        _player[0]--;
                    }
                    break;

                case Command.MoveRight:
                    isValidMove = _player[0] != _mapSize;
                    if (isValidMove) {
                        _player[0]++;
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
                    return _player[0] == _wumpus[0] && _player[1] + 1 == _wumpus[1];

                case Command.ShootDown:
                    return _player[0] == _wumpus[0] && _player[1] - 1 == _wumpus[1];

                case Command.ShootLeft:
                    return _player[1] == _wumpus[1] && _player[0] - 1 == _wumpus[0];

                case Command.ShootRight:
                    return _player[1] == _wumpus[1] && _player[0] + 1 == _wumpus[0];
            }

            return false; // Default case, should never be reached
        }

        private bool IsPlayerOn(int[] coords)
            => _player[0] == coords[0] & _player[1] == coords[1];
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