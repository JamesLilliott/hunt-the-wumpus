using HuntTheWumpusCore.GameRules.Models;

namespace HuntTheWumpusCore.GameRules.MapGenerator
{
    public class MapGenerator : IMapGenerator
    {
        private int _mapSize;

        private Position _pit;

        private Position _bats;

        private Position _wumpus;

        private Position _player;
        
        public MapGenerator(int mapSize)
        {
            PlaceObstaclesAndPlayer();
            _mapSize = mapSize;
        }
        
        public int GetMapSize()
        {
            return _mapSize;
        }

        public Position GetBatsLocation()
        {
            return _bats;
        }

        public Position GetPitLocation()
        {
            return _pit;
        }

        public Position GetPlayerLocation()
        {
            return _player;
        }

        public Position GetWumpusLocation()
        {
            return _wumpus;
        }

        public Map GetMap()
        {
            return new Map(_mapSize, _pit, _bats, _wumpus);
        }

        private void PlaceObstaclesAndPlayer()
        {
            var occupiedPositions = new List<Position>();

            var cell = SelectUnoccupiedPosition(occupiedPositions);
            occupiedPositions.Add(cell);
            _pit = cell;

            cell = SelectUnoccupiedPosition(occupiedPositions);
            occupiedPositions.Add(cell);
            _bats = cell;

            cell = SelectUnoccupiedPosition(occupiedPositions);
            occupiedPositions.Add(cell);
            _wumpus = cell;

            cell = SelectUnoccupiedPosition(occupiedPositions);
            occupiedPositions.Add(cell);
            _player = cell;
        }

        private Position SelectUnoccupiedPosition(List<Position> occupiedPositions)
        {
            var rnd = new Random();
            bool occupied;
            Position selectedCell;

            do {
                var x = rnd.Next(_mapSize);
                var y = rnd.Next(_mapSize);
                selectedCell = new Position(x, y);
                
                occupied = false;
                occupiedPositions.ForEach(occupiedPosition => {
                    if (selectedCell.IsOn(occupiedPosition))
                    {
                        occupied = true;
                    }
                });
            } while (occupied);

            return selectedCell;
        }
    }
}
