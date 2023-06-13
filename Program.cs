
Console.WriteLine("Booting Game");

// Build Game (Probs should be a factory)
Game game = new Game();

// Build Interface
ConsoleGame consoleGame = new ConsoleGame(game);

// Run the Game
consoleGame.run();