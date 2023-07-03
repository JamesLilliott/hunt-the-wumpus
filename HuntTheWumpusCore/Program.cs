using HuntTheWumpusCore.Dev;
using HuntTheWumpusCore.GameRules;
using HuntTheWumpusCore.GameRules.MapGenerator;

Console.WriteLine("Booting Game");

MapGenerator mapGenerator = new MapGenerator(4);

// Build Game (Probs should be a factory)
Game game = new Game(mapGenerator);

// Build Interface
DevConsole consoleGame = new DevConsole(game);

// Run the Game
consoleGame.run();