using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SudokuLibrary;
namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {

             SudokuEngine.NewGame(ref SudokuEngine.Board);
             SudokuEngine.DrawBoard(ref SudokuEngine.Board, out SudokuEngine.s);

           
            Console.ReadLine();
        }

       

        


    }
}
