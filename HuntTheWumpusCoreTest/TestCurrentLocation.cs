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
        CommandResponse commandResponse = game.ProcessCommand(Command.MoveDown);
        CurrentLocation currentLocation = game.GetCurrentLocation();

        // Assert the bats are nearby
        Assert.That(commandResponse, Is.EqualTo(CommandResponse.Moved));
        Assert.That(currentLocation.BatDroppings, Is.True);
        Assert.That(currentLocation.BadOdour, Is.False);
        Assert.That(currentLocation.GustsOfWind, Is.False);
    }

    [Test]
    public void LoadGameWithDevMap__MoveNextToPit_FeelWind()
    {
        // Arrange - Arrange the player 2 cells from the bats
        IMapGenerator devMap = new TestMapGenerator(4, new int[]{1, 1}, new int[]{-1, -1}, new int[]{-1, -1}, new int[]{1, 3});
        Game game = new Game(devMap);

        // Act - Move the player next the bats
        CommandResponse commandResponse = game.ProcessCommand(Command.MoveDown);
        CurrentLocation currentLocation = game.GetCurrentLocation();

        // Assert the pit are nearby
        Assert.That(commandResponse, Is.EqualTo(CommandResponse.Moved));
        Assert.That(currentLocation.GustsOfWind, Is.True);
        Assert.That(currentLocation.BadOdour, Is.False);
        Assert.That(currentLocation.BatDroppings, Is.False);
    }

    [Test]
    public void LoadGameWithDevMap__MoveNextToWumpus_SmellBadOdur()
    {
        // Arrange - Arrange the player 2 cells from the bats
        IMapGenerator devMap = new TestMapGenerator(4, new int[]{-1, -1}, new int[]{-1, -1}, new int[]{1, 1}, new int[]{1, 3});
        Game game = new Game(devMap);

        // Act - Move the player next the bats
        CommandResponse commandResponse = game.ProcessCommand(Command.MoveDown);
        CurrentLocation currentLocation = game.GetCurrentLocation();

        // Assert the wumpus are nearby
        Assert.That(commandResponse, Is.EqualTo(CommandResponse.Moved));
        Assert.That(currentLocation.BadOdour, Is.True);
        Assert.That(currentLocation.GustsOfWind, Is.False);
        Assert.That(currentLocation.BatDroppings, Is.False);
    }

    [Test]
    public void LoadGameWithDevMap__MoveNextToAll_SenceAll()
    {
        // Arrange - Arrange the player 2 cells from the bats
        IMapGenerator devMap = new TestMapGenerator(4, new int[]{2, 1}, new int[]{1, 2}, new int[]{2, 3}, new int[]{3, 2});
        Game game = new Game(devMap);

        // Act - Move the player next the bats
        CommandResponse commandResponse = game.ProcessCommand(Command.MoveLeft);
        CurrentLocation currentLocation = game.GetCurrentLocation();

        // Assert the bats, wumpus and pit are nearby
        Assert.That(commandResponse, Is.EqualTo(CommandResponse.Moved));
        Assert.That(currentLocation.BadOdour, Is.True);
        Assert.That(currentLocation.GustsOfWind, Is.True);
        Assert.That(currentLocation.BatDroppings, Is.True);
    }
    
}