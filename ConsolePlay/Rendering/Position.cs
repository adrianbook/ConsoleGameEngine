using ConsolePlay.GameEntities;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolePlay.Rendering
{
    public class Position
    {
        public int Top { get; init; }
        public int Left { get; init; }
        public char Symbol { get => Self != null ? Self!.RenderAtPosition(this) : ' '; }
        public BaseEntity? Self { get; init; } = null!;
        public Position(int left, int top, BaseEntity? self = null)
        {
            Top = top;
            Left = left;
            Self = self;
        }

        public Position() { }

        public override bool Equals(object? obj)
        {
            if (obj == null || !GetType().Equals(obj.GetType()))
            {
                return false;
            }
            if (obj is Position other)
            {
                if (other.Top != Top)
                {
                    return false;
                }
                if (other.Left != Left)
                {
                    return false;
                }
            }
            return true;
        }

        public bool StrictEquals(object? obj)
        {
            if (!this.Equals(obj)){
                return false;
            }
            if (obj is Position other && other.Symbol == this.Symbol)
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Top, Left);
        }

    }

    public class PositionRenderingComparer : IEqualityComparer<Position>
    {
        public bool Equals(Position? x, Position? y)
        {
            if (x == null || y == null)
            {
                return false;
            }
            return !x.Equals(y) && x.Symbol == y.Symbol;
        }

        public int GetHashCode([DisallowNull] Position obj)
        {
            return HashCode.Combine(obj, obj.Symbol);
        }
    }
}
