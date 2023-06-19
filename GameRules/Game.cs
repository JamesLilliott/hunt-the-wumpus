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

    public bool processCommand(Command command)
    {
        if (command.isMove()) {
            return this.movePlayer(command);
        }

        if (command.isShoot()) {
            return this.shoot(command);
        }

        return false;
        
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

    private bool movePlayer(Command command)
    {
        switch (command) {
            case Command.MoveUp:
                if (this.player[1] == 4) {
                    return false;
                }
                this.player[1]++;
            break;

            case Command.MoveDown:
                if (this.player[1] == 1) {
                    return false;
                }
                this.player[1]--;
            break;

            case Command.MoveLeft:
                if (this.player[0] == 1) {
                    return false;
                }
                this.player[0]--;
            break;

            case Command.MoveRight:
                if (this.player[0] == 4) {
                    return false;
                }
                this.player[0]++;
            break;

        }
        return true;
    }

    private bool shoot(Command command)
    {
         switch (command) {
            case Command.ShootUp:
                if (this.player[0] == this.wumpus[0] && this.player[1] + 1 == this.wumpus[1]) {
                    this.gameOver = true;
                }
                break;
            case Command.ShootDown:
                if (this.player[0] == this.wumpus[0] && this.player[1] - 1 == this.wumpus[1]) {
                    this.gameOver = true;
                }
                break;
            case Command.ShootLeft:
                if (this.player[1] == this.wumpus[1] && this.player[0] - 1 == this.wumpus[0]) {
                    this.gameOver = true;
                }
                break;
            case Command.ShootRight:
                if (this.player[1] == this.wumpus[1] && this.player[0] + 1 == this.wumpus[0]) {
                    this.gameOver = true;
                }
                break;

        }
        return true;
    }
}