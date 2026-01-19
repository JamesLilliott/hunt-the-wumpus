using HuntTheWumpusCore.GameRules.Models;

namespace HuntTheWumpusCore.GameRules.MapGenerator
{
    public class TestMapGenerator : IMapGenerator
    {
        private int mapSize;
        private int[] _pit = new int[2];
        private int[] _bats = new int[2];
        private int[] _wumpus = new int[2];
        private int[] _player = new int[2];
        public TestMapGenerator(int mapSize, int[] pit, int[] bats, int[] wumpus, int[] player)
        {
            this.mapSize = mapSize;
            _player = player;
            _pit = pit;
            _bats = bats;
            _wumpus = wumpus;
        }
        
        public int GetMapSize()
        {
            return mapSize;
        }

        public Position GetBatsLocation()
        {
            return new Position(_bats[0], _bats[1]);
        }

        public Position GetPitLocation()
        {
            return new Position(_pit[0], _pit[1]);
        }

        public Position GetPlayerLocation()
        {
            return new Position(_player[0], _player[1]);
        }

        public Position GetWumpusLocation()
        {
            return new Position(_wumpus[0], _wumpus[1]);
        }
        
        public Map GetMap()
        {
            return new Map(mapSize, GetPitLocation(), GetBatsLocation(), GetWumpusLocation());
        }
    }
}