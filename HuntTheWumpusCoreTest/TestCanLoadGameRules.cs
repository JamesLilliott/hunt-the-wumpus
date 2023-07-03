using HuntTheWumpusCore.GameRules;

namespace HuntTheWumpusCoreTest;

public class TestCanLoadGameRules
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void LoadGame_Passes()
    {
        Game game = new Game();
        game.GetCurrentLocation();
        
        Assert.Pass();
    }
}