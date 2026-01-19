namespace HuntTheWumpusCore.GameRules
{
    public enum Command {
        MoveLeft,
        MoveRight,
        MoveUp,
        MoveDown,
        ShootLeft,
        ShootRight,
        ShootUp,
        ShootDown,
    }

    internal static class CommandMethods
    {
        public static bool IsMove(this Command command) 
            => command is Command.MoveLeft or Command.MoveRight or Command.MoveUp or Command.MoveDown;

        public static bool IsShoot(this Command command)
            => command is Command.ShootLeft or Command.ShootRight or Command.ShootUp or Command.ShootDown;
    }
}