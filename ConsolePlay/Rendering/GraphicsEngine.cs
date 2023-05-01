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
                    foreach (var position in incommingPositions)
                    {
                        Console.SetCursorPosition(position.Left, position.Top);
                        Console.Write(position.Symbol);
                    }
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

            var newEnumer = incommingPositions.GetEnumerator();
            var oldEnumer = _latestPositions!.GetEnumerator();
            for (; newEnumer.MoveNext() | oldEnumer.MoveNext();)
            {
                var N = newEnumer.Current;
                var O = oldEnumer.Current;
                if (oldEnumer.Current != null && !incommingPositions.Contains(oldEnumer.Current))
                {
                    Console.SetCursorPosition(oldEnumer.Current.Left, oldEnumer.Current.Top);
                    Console.Write(' ');
                }
                if (newEnumer.Current != null && !_latestPositions.Contains(newEnumer.Current))
                {
                    Console.SetCursorPosition(newEnumer.Current.Left, newEnumer.Current.Top);
                    Console.Write(newEnumer.Current.Symbol);
                }
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
