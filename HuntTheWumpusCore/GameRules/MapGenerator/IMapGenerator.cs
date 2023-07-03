namespace HuntTheWumpusCore.GameRules.MapGenerator
{
    public interface IMapGenerator 
    {
        public int getMapSize();
        public int[] getWumpusLocation();
        public int[] getPitLocation();
        public int[] getBatsLocation();
        public int[] getPlayerLocation();
    }
}