using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsolePlay.GameEntities;
using ConsolePlay.Rendering;

namespace ConsolePlay
{
    internal class Snake : PlayerControlledEntity
    {
        public Direction Direction { get; private set; }
        public Position Head { get => Positions.Last(); }
        private Gameboard _board;
        public bool Munch { get; private set; }
        public Snake(Gameboard board): base(board)
        {
            _board = board;
            Munch = false;
            Positions = new List<Position>
            {
                new Position
                {
                    Top= ContainerHeight/2,
                    Left= (ContainerWidth/2)-3,
                    Self= this
                },
                new Position    
                {
                    Top= ContainerHeight/2,
                    Left= (ContainerWidth/2) -2,
                    Self= this
                },
                new Position
                {
                    Top= ContainerHeight/2,
                    Left=(ContainerWidth / 2)-1,
                    Self= this
                },
                new Position
                {
                    Top= ContainerHeight/2,
                    Left= ContainerWidth/2,
                    Self= this
                }
            };
            Direction = Direction.Right;
        }

        public override void HandleInput(ConsoleKeyInfo key) 
        {

            var neck = Positions.ElementAt(Positions.Count-2);
            var head = Positions.ElementAt(Positions.Count-1);

            Direction= (key.Key, head, neck) switch
            {
                (ConsoleKey.LeftArrow, { Left: var hl }, { Left: var nl }) when (hl == 0 && nl == ContainerWidth-1) || (hl-1 == nl) => Direction,
                (ConsoleKey.LeftArrow,_,_) => Direction.Left,
                (ConsoleKey.RightArrow, { Left: var hl }, { Left: var nl }) when (hl == ContainerWidth-1 && nl == 0) || (hl+1 == nl) => Direction,
                (ConsoleKey.RightArrow,_,_) => Direction.Right,
                (ConsoleKey.UpArrow, { Top: var ht}, { Top: var nt}) when (ht == 0 && nt == ContainerHeight-1) || (ht-1 == nt) => Direction,
                (ConsoleKey.UpArrow,_,_) => Direction.Up,
                (ConsoleKey.DownArrow, { Top: var ht }, { Top: var nt }) when (ht == ContainerHeight-1 && nt == 0) || (ht+1 == nt) => Direction,
                (ConsoleKey.DownArrow,_,_) => Direction.Down,
                _ => Direction
            };
        }

        public void Die()
        {
            Alive = false;
            _board.GameOver = true;
        }

        public override void Move()
        {
            var oldHead = Positions.Last();
            var newHead = (Direction, oldHead) switch
            {
                (Direction.Right, { Left: var l, Top: var t }) when l == ContainerWidth - 1 => new Position(0, t, this),
                (Direction.Right, { Left: var l, Top: var t }) => new Position(l+1, t, this),
                (Direction.Left, { Left: var l, Top: var t }) when l == 0 => new Position(ContainerWidth - 1, t, this),
                (Direction.Left, { Left: var l, Top: var t }) => new Position(l-1, t, this),
                (Direction.Up, { Left: var l, Top: var t }) when t == 0 => new Position(l, ContainerHeight-1, this),
                (Direction.Up, { Left: var l, Top: var t }) => new Position(l, t - 1, this),
                (Direction.Down, { Left: var l, Top: var t }) when t == ContainerHeight - 1 => new Position(l, 0, this),
                (Direction.Down, { Left: var l, Top: var t }) => new Position(l, t + 1, this),
                _ => throw new Exception("Growth pains")
            };
            if (Positions.Contains(newHead))
            {
                Die();
            }
            Positions.Add(newHead);
            if (!Munch)
            {
                Positions.RemoveAt(Positions.Count-1);
            } else
            {
                Munch = false;
            }
        }

        public override bool Collide(BaseEntity other)
        {
            if (this == other)
            {
                Die();
                return true;
            }

            if(other is Treat treat)
            {
                switch (treat.State)
                {
                    case Is.Fresh:
                        Munch = true;
                        treat.State = Is.Eaten;
                        return true;
                    case Is.Stale:
                        Die();
                        return true;
                    default:
                        return true;
                }
            }
            return false;
        }

        public override char RenderAtPosition(Position pos)
        {
            return pos == Head ? 'O' : 'o';
        }
    }
}
