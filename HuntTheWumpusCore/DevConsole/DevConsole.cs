using HuntTheWumpusCore.GameRules;

namespace HuntTheWumpusCore.DevConsole
{
    class DevConsole(Game game)
    {
        public void Run()
        {
            RenderLocation();

            while (!game.GameOver) {
                var input = InputCommand();
                var command = ConvertInputToCommand(input);
                var commandResponse = game.ProcessCommand(command);

                DescribeResponse(commandResponse);
                RenderLocation();
            }
        }

        private void DescribeResponse(CommandResponse commandResponse)
        {
            switch (commandResponse)
            {
                case CommandResponse.FailedToMove:
                    Console.WriteLine("Unable to move that way");
                    break;
                case CommandResponse.Moved:
                    Console.WriteLine("You moved to the next cavern");
                    break;
                case CommandResponse.MovedByBats:
                    Console.WriteLine("You wandered into a bat nest and got transported to a different cavern!");
                    break;
                case CommandResponse.AteByWumpus:
                    Console.WriteLine("You wandered into the Wumpus nest and got eaten!");
                    break;
                case CommandResponse.FellInPit:
                    Console.WriteLine("You wandered into a pit and fell to your death!");
                    break;
                case CommandResponse.ShotMissed:
                    Console.WriteLine("You missed the Wumpus!");
                    break;
                case CommandResponse.ShotHit:
                    Console.WriteLine("You hit the Wumpus!");
                    break;
                case CommandResponse.InvalidCommand:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private string InputCommand() 
        {
            var validInput = false;
            string[] validInputs = ["move up", "move down", "move left", "move right", "shoot up", "shoot down", "shoot right", "shoot left"];
            var input = "";

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

        private Command ConvertInputToCommand(string input) => input switch {
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

        private void RenderLocation()
        {
            if (game.GameOver) {
                Console.WriteLine("Game Over!");
                return;
            }
            
            var currentLocation = game.GetCurrentLocation();
            if (currentLocation.BatDroppings) {
                Console.WriteLine("Next to bats");
            }

            if (currentLocation.BadOdour) {
                Console.WriteLine("Next to wumpus");
            }

            if (currentLocation.GustsOfWind) {
                Console.WriteLine("Next to pit");
            }
        }
    }
}