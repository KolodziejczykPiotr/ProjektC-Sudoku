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
        public static string s;

        public static void NewGame(ref int[,] Board)
        {
            for(int i = 0; i < 9; i++)
            {
                for(int x = 0; x < 9; x++)
                {
                    //Board[i, x] = (i * 3 + i / 3 + x) % 9 + 1;
                }
            }
        }
                 
        public static void DrawBoard(ref int[,] b , out string _s)
        {
            for(int x = 0; x < 9; x++)
            {
                for(int y = 0; y < 9; y++)
                {
                    s += b[x, y].ToString()+" " ;
                }
                s += Environment.NewLine;
            }
            
            Console.WriteLine(s);
            _s = s;
            s = "";
            
           
        }

                    
                
             


     






    }
}
