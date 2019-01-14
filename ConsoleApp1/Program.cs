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
            
            SudokuEngine.NewGame(SudokuEngine.Board);
            SudokuEngine.DrawBoard();
            
            Console.ReadLine();
            
        }
        





    }
}
