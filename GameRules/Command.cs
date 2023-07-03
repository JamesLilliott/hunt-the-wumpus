namespace GameRules
{
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
            if (command is Command.MoveLeft or Command.MoveRight or Command.MoveUp or Command.MoveDown) {
                return true;
            }        
            return false;
        }

        public static bool isShoot(this Command command)
        {
            if (command is Command.ShootLeft or Command.ShootRight or Command.ShootUp or Command.ShootDown) {
                return true;
            }        

            return false;
        }
    }
}