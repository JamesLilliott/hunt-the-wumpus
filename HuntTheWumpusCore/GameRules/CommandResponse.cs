namespace HuntTheWumpusCore.GameRules
{
    enum CommandResponse {
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
