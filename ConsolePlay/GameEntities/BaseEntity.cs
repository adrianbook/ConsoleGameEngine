using ConsolePlay.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolePlay.GameEntities
{
    public abstract class BaseEntity
    {
        private Gameboard _board;
        public int ContainerHeight { get => _board.Height;  }
        public int ContainerWidth { get => _board.Width; }
        public bool DoRender { get; set; }
        public abstract char RenderAtPosition(Position pos);
        public HashSet<Position> PositionsAsSet { get => Positions.ToHashSet(); }
        public List<Position> Positions { get; set; }
        public BaseEntity(Gameboard board, params Position[]? initialPositions)
        {
            Positions = initialPositions?.ToList() ?? new List<Position>();
            DoRender = true;
            _board= board;
            DoRender = true;
        }
    }

    public enum Direction
    {
        Up, Right, Down, Left
    }
}
