using HuntTheWumpusCore.GameRules;
using HuntTheWumpusCore.GameRules.MapGenerator;

namespace HuntTheWumpusCoreTest;

public class TestShooting
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void LoadGameWithDevMap_ShootLeft_HitWumpus()
    {
        // Arrange - Place player next to wumpus
        IMapGenerator devMap = new TestMapGenerator(4, new int[]{-1, -1}, new int[]{-1, -1}, new int[]{1, 1}, new int[]{2, 1});
        Game game = new Game(devMap);

        // Act - Shoot left
        CommandResponse commandResponse = game.ProcessCommand(Command.ShootLeft);

        // Assert - Hit
        Assert.That(commandResponse, Is.EqualTo(CommandResponse.ShotHit));
        Assert.That(game.GameOver, Is.EqualTo(true));
    }

    [Test]
    public void LoadGameWithDevMap_ShootRight_HitWumpus()
    {
        // Arrange - Place player next to wumpus
        IMapGenerator devMap = new TestMapGenerator(4, new int[]{-1, -1}, new int[]{-1, -1}, new int[]{1, 1}, new int[]{0, 1});
        Game game = new Game(devMap);

        // Act - Shoot right
        CommandResponse commandResponse = game.ProcessCommand(Command.ShootRight);

        // Assert - Hit
        Assert.That(commandResponse, Is.EqualTo(CommandResponse.ShotHit));
        Assert.That(game.GameOver, Is.EqualTo(true));
    } 

    [Test]
    public void LoadGameWithDevMap_ShootUp_HitWumpus()
    {
        // Arrange - Place player next to wumpus
        IMapGenerator devMap = new TestMapGenerator(4, new int[]{-1, -1}, new int[]{-1, -1}, new int[]{1, 1}, new int[]{1, 0});
        Game game = new Game(devMap);

        // Act - Shoot up
        CommandResponse commandResponse = game.ProcessCommand(Command.ShootUp);

        // Assert - Hit
        Assert.That(commandResponse, Is.EqualTo(CommandResponse.ShotHit));
        Assert.That(game.GameOver, Is.EqualTo(true));
    }   

    [Test]
    public void LoadGameWithDevMap_ShootDown_HitWumpus()
    {
        // Arrange - Place player next to wumpus
        IMapGenerator devMap = new TestMapGenerator(4, new int[]{-1, -1}, new int[]{-1, -1}, new int[]{1, 1}, new int[]{1, 2});
        Game game = new Game(devMap);

        // Act - Shoot down
        CommandResponse commandResponse = game.ProcessCommand(Command.ShootDown);

        // Assert - Hit
        Assert.That(commandResponse, Is.EqualTo(CommandResponse.ShotHit));
        Assert.That(game.GameOver, Is.EqualTo(true));
    } 

    [Test]
    public void LoadGameWithDevMap_Shoot_MissWumpus()
    {
        // Arrange - Place player next to wumpus
        IMapGenerator devMap = new TestMapGenerator(4, new int[]{-1, -1}, new int[]{-1, -1}, new int[]{1, 1}, new int[]{4, 4});
        Game game = new Game(devMap);

        // Act - Shoot down
        CommandResponse commandResponse = game.ProcessCommand(Command.ShootDown);

        // Assert - Hit
        Assert.That(commandResponse, Is.EqualTo(CommandResponse.ShotMissed));
        Assert.That(game.GameOver, Is.EqualTo(false));
    }   
}