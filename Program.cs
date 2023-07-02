
Console.WriteLine("Booting Game");

// Build Game (Probs should be a factory)
Game game = new Game();

// Build Interface
DevConsole consoleGame = new DevConsole(game);

// Run the Game
consoleGame.run();