namespace HuntTheWumpusCore.GameRules
{
    public enum CommandResponse {
        FailedToMove,
        Moved,
        MovedByBats,
        AteByWumpus,
        FellInPit,
        ShotMissed,
        ShotHit,
        InvalidCommand,
    }
}
