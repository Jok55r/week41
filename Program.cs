using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace week41
{
    internal class Program
    {
        static int wide = 50;
        static int hight = 20;

        static char[,] lab = new char[hight + 1, wide + 1];

        static int x = 1;
        static int y = 1;

        static int coin = 0;
        static int maxCoin = 0;
        static int moves = 0;

        static void Main(string[] args)
        {
            Make();
            for(; ; )
            {
                Draw();
                Movement();
                Console.Clear();
                CheckIfCoin();
                CheckIfWin();
            }
        }

        static void Make()
        {
            Random rnd = new Random();
            for (int i = 0; i <= hight; i++)
            {
                for (int j = 0; j <= wide; j++)
                {
                    if (i == 0 || j == 0 || i == hight || j == wide) lab[i, j] = '█';
                    else if (i == hight - 1 && j == wide - 1) lab[i, j] = 'W';
                    else if (rnd.Next(0, 100) == 1) { lab[i, j] = 'C'; maxCoin++; }
                    else if (rnd.Next(0, 4) == 1) lab[i, j] = '█';
                    else lab[i, j] = ' ';
                }
            }
        }

        static void Draw()
        {
            for (int i = 0; i <= hight; i++)
            {
                for (int j = 0; j <= wide; j++)
                {
                    if (i == y && j == x) Console.Write('@');
                    else Console.Write(lab[i, j]);
                }
                if (i == 0) Console.Write($"     coins: {coin}/{maxCoin}");
                if (i == 1) Console.Write($"     moves: {moves}");

                Console.WriteLine();
            }
            Console.WriteLine("WASD - movement");
            Console.WriteLine("R - restart");
        }

        static void CheckIfCoin()
        {
            if (lab[y, x] == 'C')
            {
                lab[y, x] = ' ';
                coin++;
            }
        }

        static void CheckIfWin()
        {
            if (x == wide - 1 && y == hight - 1)
            {
                Console.WriteLine("---Yay, you win!---");
                Restart();
            }
        }

        static void Restart()
        {
            Make();
            x = 1;
            y = 1;
            maxCoin = 0;
            coin = 0;
            moves = 0;
        }

        static void Movement()
        {
            ConsoleKey input = Console.ReadKey(true).Key;
            if (input == ConsoleKey.R) Restart();
            if (input == ConsoleKey.S) {y++; CheckIfCanMove(true, true); }
            if (input == ConsoleKey.W) {y--; CheckIfCanMove(true, false); }
            if (input == ConsoleKey.A) {x--; CheckIfCanMove(false, false); }
            if (input == ConsoleKey.D) {x++; CheckIfCanMove(false, true); }
            moves++;
        }

        static void CheckIfCanMove(bool isY, bool plus)
        {
            if (lab[y, x] == '█')
            {
                if (plus && isY) y--;
                if (!plus && isY) y++;
                if (!plus && !isY) x++;
                if (plus && !isY) x--;
            }
        }
    }
}