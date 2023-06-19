enum Command {
    MoveLeft,
    MoveRight,
    MoveUp,
    MoveDown,
    ShootLeft,
    ShootRight,
    ShootUp,
    ShootDown,
}

static class CommandMethods
{
    public static bool isMove(this Command command)
    {
        switch (command)
        {
            case Command.MoveLeft:
                return true;
            case Command.MoveRight:
                return true;
            case Command.MoveUp:
                return true;
            case Command.MoveDown:
                return true;
            default:
                return false;
        }
        
    }
}
