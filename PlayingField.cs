using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PIIS_labs
{
    class PlayingField
    {
        public CellContent[ , ] field;

        public PlayingField()
        {
            field = new CellContent[3, 3] {
                {CellContent.EmptyCell, CellContent.EmptyCell, CellContent.EmptyCell},
                {CellContent.EmptyCell, CellContent.EmptyCell, CellContent.EmptyCell},
                {CellContent.EmptyCell, CellContent.EmptyCell, CellContent.EmptyCell}
            };
        }

        public PlayingField(PlayingField _PlayingField)
        {
            for(int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    field[i, j] = _PlayingField.field[i, j];
                }
            }
        }

        public void ShowField()
        {
            Console.Clear();
            int winner, one, two, three;
            (winner, one, two, three) = check_field();

            string symbol;
            ConsoleColor color;
            int coordinate;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if(field[i, j] == CellContent.EmptyCell)
                    {
                        symbol = CoordinatesInNumber(i, j).ToString();
                        color = ConsoleColor.DarkGray;
                    }
                    else
                    {
                        if (field[i, j] == CellContent.XCell) symbol = "X";
                        else symbol = "O";
                        coordinate = CoordinatesInNumber(i, j);
                        if (coordinate == one || coordinate == two || coordinate == three) color = ConsoleColor.Blue;
                        else color = ConsoleColor.White;
                    }
                    ShowSymbol(symbol, color);

                    if (j != 2)
                    {
                        Console.Write("|");
                    }
                }
                if (i != 2)
                {
                    Console.Write("\n---+---+---\n");
                }
            }
            Console.Write("\n");
        }

        private void ShowSymbol(string symbol, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(" " + symbol + " ");
            Console.ResetColor();
        }

        public int CoordinatesInNumber(int i, int j)
        {
            return (i * 3 + j + 1);
        }

        public (int i, int j) NumberInCoordinates(int coordinate)
        {
            return ((coordinate - 1) / 3, (coordinate - 1) % 3);
        }

        public (int winner, int one, int two, int three) check_field()
        {
            for(int i=0; i<3; i++)
            {
                if (field[i, 0]!=CellContent.EmptyCell && field[i, 0].Equals(field[i, 1]) && field[i, 0].Equals(field[i, 2])) return (field[i, 0].Equals(CellContent.XCell)?1:2, 
                        CoordinatesInNumber(i, 0), CoordinatesInNumber(i, 1), CoordinatesInNumber(i, 2));
            }
            for (int i = 0; i < 3; i++)
            {
                if (field[0, i] != CellContent.EmptyCell && field[0, i].Equals(field[1, i]) && field[0, i].Equals(field[2, i])) return (field[0, i].Equals(CellContent.XCell) ? 1 : 2,
                        CoordinatesInNumber(0, i), CoordinatesInNumber(1, i), CoordinatesInNumber(2, i));
            }
            if (field[1, 1] != CellContent.EmptyCell && field[1, 1].Equals(field[0, 0]) && field[1, 1].Equals(field[2, 2])) return (field[1, 1].Equals(CellContent.XCell) ? 1 : 2,
                         CoordinatesInNumber(0, 0), CoordinatesInNumber(1, 1), CoordinatesInNumber(2, 2));
            if (field[1, 1] != CellContent.EmptyCell && field[1, 1].Equals(field[2, 0]) && field[1, 1].Equals(field[0, 2])) return (field[1, 1].Equals(CellContent.XCell) ? 1 : 2,
                         CoordinatesInNumber(2, 0), CoordinatesInNumber(1, 1), CoordinatesInNumber(0, 2));

            int count = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (field[i, j] != CellContent.EmptyCell) count++;
                }
            }
            if (count == 9) return (0, 0, 0, 0);
            else return (-1, 0, 0, 0);
        }

        public bool step(int coordinate, CellContent content)
        {
            int i, j;
            (i, j) = NumberInCoordinates(coordinate);
            if (field[i, j] == CellContent.EmptyCell)
            {
                field[i, j] = content;
                return true;
            }
            else return false;
        }
    }
}
