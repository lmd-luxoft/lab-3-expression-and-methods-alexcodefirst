using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XO
{
    class Program
    {
       static char win = '-';
       static string PlayerName1, PlayerName2;
       static char[] cells = new char[]{ '-', '-', '-', '-', '-', '-', '-', '-', '-' };

        static void show_cells()
        {
            Console.Clear();

            Console.WriteLine("Числа клеток:");
            Console.WriteLine("-1-|-2-|-3-");
            Console.WriteLine("-4-|-5-|-6-");
            Console.WriteLine("-7-|-8-|-9-");

            Console.WriteLine("Текущая ситуация (---пустой):");
            Console.WriteLine($"-{cells[0]}-|-{cells[1]}-|-{cells[2]}-");
            Console.WriteLine($"-{cells[3]}-|-{cells[4]}-|-{cells[5]}-");
            Console.WriteLine($"-{cells[6]}-|-{cells[7]}-|-{cells[8]}-");        
        }
        static void make_move(int num)
        {
            string raw_cell;
            int cell;

            Console.Write(GetCurrentPlayer(num));

            do
            {
                Console.Write(",введите номер ячейки,сделайте свой ход:");

                raw_cell = Console.ReadLine();
            }
            while (!Int32.TryParse(raw_cell, out cell));
            while (CantMove(cell))
            {
                do
                {
                    Console.Write("Введите номер правильного ( 1-9 ) или пустой ( --- ) клетки , чтобы сделать ход:");
                    raw_cell = Console.ReadLine();
                }
                while (!Int32.TryParse(raw_cell, out cell));
                Console.WriteLine();
            }

            cells[cell - 1] = GetCellSymbol(num);
        }

        static char check()
        {
            for (int i = 0; i < 3; i++)
            {
                if (HasWin(i)) return cells[i];
            }

            return '-';
        }

        static void result()
        {
            Console.WriteLine($"{GetWinner()} вы  выиграли поздравляем {GetLoser()} а вы проиграли...");
        }

        static void Main(string[] args)
        {
            do
            {
                Console.Write("Введите имя первого игрока : ");
                PlayerName1 = Console.ReadLine();

                Console.Write("Введите имя второго игрока: ");
                PlayerName2 = Console.ReadLine();
                Console.WriteLine();
            } while (PlayerName1 == PlayerName2);

            show_cells();

            for (int move = 1; move <= 9; move++)
            {
                make_move(GetPlayerTurn(move));

                show_cells();

                if (move >= 5)
                {
                    win = check();
                    if (win != '-')
                        break;
                }

            }

            result();
        }

        private static int GetPlayerTurn(int move)
        {
            return move % 2 != 0 ? 1 : 2;
        }

        private static string GetWinner()
        {
            return win == 'X' ? PlayerName1 : PlayerName2;
        }

        private static string GetLoser()
        {
            return win == 'X' ? PlayerName2 : PlayerName1;
        }

        private static bool HasWin(int i)
        {
            return HasHorizontalWin(i) || HasVarticalWin(i) || HasDiagonalWin();
        }

        private static bool HasDiagonalWin()
        {
            return (cells[2] == cells[4] && cells[4] == cells[6]) || (cells[0] == cells[4] && cells[4] == cells[8]);
        }
        private static bool HasVarticalWin(int i)
        {
            return cells[i] == cells[i + 3] && cells[i + 3] == cells[i + 6];
        }

        private static bool HasHorizontalWin(int i)
        {
            return cells[i * 3] == cells[i * 3 + 1] && cells[i * 3 + 1] == cells[i * 3 + 2];
        }

        private static char GetCellSymbol(int num)
        {
            return num == 1 ? 'X' : 'O';
        }

        private static string GetCurrentPlayer(int num)
        {
            return num == 1 ? PlayerName1 : PlayerName2;
        }

        private static bool CantMove(int cell)
        {
            return cell > 9 || cell < 1 || cells[cell - 1] == 'O' || cells[cell - 1] == 'X';
        }
    }
}
