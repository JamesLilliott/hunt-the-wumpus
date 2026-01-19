using HuntTheWumpusCore.GameRules.Models;

namespace HuntTheWumpusCore.GameRules.MapGenerator
{
    public interface IMapGenerator 
    {
        public int GetMapSize();

        public Position GetWumpusLocation();

        public Position GetPitLocation();

        public Position GetBatsLocation();

        public Position GetPlayerLocation();
    }
}