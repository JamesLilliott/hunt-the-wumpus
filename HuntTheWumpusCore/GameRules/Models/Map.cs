namespace HuntTheWumpusCore.GameRules.Models;

public record Map(int MapSize, Position Pit, Position Bats, Position Wumpus)
{
    public int MapSize = MapSize;
    public Position Pit = Pit;
    public Position Bats = Bats;
    public Position Wumpus = Wumpus;
    
    public Position SelectUnoccupiedPosition(Position _player)
    {
        var rnd = new Random();
        bool occupied;
        Position selectedCell;
        var occupiedPositions = new List<Position> { Pit, Bats, Wumpus, _player };

        do {
            var x = rnd.Next(MapSize);
            var y = rnd.Next(MapSize);
            selectedCell = new Position(x, y);
                
            occupied = false;
            
            occupiedPositions.ForEach(occupiedCell => {
                // Check if selected cell matches any occupied cells
                if (selectedCell.IsOn(occupiedCell))
                {
                    occupied = true;
                }
            });
        } while (occupied);

        return selectedCell;
    }
}