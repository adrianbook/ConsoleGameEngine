// See https://aka.ms/new-console-template for more information
using ConsolePlay;
using ConsolePlay.GameEntities;
using ConsolePlay.Rendering;

var height = Console.LargestWindowHeight;
var width = Console.LargestWindowWidth;


var engine = new GraphicsEngine(height, width);
var board = new Gameboard(height, width, "Game Over");
var inputTracker = new InputTracker();
var snake = new Snake(board);
board.RegisterEntity(snake);
//board.RegisterEntity(Treat.CreateTreat(board));
board.GameOver= true;
Console.WriteLine("ojojoj");
do
{
    try
    {
        while (Console.KeyAvailable == false)
        {
            if (inputTracker.InputChange)
            {
                 snake.HandleInput(inputTracker.GetInput());
            }
            if (snake.Munch)
            {
                board.RegisterEntity(Treat.CreateTreat(board));
            }
            snake.Move();

            engine.Render(board.ResolveFrame());

            Thread.Sleep(30);
        }

        inputTracker.ChangeInput(Console.ReadKey(true));
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }

} while (inputTracker.PeekInputValue() != ConsoleKey.Escape);

Console.WriteLine("ojojoj");