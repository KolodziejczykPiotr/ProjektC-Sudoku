using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace sudoku
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Random rnd = new Random();
            //Losuje dowolne liczby na plansze
                for (int i = 0; i < 9; i++)
                {
                    for (int x = 0; x < 9; x++)
                    {
                        SudokuBoard[i, x] = rnd.Next(1, 9);
                    }
                }
        }


        public static int[,] SudokuBoard = new int[9, 9];


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            box1.Text = SudokuBoard[0, 0].ToString();
            box2.Text = SudokuBoard[0, 1].ToString();
            box3.Text = SudokuBoard[0, 2].ToString();
            box4.Text = SudokuBoard[0, 3].ToString();
            box5.Text = SudokuBoard[0, 4].ToString();
            box6.Text = SudokuBoard[0, 5].ToString();
            box7.Text = SudokuBoard[0, 6].ToString();
            box8.Text = SudokuBoard[0, 7].ToString();
            box9.Text = SudokuBoard[0, 8].ToString();
            
        }
    }
}
