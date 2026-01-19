using HuntTheWumpusCore.GameRules.Models;

namespace HuntTheWumpusCore.GameRules.MapGenerator
{
    public class MapGenerator : IMapGenerator
    {
        private int _mapSize;

        private int[] _pit = new int[2];

        private int[] _bats = new int[2];

        private int[] _wumpus = new int[2];

        private int[] _player = new int[2];
        
        public MapGenerator(int mapSize)
        {
            _mapSize = mapSize;
            PlaceObstaclesAndPlayer();
        }
        
        public int GetMapSize()
        {
            return _mapSize;
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

        private void PlaceObstaclesAndPlayer()
        {
            var occupiedCells = new List<int[]>();

            var cell = SelectUnoccupiedCell(occupiedCells);
            occupiedCells.Add(cell);
            _pit[0] = cell[0]; _pit[1] = cell[1];

            cell = SelectUnoccupiedCell(occupiedCells);
            occupiedCells.Add(cell);
            _bats[0] = cell[0]; _bats[1] = cell[1];

            cell = SelectUnoccupiedCell(occupiedCells);
            occupiedCells.Add(cell);
            _wumpus[0] = cell[0]; _wumpus[1] = cell[1];

            cell = SelectUnoccupiedCell(occupiedCells);
            occupiedCells.Add(cell);
            _player[0] = cell[0]; _player[1] = cell[1];
        }

        private int[] SelectUnoccupiedCell(List<int[]> occupiedCells)
        {
            var rnd = new Random();
            bool occupied;
            int[] selectedCell;

            do {
                var x = rnd.Next(_mapSize);
                var y = rnd.Next(_mapSize);
                selectedCell = [x, y];
                
                occupied = false;
                occupiedCells.ForEach(occupiedCell => {
                    // Check if selected cell matches any occupied cells
                    if (occupiedCell[0] == selectedCell[0] && occupiedCell[1] == selectedCell[1]) {
                        occupied = true;
                    }
                });
            } while (occupied);

            return selectedCell;
        }
    }
}
