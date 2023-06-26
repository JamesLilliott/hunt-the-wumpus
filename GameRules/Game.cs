class Game 
{
    private int[] pit = new int[2];
    private int[] bats = new int[2];
    private int[] wumpus = new int[2];
    private int[] player = new int[2];
    public Boolean gameOver;
    
    public Game()
    { 
        this.pit[0] = 1; this.pit[1] = 1;
        this.bats[0] = 1; this.bats[1] = 2;
        this.wumpus[0] = 2; this.wumpus[1] = 3;
        this.player[0] = 3; this.player[1] = 3;
        this.gameOver = false;
    }

    public CommandResponse processCommand(Command command)
    {
        if (command.isMove()) {
            CommandResponse commandResponse = this.MovePlayer(command);
            if (commandResponse != CommandResponse.FailedToMove) {
                
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

        Random rnd = new Random();
        int x = rnd.Next(4);
        int y = rnd.Next(4);

        while (this.IsCoordsOccupied(x, y))
        {
            x = rnd.Next(4);
            y = rnd.Next(4);
        }

        this.player[0] = x;
        this.player[0] = y;

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
                isValidMove = player[1] != 4;
                if (isValidMove)
                    player[1]++;
                break;

            case Command.MoveDown:
                isValidMove = player[1] != 1;
                if (isValidMove)
                    player[1]--;
                break;

            case Command.MoveLeft:
                isValidMove = player[0] != 1;
                if (isValidMove)
                    player[0]--;
                break;

            case Command.MoveRight:
                isValidMove = player[0] != 4;
                if (isValidMove)
                    player[0]++;
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

    private bool IsCoordsOccupied(int x, int y) 
    {
        if (this.pit[0] == x & this.pit[1] == y) {
            return true;
        }

        if (this.wumpus[0] == x & this.wumpus[1] == y) {
            return true;
        }

        return false;
    }

}