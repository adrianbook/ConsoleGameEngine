using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsolePlay.GameEntities;
using ConsolePlay.Rendering;

namespace ConsolePlay
{
    public class Gameboard
    {
        public int Height { get; init; }
        public int Width { get; init; }
        private List<BaseEntity> _entities;
        private HashSet<Position> _occupiedPossitions;
        public ImmutableHashSet<Position> OccupiedPositions { get => _occupiedPossitions.ToImmutableHashSet(); }
        public bool GameOver { get; set; }
        public bool Empty { get => _occupiedPossitions.Count > 0; }

        public BaseEntity GameOverScreen { get; set; }
 
        public Gameboard(int height, int width, string gameOverText)
        {
            Height=height;
            Width=width;
            _entities= new List<BaseEntity>();
            _occupiedPossitions=new HashSet<Position>();
            GameOverScreen = new GameOver(this, gameOverText);
        }

        public bool RegisterEntity(BaseEntity entity, bool failOnCollission = true)
        {
            for (int i = 0; i< entity.Positions.Count; i++)
            {
                if (_occupiedPossitions.Contains(entity.Positions[i]) && failOnCollission)
                {
                    return false;
                }
            }
            _entities.Add(entity);
            return true;
        }

        public bool RemoveEntity(BaseEntity entity)
        {
            for (int i = 0; i< _entities.Count; i++)
            {
                _occupiedPossitions.Remove(entity.Positions[i]);
            }
            return _entities.Remove(entity);
        }

        public HashSet<Position> ResolveFrame()
        {
            _occupiedPossitions= new HashSet<Position>();
            if (GameOver)
            {
                _entities = new List<BaseEntity> { GameOverScreen };
            }
            for (int i = 0; i < _entities.Count; i++)
            {
                var currentEntity = _entities[i];
                for (int j = i+1; j < _entities.Count; j++)
                {
                    var otherEntity = _entities[j];
                    if (currentEntity.PositionsAsSet.Overlaps(otherEntity.PositionsAsSet))
                    {
                        if (currentEntity is MovingEntity currentMoving)
                        {
                            currentMoving.Collide(otherEntity);
                        }
                        if (otherEntity is MovingEntity otherMoving)
                        {
                            otherMoving.Collide(currentEntity);
                        }
                    }
                }
                foreach (var pos in currentEntity.PositionsAsSet)
                {
                    _occupiedPossitions.Add(pos);
                }
            }
            
            return _occupiedPossitions;
        }
    }
}
