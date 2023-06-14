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
        this.player[0] = 2; this.player[1] = 2;
        this.gameOver = false;
    }

    public bool processCommand(Command command)
    {
        return true;
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
        if (this.areCoordsNextToEAchOther(this.player[0], coords[0])) {
            return true;
        }

        if (this.areCoordsNextToEAchOther(this.player[1], coords[1])) {
            return true;
        }

        return false;
    }

    private bool areCoordsNextToEAchOther(int x, int y)
    {
        return (x - y) == 1 || (y - x) == 1;
    }
}