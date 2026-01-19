namespace HuntTheWumpusCore.GameRules.Models;

public record Position(int X, int Y)
{
    public int X = X;
    public int Y = Y;

    public bool IsOn(Position position) 
        => position.X == X && position.Y == Y;

    public bool IsAbove(Position position) 
        => position.X == X && position.Y + 1 == Y;

    public bool IsBelow(Position position) 
        => position.X == X && position.Y - 1 == Y;

    public bool IsLeftOf(Position position)
        => position.Y == Y && position.X - 1 == X;

    public bool IsRightOf(Position position) 
        => position.Y == Y && position.X + 1 == X;

    public bool IsNextTo(Position position)
    {
        // Do x coords match and y off by one?
        if (position.X == X && IsYAdjacent(position)) {
            return true;
        }

        // Do y coords match and x off by one?
        if (position.Y == Y && IsXAdjacent(position)) {
            return true;
        }

        return false;
    }

    public void Remove()
    {
        X = -1;
        Y = -1;
    }
    
    private bool IsYAdjacent(Position position) 
        => Y - position.Y == 1 || position.Y - Y == 1;

    private bool IsXAdjacent(Position position) 
        => X - position.X == 1 || position.X - X == 1;
}