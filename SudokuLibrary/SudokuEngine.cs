using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SudokuLibrary
{/// <summary>
///    Silnik do gry sudoku 
/// </summary>
    public static class SudokuEngine
    {
        private static int[,] Board = new int[9, 9];

        /// <summary>
        /// Tworzy nowa gre ,losując liczby do gry
        /// </summary>
        /// <param name="Board"></param>
        /// <param name="b"></param>
        public static void NewGame(int[,] Board, int[] b)
        {
            b = new int[9];

            Random rnd = new Random();
            //Losuje dowolne liczby na plansze
            for (int r = 0; r < 6; r++)
            {
                for (int i = 0; i < 9; i++)
                {
                    for (int x = 0; x < 9; x++)
                    {


                        Board[i, x] = rnd.Next(1, 9);

                    }


                }
            }

            //sprawdza na tablicy czy sie nie powtarzaja liczby poziom
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    b[j] = Board[i, j];
                    Check(b);
                    Board[i, j] = b[j];

                }


            }
            //to samo tylko ze pion
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    b[j] = Board[j, i];
                    Check(b);
                    Board[j, i] = b[j];

                }


            }
            for (int i = 0; i <= 6; i += 3)
            {
                for (int j = 0; j <= 6; j += 3)
                {
                    CheckSmallSquare(i, j, Board);
                }
            }










        }

        /// <summary>
        /// Rysuje plansze w konsoli
        /// </summary>
        public static void DrawBoard()
        {

            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    Console.Write(Board[x, y] + " ");
                }
                Console.WriteLine();
            }





        }

        /// <summary>
        /// metoda ktora sprawdza male kwadraty w ciele metody NewGame
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="Board"></param>
        public static void CheckSmallSquare(int i, int j, int[,] Board)
        {
            int[] array = new int[9];
            int number = 0;
            for (int r = i; r < i + 3; r++)
            {
                for (int l = j; l < j + 3; l++)
                {
                    array[number] = Board[r, l];
                    number++;

                }

            }
            Check(array);
            number = 0;
            for (int r = i; r < i + 3; r++)
            {
                for (int l = j; l < j + 3; l++)
                {
                    Board[r, l] = array[number];
                    number++;
                }

            }






        }
        /// <summary>
        /// Obsluguje sprawdzanie w pionie i poziomie w ciele metody NewGame
        /// </summary>
        /// <param name="b"></param>
        public static void Check(int[] b)
        {



            for (int i = 0; i < b.Length; i++)
            {
                for (int j = 0; j < 9; j++)
                {

                    if (b[i] == b[j] && i != j)
                    {
                        b[j] = 0;


                    }
                }

            }


        }
        /// <summary>
        /// Sprawdza po zmianie numerów w tablicy
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool CheckAfterChangeNumbers(int[] b)
        {



            for (int i = 0; i < b.Length; i++)
            {
                for (int j = 0; j < 9; j++)
                {

                    if (b[i] == b[j] && i != j && b[i] != 0)
                    {

                        return true;

                    }
                }

            }

            return false;
        }
        /// <summary>
        /// Sprawdza male kwadraty po zmianie numerów
        /// </summary>
        /// <param name="NameArray"></param>
        /// <param name="texts"></param>
        /// <param name="array"></param>
        public static void CheckSmallSquare(string[] NameArray, TextBox[,] texts, int[] array)
        {
            int first = 0;
            int second = 0;
            switch (Convert.ToInt32(NameArray[1]))
            {
                case 0:
                case 1:
                case 2:
                    first = 0;
                    break;
                case 3:
                case 4:
                case 5:
                    first = 3;
                    break;
                case 6:
                case 7:
                case 8:
                    first = 6;
                    break;



            }

            switch (Convert.ToInt32(NameArray[2]))
            {
                case 0:
                case 1:
                case 2:
                    second = 0;
                    break;
                case 3:
                case 4:
                case 5:
                    second = 3;
                    break;
                case 6:
                case 7:
                case 8:
                    second = 6;
                    break;
            }

            int number = 0;
            for (int r = first; r < first + 3; r++)
            {
                for (int l = second; l < second + 3; l++)
                {
                    if (texts[r, l].Text != "")
                    {
                        array[number] = Convert.ToInt32(texts[r, l].Text);
                    }
                    if (texts[r, l].Text == "")
                    {
                        array[number] = 0;
                    }

                    number++;

                }

            }
        }
        /// <summary>
        /// Sprawdza w pionie po zmianie numerów
        /// </summary>
        /// <param name="NameArray"></param>
        /// <param name="texts"></param>
        /// <param name="array"></param>
        public static void CheckVertical(string[] NameArray, TextBox[,] texts, int[] array)
        {
            for (int i = 0; i < 9; i++)
            {
                if (texts[Convert.ToInt32(NameArray[1]), i].Text != "")
                    array[i] = Convert.ToInt32(texts[Convert.ToInt32(NameArray[1]), i].Text);
                if (texts[Convert.ToInt32(NameArray[1]), i].Text == "")
                {
                    array[i] = 0;
                }



            }
        }
        /// <summary>
        /// Sprzawdza w poziomie po zmianie numerów
        /// </summary>
        /// <param name="NameArray"></param>
        /// <param name="texts"></param>
        /// <param name="array"></param>
        public static void CheckHorizontal(string[] NameArray, TextBox[,] texts, int[] array)
        {
            for (int i = 0; i < 9; i++)
            {
                if (texts[i, Convert.ToInt32(NameArray[2])].Text != "")
                    array[i] = Convert.ToInt32(texts[i, Convert.ToInt32(NameArray[2])].Text);
                if (texts[i, Convert.ToInt32(NameArray[2])].Text == "")
                {
                    array[i] = 0;
                }



            }
        }
    }
}










