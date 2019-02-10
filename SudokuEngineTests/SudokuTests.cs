using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SudokuLibrary;
namespace SudokuEngineTests
{
    /// <summary>
    /// Testy
    /// </summary>
    [TestClass]
    public class SudokuTests
    {
        /// <summary>
        /// Test głównej metody
        /// </summary>
        [TestMethod]
        public void NewGameTest()
        {
            int[,] testBoard = new int[9, 9];
            int[] testBoard1 = new int[9];
            bool flag = true;
            SudokuEngine.NewGame(testBoard, testBoard1);
            foreach (var i in testBoard)
            {
                if (i != 0)
                {
                    flag = false;
                    break;
                }
                else
                {
                    flag = true;
                }
            }
            Assert.IsFalse(flag);

        }

    }
}
