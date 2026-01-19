namespace HuntTheWumpusCore.GameRules
{
    public record CurrentLocation(bool BatDroppings, bool BadOdour, bool GustsOfWind)
    {
        public bool BatDroppings = BatDroppings;
        public bool BadOdour = BadOdour;
        public bool GustsOfWind = GustsOfWind;
    }
}
