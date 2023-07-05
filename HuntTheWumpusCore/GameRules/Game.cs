using HuntTheWumpusCore.GameRules.MapGenerator;

namespace HuntTheWumpusCore.GameRules
{
    public class Game 
    {
        private int[] _pit;
        private int[] _bats;
        private int[] _wumpus;
        private int[] _player;
        private int _mapSize;
        public Boolean GameOver;
        
        public Game(IMapGenerator mapGenerator)
        { 
            this._mapSize = mapGenerator.getMapSize();
            
            this._player = mapGenerator.getPlayerLocation();
            this._wumpus = mapGenerator.getWumpusLocation();
            this._pit = mapGenerator.getPitLocation();
            this._bats = mapGenerator.getBatsLocation();

            this.GameOver = false;
        }

        public CommandResponse processCommand(Command command)
        {
            if (command.isMove()) {
                CommandResponse commandResponse = this.MovePlayer(command);
                    
                if (this.IsPlayerOn(this._wumpus)) {
                    this.GameOver = true;
                    return CommandResponse.AteByWumpus;
                }

                if (this.IsPlayerOn(this._bats)) {
                    return BatsMovePlayer();
                }

                if (this.IsPlayerOn(this._pit)) {
                    this.GameOver = true;
                    return CommandResponse.FellInPit;
                }

                return commandResponse;
            }

            if (command.isShoot()) {
                return this.Shoot(command);
            }

            return CommandResponse.InvalidCommand;
            
        }

        private CommandResponse BatsMovePlayer()
        {
            this._bats[0] = -1;
            this._bats[1] = -1;

            List<int[]> occupiedCells = new List<int[]>();
            occupiedCells.Add(new int[]{this._wumpus[0], this._wumpus[1]});
            occupiedCells.Add(new int[]{this._pit[0], this._pit[1]});
            occupiedCells.Add(new int[]{this._player[0], this._player[1]}); // Add the player so you don't end up back on the same cell

            int[] cell = this.SelectUnoccupiedCell(occupiedCells);
            this._player[0] = cell[0]; this._player[1] = cell[1];

            return CommandResponse.MovedByBats;
        }

        public CurrentLocation GetCurrentLocation()
        {
            return new CurrentLocation(
                this.IsPlayerNextTo(this._bats),
                this.IsPlayerNextTo(this._wumpus),
                this.IsPlayerNextTo(this._pit)
            );
        }

        private bool IsPlayerNextTo(int[] coords)
        {
            // Do x coords match and y off by one?
            if (coords[0] == this._player[0] && this.AreCoordsNextToEachOther(this._player[1], coords[1])) {
                return true;
            }

            // Do y coords match and x off by one?
            if (coords[1] == this._player[1] && this.AreCoordsNextToEachOther(this._player[0], coords[0])) {
                return true;
            }

            return false;
        }

        private bool AreCoordsNextToEachOther(int x, int y)
        {
            return (x - y) == 1 || (y - x) == 1;
        }

        private CommandResponse MovePlayer(Command command)
        {
            bool isValidMove = false;

            switch (command)
            {
                case Command.MoveUp:
                    isValidMove = this._player[1] != this._mapSize;
                    if (isValidMove) {
                        this._player[1]++;
                    }
                    break;

                case Command.MoveDown:
                    isValidMove = this._player[1] != 0;
                    if (isValidMove) {
                        this._player[1]--;
                    }
                    break;

                case Command.MoveLeft:
                    isValidMove = this._player[0] != 0;
                    if (isValidMove) {
                        this._player[0]--;
                    }
                    break;

                case Command.MoveRight:
                    isValidMove = this._player[0] != this._mapSize;
                    if (isValidMove) {
                        this._player[0]++;
                    }
                    break;
            }

            return isValidMove ? CommandResponse.Moved : CommandResponse.FailedToMove;
        }

        private CommandResponse Shoot(Command command)
        {
            bool isShotHit = IsPlayerAdjacentToWumpus(command);

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
        {
            return this._player[0] == coords[0] & this._player[1] == coords[1];
        }
        private int[] SelectUnoccupiedCell(List<int[]> occupiedCells)
        {
            Random rnd = new Random();
            bool occupied;
            int[] selectedCell;
            int x;
            int y;

            do {
                x = rnd.Next(this._mapSize);
                y = rnd.Next(this._mapSize);
                selectedCell = new int[2]{x, y};
                
                occupied = false;
                occupiedCells.ForEach((occupiedCell) => {
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