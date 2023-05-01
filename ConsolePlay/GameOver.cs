using ConsolePlay.GameEntities;
using ConsolePlay.Rendering;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolePlay
{
    internal class GameOver : BaseEntity
    {
        private string _gameOverText = "Game Over";
        private int renderHeight;
        private int startingWidth;
        public GameOver(Gameboard board, string gameOverText): base(board)
        {
            _gameOverText = gameOverText;
            var height = board.Height;
            var width = board.Width;

            renderHeight= height / 2;
            startingWidth = (width / 2) - (_gameOverText.Length / 2);
            Positions = new List<Position>();
            for (int i = 0; i < _gameOverText.Length; i++)
            {
                Positions.Add(new Position(startingWidth+i, renderHeight, this));
                
            }
        }

        public override char RenderAtPosition(Position pos)
        {
            return _gameOverText[pos.Left-startingWidth];
        }
    }
}
