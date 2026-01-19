using HuntTheWumpusCore.GameRules.Models;

namespace HuntTheWumpusCore.GameRules.MapGenerator
{
    public class TestMapGenerator : IMapGenerator
    {
        private int mapSize;
        private int[] pit = new int[2];
        private int[] bats = new int[2];
        private int[] wumpus = new int[2];
        private int[] player = new int[2];
        public TestMapGenerator(int mapSize, int[] pit, int[] bats, int[] wumpus, int[] player)
        {
            this.mapSize = mapSize;
            this.player = player;
            this.pit = pit;
            this.bats = bats;
            this.wumpus = wumpus;
        }
        
        public int GetMapSize()
        {
            return mapSize;
        }

        public Position GetBatsLocation()
        {
            return new Position(bats[0], bats[1]);
        }

        public Position GetPitLocation()
        {
            return new Position(pit[0], pit[1]);
        }

        public Position GetPlayerLocation()
        {
            return new Position(player[0], player[1]);
        }

        public Position GetWumpusLocation()
        {
            return new Position(wumpus[0], wumpus[1]);
        }
    }
}