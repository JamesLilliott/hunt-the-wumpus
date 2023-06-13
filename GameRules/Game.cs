class Game 
{
    private String pit;
    private String bats;
    private String wumpus;
    private String player;
    public Boolean gameOver;
    
    public Game()
    {
        this.pit = "1,1";
        this.bats = "1,2";
        this.wumpus = "1,3";
        this.player = "1,4";
        this.gameOver = false;
    }

    public bool processCommand(Command command)
    {
        return true;
    }
}