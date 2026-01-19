using HuntTheWumpusCore.GameRules.MapGenerator;
using HuntTheWumpusCore.GameRules.Models;

namespace HuntTheWumpusCore.GameRules
{
    public class Game(IMapGenerator mapGenerator)
    {
        public bool GameOver;
        private int _mapSize = mapGenerator.GetMapSize();

        private Map _map = mapGenerator.GetMap();
        private Position _player = mapGenerator.GetPlayerLocation();

        public CommandResponse ProcessCommand(Command command)
        {
            if (command.IsMove()) {
                var commandResponse = MovePlayer(command);
                    
                if (IsPlayerOn(_map.Wumpus)) {
                    GameOver = true;
                    return CommandResponse.AteByWumpus;
                }

                if (IsPlayerOn(_map.Bats)) {
                    return BatsMovePlayer();
                }

                if (IsPlayerOn(_map.Pit)) {
                    GameOver = true;
                    return CommandResponse.FellInPit;
                }

                return commandResponse;
            }

            if (command.IsShoot()) {
                return Shoot(command);
            }

            return CommandResponse.InvalidCommand;
            
        }

        private CommandResponse BatsMovePlayer()
        {
            _map.Bats.Remove();
            _player = _map.SelectUnoccupiedPosition(_player);

            return CommandResponse.MovedByBats;
        }

        public CurrentLocation GetCurrentLocation()
        {
            return new CurrentLocation(
                IsPlayerNextTo(_map.Bats),
                IsPlayerNextTo(_map.Wumpus),
                IsPlayerNextTo(_map.Pit)
            );
        }

        private bool IsPlayerNextTo(Position coords) 
            => _player.IsNextTo(coords);

        private CommandResponse MovePlayer(Command command)
        {
            var isValidMove = false;

            switch (command)
            {
                case Command.MoveUp:
                    isValidMove = _player.Y != _mapSize;
                    if (isValidMove) {
                        _player.Y++;
                    }
                    break;

                case Command.MoveDown:
                    isValidMove = _player.Y != 0;
                    if (isValidMove) {
                        _player.Y--;
                    }
                    break;

                case Command.MoveLeft:
                    isValidMove = _player.X != 0;
                    if (isValidMove) {
                        _player.X--;
                    }
                    break;

                case Command.MoveRight:
                    isValidMove = _player.X != _mapSize;
                    if (isValidMove) {
                        _player.X++;
                    }
                    break;
            }

            return isValidMove ? CommandResponse.Moved : CommandResponse.FailedToMove;
        }

        private CommandResponse Shoot(Command command)
        {
            var isShotHit = DidShotHit(command);

            if (isShotHit)  
            {
                GameOver = true;
                return CommandResponse.ShotHit;
            }

            return CommandResponse.ShotMissed;
        }

        private bool DidShotHit(Command command)
        {
            switch (command)
            {
                case Command.ShootUp:
                    return _map.Wumpus.IsAbove(_player);

                case Command.ShootDown:
                    return _map.Wumpus.IsBelow(_player);

                case Command.ShootLeft:
                    return _map.Wumpus.IsLeftOf(_player);

                case Command.ShootRight:
                    return _map.Wumpus.IsRightOf(_player);
            }

            return false; // Default case, should never be reached
        }

        private bool IsPlayerOn(Position coords) => _player.IsOn(coords);
    }
}