
using HuntTheWumpusCore.GameRules;
/**
* A dev console to test changes made to GameRules
*/
namespace HuntTheWumpusCore.Dev
{
    class DevConsole
    {
        public Game game;
        public DevConsole(Game game)
        {
            this.game = game;
        }

        public void run()
        {
            this.RenderLocation();

            String input;
            while (!this.game.GameOver) {
                input = InputCommand();
                Command command = this.ConvertInputToCommand(input);
                CommandResponse commandResponse = game.processCommand(command);

                this.DescribeResponse(commandResponse);
                this.RenderLocation();
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
            if (this.game.GameOver) {
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
}