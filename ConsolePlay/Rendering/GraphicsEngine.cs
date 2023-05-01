using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#pragma warning disable CA1416

namespace ConsolePlay.Rendering
{
    internal class GraphicsEngine
    {
        public int Height { get; }
        public int Width { get; }
        public string? CurrentMessage { get; private set; }
        private HashSet<Position>? _latestPositions;
        public GraphicsEngine(int height, int width)
        {
            Height = height;
            Width = width;
            Console.WindowHeight = height;
            Console.WindowWidth = width;
            Console.BufferHeight = height;
            Console.BufferWidth = width;
            Console.CursorVisible = false;
        }
        public void Render(HashSet<Position> incommingPositions)
        {
            try
            {
                CurrentMessage = null;

                if (!incommingPositions.Any())
                {
                    Console.Clear();
                    return;
                }
                if (_latestPositions == null || !_latestPositions.Any())
                {
                    RenderOnEmptyScreen(incommingPositions);
                }
                else
                {
                    RenderOnTopOf(incommingPositions);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void RenderOnEmptyScreen(HashSet<Position> incommingPositions)
        {
            Console.Clear();
            foreach (var position in incommingPositions)
            {
                Console.SetCursorPosition(position.Left, position.Top);
                Console.Write(position.Symbol);
            }
            _latestPositions= incommingPositions;
        }
        private void RenderOnTopOf(HashSet<Position> incommingPositions)
        {
            var positionsToUpdate = new HashSet<Position>(incommingPositions, new PositionRenderingComparer());
            var positionsToRemove = new HashSet<Position>(_latestPositions!, new PositionRenderingComparer());

            positionsToUpdate.ExceptWith(_latestPositions!);
            positionsToRemove.ExceptWith(incommingPositions);

            foreach (var pos in positionsToUpdate)
            {
                Console.SetCursorPosition(pos.Left, pos.Top);
                Console.Write(pos.Symbol);
            }
            foreach (var pos in positionsToRemove)
            {
                Console.SetCursorPosition(pos.Left, pos.Top);
                Console.Write(' ');
            }
            _latestPositions = incommingPositions;
        }

        public void RenderMessage(string message)
        {
            if (CurrentMessage == message) return;
            CurrentMessage = message;
            Console.Clear();
            Console.SetCursorPosition(Width / 2 - message.Length, Height / 2);
            Console.Write(message);
        }
    }
}
