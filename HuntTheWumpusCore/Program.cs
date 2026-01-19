using HuntTheWumpusCore.DevConsole;
using HuntTheWumpusCore.GameRules;
using HuntTheWumpusCore.GameRules.MapGenerator;

Console.WriteLine("Booting Game");

MapGenerator mapGenerator = new MapGenerator(4);
Game game = new Game(mapGenerator);

DevConsole consoleGame = new DevConsole(game);

consoleGame.Run();