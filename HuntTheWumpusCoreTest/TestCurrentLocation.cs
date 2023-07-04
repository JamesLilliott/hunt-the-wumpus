using HuntTheWumpusCore.GameRules;
using HuntTheWumpusCore.GameRules.MapGenerator;

namespace HuntTheWumpusCoreTest;

public class TestCurrentLocation
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void LoadGameWithDevMap__MoveNextToBat_SeePoo()
    {
        // Arrange - Arrange the player 2 cells from the bats
        IMapGenerator devMap = new TestMapGenerator(4, new int[]{-1, -1}, new int[]{1, 1}, new int[]{-1, -1}, new int[]{1, 3});
        Game game = new Game(devMap);

        // Act - Move the player next the bats
        CommandResponse commandResponse = game.processCommand(Command.MoveDown);
        CurrentLocation currentLocation = game.GetCurrentLocation();

        // Assert the bats are nearby
        Assert.AreEqual(CommandResponse.Moved, commandResponse);
        Assert.IsTrue(currentLocation.batDroppings);
        Assert.IsFalse(currentLocation.badOdur);
        Assert.IsFalse(currentLocation.gustsOfWind);
    }

    [Test]
    public void LoadGameWithDevMap__MoveNextToPit_FeelWind()
    {
        // Arrange - Arrange the player 2 cells from the bats
        IMapGenerator devMap = new TestMapGenerator(4, new int[]{1, 1}, new int[]{-1, -1}, new int[]{-1, -1}, new int[]{1, 3});
        Game game = new Game(devMap);

        // Act - Move the player next the bats
        CommandResponse commandResponse = game.processCommand(Command.MoveDown);
        CurrentLocation currentLocation = game.GetCurrentLocation();

        // Assert the bats are nearby
        Assert.AreEqual(CommandResponse.Moved, commandResponse);
        Assert.IsTrue(currentLocation.gustsOfWind);
        Assert.IsFalse(currentLocation.badOdur);
        Assert.IsFalse(currentLocation.batDroppings);
    }

    [Test]
    public void LoadGameWithDevMap__MoveNextToWumpus_SmellBadOdur()
    {
        // Arrange - Arrange the player 2 cells from the bats
        IMapGenerator devMap = new TestMapGenerator(4, new int[]{-1, -1}, new int[]{-1, -1}, new int[]{1, 1}, new int[]{1, 3});
        Game game = new Game(devMap);

        // Act - Move the player next the bats
        CommandResponse commandResponse = game.processCommand(Command.MoveDown);
        CurrentLocation currentLocation = game.GetCurrentLocation();

        // Assert the bats are nearby
        Assert.AreEqual(CommandResponse.Moved, commandResponse);
        Assert.IsTrue(currentLocation.badOdur);
        Assert.IsFalse(currentLocation.gustsOfWind);
        Assert.IsFalse(currentLocation.batDroppings);
    }

    [Test]
    public void LoadGameWithDevMap__MoveNextToAll_SenceAll()
    {
        // Arrange - Arrange the player 2 cells from the bats
        IMapGenerator devMap = new TestMapGenerator(4, new int[]{2, 1}, new int[]{1, 2}, new int[]{2, 3}, new int[]{3, 2});
        Game game = new Game(devMap);

        // Act - Move the player next the bats
        CommandResponse commandResponse = game.processCommand(Command.MoveLeft);
        CurrentLocation currentLocation = game.GetCurrentLocation();

        // Assert the bats are nearby
        Assert.AreEqual(CommandResponse.Moved, commandResponse);
        Assert.IsTrue(currentLocation.badOdur);
        Assert.IsTrue(currentLocation.gustsOfWind);
        Assert.IsTrue(currentLocation.batDroppings);
    }
    
}