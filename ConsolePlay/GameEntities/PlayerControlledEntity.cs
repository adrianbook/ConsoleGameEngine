using ConsolePlay.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolePlay.GameEntities
{
    public abstract class PlayerControlledEntity : MovingEntity
    {
        public abstract void HandleInput(ConsoleKeyInfo key);
        public PlayerControlledEntity(Gameboard board, params Position[]? initialPositions) : base(board, initialPositions)
        {
        }
    }
}
