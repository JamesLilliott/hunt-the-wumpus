class ConsoleGame
{
    public Game game;
    public ConsoleGame(Game game)
    {
        this.game = game;
    }

    public void run()
    {
        this.welcomePlayer();
        // add instructions here
        String input;
        while (!this.game.gameOver) {
            input = this.inputCommand();
            Command command = this.convertInputToCommand(input);
            CommandResponse commandResponse = game.processCommand(command);
            
            if (commandResponse is CommandResponse.FailedToMove) {
                Console.WriteLine("Unable to move that way");
            }

            if (commandResponse is CommandResponse.Moved) {
                Console.WriteLine("You moved to the next cavern");
            }

            if (commandResponse is CommandResponse.ShotHit) {
                Console.WriteLine("You hit the Wumpus!");
            }

            if (commandResponse is CommandResponse.ShotMissed) {
                Console.WriteLine("You missed the Wumpus!");
            }

            this.renderLocation();
        }
    }

    private void welcomePlayer()
    {
        Console.WriteLine("");
        Console.WriteLine("////////////////"); Thread.Sleep(1000);
        Console.WriteLine("//  WELCOME   //"); Thread.Sleep(1000);
        Console.WriteLine("//    TO      //"); Thread.Sleep(1000);
        Console.WriteLine("//   HUNT     //"); Thread.Sleep(500);
        Console.WriteLine("//    THE     //"); Thread.Sleep(500);
        Console.WriteLine("//   WUMPUS   //"); Thread.Sleep(500);
        Console.WriteLine("////////////////"); Thread.Sleep(1000);
        Console.WriteLine("");
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
        CurrentLocation currentLocation = this.game.GetCurrentLocation();
        if (currentLocation.batDroppings) {
            Console.WriteLine("Bat Droppings");
        }

        if (currentLocation.badOdur) {
            Console.WriteLine("Bad odur");
        }

        if (currentLocation.gustsOfWind) {
            Console.WriteLine("Gust of wind");
        }
    }
}
