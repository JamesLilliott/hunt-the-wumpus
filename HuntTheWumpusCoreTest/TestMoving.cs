using HuntTheWumpusCore.GameRules;
using HuntTheWumpusCore.GameRules.MapGenerator;

namespace HuntTheWumpusCoreTest;

public class TestMoving
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void LoadGameWithDevMap_MoveAround_HitWall()
    {
        // Arrange - Hide all the entities and place the player
        IMapGenerator devMap = new TestMapGenerator(4, new int[]{-1, -1}, new int[]{-1, -1}, new int[]{-1, -1}, new int[]{0, 1});
        Game game = new Game(devMap);

        // Act & Assert  - Move Around
        CommandResponse commandResponse;
        
        commandResponse = game.ProcessCommand(Command.MoveDown);
        Assert.AreEqual(CommandResponse.Moved, commandResponse);
        
        commandResponse = game.ProcessCommand(Command.MoveUp);
        Assert.AreEqual(CommandResponse.Moved, commandResponse);
        
        commandResponse = game.ProcessCommand(Command.MoveRight);
        Assert.AreEqual(CommandResponse.Moved, commandResponse);
        
        commandResponse = game.ProcessCommand(Command.MoveLeft);
        Assert.AreEqual(CommandResponse.Moved, commandResponse);
        
        commandResponse = game.ProcessCommand(Command.MoveLeft);
        Assert.AreEqual(CommandResponse.FailedToMove, commandResponse); // hit the wall        
    }   
}