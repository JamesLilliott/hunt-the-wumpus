using HuntTheWumpusCore.GameRules.MapGenerator;

namespace HuntTheWumpusCore.GameRules
{
    public class Game 
    {
        private int[] pit = new int[2];
        private int[] bats = new int[2];
        private int[] wumpus = new int[2];
        private int[] player = new int[2];
        private int mapSize;
        public Boolean gameOver;
        
        public Game(IMapGenerator mapGenerator)
        { 
            this.mapSize = mapGenerator.getMapSize();
            
            this.player = mapGenerator.getPlayerLocation();
            this.wumpus = mapGenerator.getWumpusLocation();
            this.pit = mapGenerator.getPitLocation();
            this.bats = mapGenerator.getBatsLocation();

            this.gameOver = false;
        }

        public CommandResponse processCommand(Command command)
        {
            if (command.isMove()) {
                CommandResponse commandResponse = this.MovePlayer(command);
                    
                if (this.IsPlayerOn(this.wumpus)) {
                    this.gameOver = true;
                    return CommandResponse.AteByWumpus;
                }

                if (this.IsPlayerOn(this.bats)) {
                    return BatsMovePlayer();
                }

                if (this.IsPlayerOn(this.pit)) {
                    this.gameOver = true;
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
            this.bats[0] = -1;
            this.bats[1] = -1;

            List<int[]> occupiedCells = new List<int[]>();
            occupiedCells.Add(new int[]{this.wumpus[0], this.wumpus[1]});
            occupiedCells.Add(new int[]{this.pit[0], this.pit[1]});
            occupiedCells.Add(new int[]{this.player[0], this.player[1]}); // Add the player so you don't end up back on the same cell

            int[] cell = this.SelectUnoccupidCell(occupiedCells);
            this.player[0] = cell[0]; this.player[1] = cell[1];

            return CommandResponse.MovedByBats;
        }

        public CurrentLocation GetCurrentLocation()
        {
            return new CurrentLocation(
                this.isPlayerNextTo(this.bats),
                this.isPlayerNextTo(this.wumpus),
                this.isPlayerNextTo(this.pit)
            );
        }

        private bool isPlayerNextTo(int[] coords)
        {
            // Do x coords match and y off by one?
            if (coords[0] == this.player[0] && this.areCoordsNextToEachOther(this.player[1], coords[1])) {
                return true;
            }

            // Do y coords match and x off by one?
            if (coords[1] == this.player[1] && this.areCoordsNextToEachOther(this.player[0], coords[0])) {
                return true;
            }

            return false;
        }

        private bool areCoordsNextToEachOther(int x, int y)
        {
            return (x - y) == 1 || (y - x) == 1;
        }

        private CommandResponse MovePlayer(Command command)
        {
            bool isValidMove = false;

            switch (command)
            {
                case Command.MoveUp:
                    isValidMove = this.player[1] != this.mapSize;
                    if (isValidMove) {
                        this.player[1]++;
                    }
                    break;

                case Command.MoveDown:
                    isValidMove = this.player[1] != 0;
                    if (isValidMove) {
                        this.player[1]--;
                    }
                    break;

                case Command.MoveLeft:
                    isValidMove = this.player[0] != 0;
                    if (isValidMove) {
                        this.player[0]--;
                    }
                    break;

                case Command.MoveRight:
                    isValidMove = this.player[0] != this.mapSize;
                    if (isValidMove) {
                        this.player[0]++;
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
                gameOver = true;
                return CommandResponse.ShotHit;
            }

            return CommandResponse.ShotMissed;
        }

        private bool IsPlayerAdjacentToWumpus(Command command)
        {
            switch (command)
            {
                case Command.ShootUp:
                    return player[0] == wumpus[0] && player[1] + 1 == wumpus[1];

                case Command.ShootDown:
                    return player[0] == wumpus[0] && player[1] - 1 == wumpus[1];

                case Command.ShootLeft:
                    return player[1] == wumpus[1] && player[0] - 1 == wumpus[0];

                case Command.ShootRight:
                    return player[1] == wumpus[1] && player[0] + 1 == wumpus[0];
            }

            return false; // Default case, should never be reached
        }

        private bool IsPlayerOn(int[] coords)
        {
            return this.player[0] == coords[0] & this.player[1] == coords[1];
        }
        private int[] SelectUnoccupidCell(List<int[]> occupiedCells)
        {
            Random rnd = new Random();
            bool occupied;
            int[] selectedCell;
            int x;
            int y;

            do {
                x = rnd.Next(this.mapSize);
                y = rnd.Next(this.mapSize);
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