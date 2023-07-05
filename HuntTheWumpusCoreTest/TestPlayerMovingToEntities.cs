using HuntTheWumpusCore.GameRules;
using HuntTheWumpusCore.GameRules.MapGenerator;

namespace HuntTheWumpusCoreTest;

public class TestPlayerMovingToEntities
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void LoadGame__CallGame_Passes()
    {
        MapGenerator mapGenerator = new MapGenerator(4);
    
        Game game = new Game(mapGenerator);
        game.GetCurrentLocation();
        
        Assert.Pass();
    }

    [Test]
    public void LoadGameWithDevMap_MovePlayerToWumpus_GameOver()
    {
        // Arrange - Arrange the player next to the wumpus
        IMapGenerator devMap = new TestMapGenerator(4, new int[]{1, 1}, new int[]{1, 2}, new int[]{1, 3}, new int[]{2,3});
        Game game = new Game(devMap);

        // Act - Move the player into the wumpus
        CommandResponse commandResponse = game.processCommand(Command.MoveLeft);

        // Assert the game is over
        Assert.AreEqual(CommandResponse.AteByWumpus, commandResponse);
        Assert.AreEqual(true, game.GameOver);
    }

    [Test]
    public void LoadGameWithDevMap_MovePlayerToPit_GameOver()
    {
        // Arrange - Arrange the player next to the wumpus
        IMapGenerator devMap = new TestMapGenerator(4, new int[]{1, 1}, new int[]{1, 2}, new int[]{1, 3}, new int[]{2,1});
        Game game = new Game(devMap);

        // Act - Move the player into the pit
        CommandResponse commandResponse = game.processCommand(Command.MoveLeft);

        // Assert the game is over
        Assert.AreEqual(CommandResponse.FellInPit, commandResponse);
        Assert.AreEqual(true, game.GameOver);
    }

    [Test]
    public void LoadGameWithDevMap_MovePlayerToBat_PlayerMoved()
    {
        // Arrange - Arrange the player next to the wumpus
        IMapGenerator devMap = new TestMapGenerator(4, new int[]{1, 1}, new int[]{1, 2}, new int[]{1, 3}, new int[]{2,2});
        Game game = new Game(devMap);

        // Act - Move the player into the bats
        CommandResponse commandResponse = game.processCommand(Command.MoveLeft);

        // Assert the game is over
        Assert.AreEqual(CommandResponse.MovedByBats, commandResponse);
    }
}