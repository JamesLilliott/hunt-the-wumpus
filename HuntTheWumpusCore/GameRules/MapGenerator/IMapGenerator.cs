namespace HuntTheWumpusCore.GameRules.MapGenerator
{
    public interface IMapGenerator 
    {
        public int GetMapSize();

        public int[] GetWumpusLocation();

        public int[] GetPitLocation();

        public int[] GetBatsLocation();

        public int[] GetPlayerLocation();
    }
}