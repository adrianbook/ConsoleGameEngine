using ConsolePlay.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsolePlay.GameEntities
{
    public abstract class MovingEntity : BaseEntity
    {
        public bool Alive { get; set; }
        public abstract void Move();
        public abstract bool Collide(BaseEntity other);
        public bool IsAlive { get { return Alive; } }
        protected MovingEntity(Gameboard board, params Position[]? initialPositions) : base(board, initialPositions)
        {
        }
    }
}
