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
        
        public int getMapSize()
        {
            return this.mapSize;
        }

        public int[] getBatsLocation()
        {
            return new int[]{this.bats[0], this.bats[1]};
        }

        public int[] getPitLocation()
        {
            return new int[]{this.pit[0], this.pit[1]};
        }

        public int[] getPlayerLocation()
        {
            return new int[]{this.player[0], this.player[1]};
        }

        public int[] getWumpusLocation()
        {
            return new int[]{this.wumpus[0], this.wumpus[1]};
        }
    }
}