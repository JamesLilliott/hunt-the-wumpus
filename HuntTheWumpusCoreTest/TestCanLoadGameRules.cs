using HuntTheWumpusCore.GameRules;
using HuntTheWumpusCore.GameRules.MapGenerator;

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
        MapGenerator mapGenerator = new MapGenerator(4);
    
        Game game = new Game(mapGenerator);
        game.GetCurrentLocation();
        
        Assert.Pass();
    }
}