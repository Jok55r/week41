using System;

namespace week41
{
    internal class Program
    {
        static int width = 60;
        static int height = 25;

        static char[,] maze = new char[height + 1, width + 1];

        static int playerX = 1;
        static int playerY = 1;

        static int coin = 0;
        static int maxCoin = 0;
        static int moves = 0;
        static int breaks = 3;

        static char wall = '█';
        static char space = ' ';

        static void Main(string[] args)
        {
            MakeMaze();
            for(; ; )
            {
                DrawMaze();
                MovementInput();
                Console.Clear();
                PickCoin();
                WinCheck();
            }
        }
        static void MakeMaze()
        {
            Random rnd = new Random();
            for (int i = 0; i <= height; i++)
            {
                for (int j = 0; j <= width; j++)
                {
                    if (i == 0 || j == 0 || i == height || j == width) maze[i, j] = wall;
                    else if (i == height - 1 && j == width - 1) maze[i, j] = 'W';
                    else if (rnd.Next(0, 4) == 1 && (i != 1 || j != 1)) maze[i, j] = wall;
                    else if (rnd.Next(0, 100) == 1) { maze[i, j] = 'C'; maxCoin++; }
                    else maze[i, j] = space;
                }
            }
        }

        static void DrawMaze()
        {
            for (int i = 0; i <= height; i++)
            {
                for (int j = 0; j <= width; j++)
                {
                    if (i == playerY && j == playerX) Console.Write('@');
                    else Console.Write(maze[i, j]);
                }
                if (i == 0) Console.Write($"     coins: {coin}/{maxCoin}");
                if (i == 1) Console.Write($"     moves: {moves}");
                if (i == 2) Console.Write($"     breaks left: {breaks}");

                Console.WriteLine();
            }
            Console.WriteLine("WASD - movement");
            Console.WriteLine("R - restart");
            Console.WriteLine("arrows - break wall");
        }

        static void PickCoin()
        {
            if (maze[playerY, playerX] == 'C')
            {
                maze[playerY, playerX] = ' ';
                coin++;
            }
        }

        static void WinCheck()
        {
            if (playerX == width - 1 && playerY == height - 1)
            {
                Console.WriteLine("---Yay, you win!---");
                Restart();
            }
        }

        static void Restart()
        {
            playerX = 1;
            playerY = 1;
            maxCoin = 0;
            coin = 0;
            moves = 0;
            breaks = 3;
            MakeMaze();
        }

        static void MovementInput()
        {
            moves++;
            ConsoleKey input = Console.ReadKey(true).Key;
            if (input == ConsoleKey.R) Restart();
            CanBreakCheck(input);
            MovingIfCan(input);
        }

        static void MovingIfCan(ConsoleKey input)
        {
            if ((input == ConsoleKey.A || input == ConsoleKey.LeftArrow) && maze[playerY, playerX - 1] != wall) playerX--;
            if ((input == ConsoleKey.D || input == ConsoleKey.RightArrow) && maze[playerY, playerX + 1] != wall) playerX++;
            if ((input == ConsoleKey.S || input == ConsoleKey.DownArrow) && maze[playerY + 1, playerX] != wall) playerY++;
            if ((input == ConsoleKey.W || input == ConsoleKey.UpArrow) && maze[playerY - 1, playerX] != wall) playerY--;
        }

        static void CanBreakCheck(ConsoleKey input)
        {
            if (input == ConsoleKey.LeftArrow && maze[playerY, playerX - 1] == wall && breaks >= 1) 
            { 
                maze[playerY, playerX - 1] = space; 
                breaks--; 
            }
            if (input == ConsoleKey.RightArrow && maze[playerY, playerX + 1] == wall && breaks >= 1)
            {
                maze[playerY, playerX + 1] = space;
                breaks--;
            }
            if (input == ConsoleKey.DownArrow && maze[playerY + 1, playerX] == wall && breaks >= 1)
            {
                maze[playerY + 1, playerX] = space;
                breaks--;
            }
            if (input == ConsoleKey.UpArrow && maze[playerY - 1, playerX] == wall && breaks >= 1)
            {
                maze[playerY - 1, playerX] = space;
                breaks--;
            }
        }
    }
}