using ConsolePlay.GameEntities;
using ConsolePlay.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsolePlay
{
    internal class Treat : BaseEntity
    {
        private DateTime _creation;
        private static readonly Random _random = new Random();
        public Is State { get; set; }
       
        private Treat(Gameboard board, params Position[]? initialPositions) : base(board, initialPositions)
        {
            _creation = DateTime.Now;
            State = Is.Fresh;
        }
        
        public static Treat CreateTreat(Gameboard board)
        {
            var occupiedPositions = board.OccupiedPositions;
            Position pos = null!;
            do
            {
                var left = _random.Next(0, board.Width);
                var top = _random.Next(0, board.Height);
                pos = new Position(left, top);
            } while (!occupiedPositions.Contains(pos));

            return new Treat(board, pos);
        }

        private char GetCountdown()
        {
            try
            {
                var now = DateTime.Now;
                var diff = now - _creation;
                var seconds = diff.TotalSeconds;
                var countdown = 10 - seconds;
                if (countdown < 1)
                {
                    State = Is.Stale;
                    return '&';
                }
                return countdown switch
                {
                    < 0 => 'O',
                    < 1 => '1',
                    < 2 => '2',
                    < 3 => '3',
                    < 4 => '4',
                    < 5 => '5',
                    < 6 => '6',
                    < 7 => '7',
                    < 8 => '8',
                    < 9 => '9',
                    _ => '0'
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        public override char RenderAtPosition(Position pos) => (this, pos) switch
        {
            ({ State: Is.Stale}, _) => '&',
            ({ State: Is.Eaten}, _) => 'O',
            ({ State: Is.Fresh, _creation: var c}, _) => GetCountdown()
        };
            
    }

    public enum Is
    {
        Stale, Eaten, Fresh
    }
}
