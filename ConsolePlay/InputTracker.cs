using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolePlay
{
    internal class InputTracker
    {
        public bool InputChange { get; private set; } = false;
        private ConsoleKeyInfo? _currentInput;

        public void ChangeInput(ConsoleKeyInfo input)
        {
            _currentInput = input;
            InputChange= true;
        }

        public ConsoleKeyInfo GetInput()
        {
            InputChange= false;
            return (ConsoleKeyInfo)_currentInput!;
        }

        public ConsoleKey? PeekInputValue()
        {
            return _currentInput?.Key;
        }
    }
}
