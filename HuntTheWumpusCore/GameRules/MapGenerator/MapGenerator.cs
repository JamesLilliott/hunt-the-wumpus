namespace HuntTheWumpusCore.GameRules.MapGenerator
{
    public class MapGenerator : IMapGenerator
    {

        private int mapSize;
        private int[] pit = new int[2];
        private int[] bats = new int[2];
        private int[] wumpus = new int[2];
        private int[] player = new int[2];
        public MapGenerator(int mapSize)
        {
            this.mapSize = mapSize;
            this.PlaceObsticlesAndPlayer();
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

        private void PlaceObsticlesAndPlayer()
        {
            List<int[]> occupiedCells = new List<int[]>();
            int[] cell;
            
            cell = this.SelectUnoccupidCell(occupiedCells);
            occupiedCells.Add(cell);
            this.pit[0] = cell[0]; this.pit[1] = cell[1];

            cell = this.SelectUnoccupidCell(occupiedCells);
            occupiedCells.Add(cell);
            this.bats[0] = cell[0]; this.bats[1] = cell[1];

            cell = this.SelectUnoccupidCell(occupiedCells);
            occupiedCells.Add(cell);
            this.wumpus[0] = cell[0]; this.wumpus[1] = cell[1];

            cell = this.SelectUnoccupidCell(occupiedCells);
            occupiedCells.Add(cell);
            this.player[0] = cell[0]; this.player[1] = cell[1];
        }

        private int[] SelectUnoccupidCell(List<int[]> occupiedCells)
        {
            Random rnd = new Random();
            bool occupied;
            int[] selectedCell;
            int x;
            int y;

            do {
                x = rnd.Next(this.mapSize);
                y = rnd.Next(this.mapSize);
                selectedCell = new int[2]{x, y};
                
                occupied = false;
                occupiedCells.ForEach((occupiedCell) => {
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
