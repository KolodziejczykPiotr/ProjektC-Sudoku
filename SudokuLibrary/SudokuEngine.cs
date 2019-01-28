using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuLibrary
{
    public static class SudokuEngine
    {
        public static int[,] Board = new int[9, 9];


        public static void NewGame(int[,] Board)
        {
            int[] b = new int[9];

            Random rnd = new Random();
            //Losuje dowolne liczby na plansze
            for (int r = 0; r < 3; r++)
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
         //Rysuje Plansze       
        public static void DrawBoard()
        {
            
            for(int x = 0; x < 9; x++)
            {
                for(int y = 0; y < 9; y++)
                {
                    Console.Write(Board[x, y]+" ");
                }
                Console.WriteLine();
            }
         
            
            
            
           
        }
        //metoda ktora sprawdza male kwadraty 
        public static void CheckSmallSquare(int i,int j,int[,] Board)
        {
            int[] array = new int[9];
            int number = 0;
            for (int r =i; r <i+3; r++)
            {
                for(int l = j; l<j+3; l++)
                {
                       array[number] = Board[r, l];
                       number++;
                    
                }
               
            }
            Check(array);
            number = 0;
            for(int r = i; r < i + 3; r++)
            {
                for(int l = j; l < j + 3; l++)
                {
                    Board[r, l] = array[number];
                    number++;
                }
               
            }
            
            
            

            
            
        }
        //Obsluguje sprawdzanie w pionie i poziomie
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
      







    }
}
