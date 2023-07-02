/**
* A dev console to test changes made to GameRules
*/
class DevConsole
{
    public Game game;
    public DevConsole(Game game)
    {
        this.game = game;
    }

    public void run()
    {
        this.renderLocation();

        String input;
        while (!this.game.gameOver) {
            input = this.inputCommand();
            Command command = this.convertInputToCommand(input);
            CommandResponse commandResponse = game.processCommand(command);
            Console.WriteLine("commandResponse: " + commandResponse);

            this.renderLocation();
        }
    }

    private string inputCommand() 
    {
        Boolean validInput = false;
        String[] validInputs = {"move up", "move down", "move left", "move right", "shoot up", "shoot down", "shoot right", "shoot left"};
        String? input = "";

        while (!validInput) {
            Console.WriteLine("Please input a command: ");
            input = Console.ReadLine();
            
            if (input is null) {
                Console.WriteLine("NULL received");
                continue;
            }

            input = input.ToLower().Trim();
            if (!validInputs.Contains(input)) {
                Console.WriteLine("Invalid command");
                continue;
            }
            
            validInput = true;
        }

        if (input is null) {
            throw new Exception("Input is null");
        }

        return input;
    }

    private Command convertInputToCommand(string input) => input switch {
        "move left" => Command.MoveLeft,
        "move right" => Command.MoveRight,
        "move up" => Command.MoveUp,
        "move down" => Command.MoveDown,
        "shoot left" => Command.ShootLeft,
        "shoot right" => Command.ShootRight,
        "shoot up" => Command.ShootUp,
        "shoot down" => Command.ShootDown,
        _ => throw new Exception("Invalid Command"),
    };

    private void renderLocation()
    {
        if (this.game.gameOver) {
            Console.WriteLine("Game Over!");
            return;
        }
        
        CurrentLocation currentLocation = this.game.GetCurrentLocation();
        if (currentLocation.batDroppings) {
            Console.WriteLine("Next to bats");
        }

        if (currentLocation.badOdur) {
            Console.WriteLine("Next to wumpus");
        }

        if (currentLocation.gustsOfWind) {
            Console.WriteLine("Next to pit");
        }
    }
}
