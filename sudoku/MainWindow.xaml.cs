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
using SudokuLibrary;
namespace sudoku
{
    public partial class MainWindow : Window
    {
        public int[,] SudokuBoard = new int[9, 9];
        UIElement[] test = new UIElement[90];
        Border[] borders = new Border[90];
        TextBox[] texts = new TextBox[90];
        public int[] Array = new int[9];
        
        public MainWindow()
        {
            InitializeComponent();
        
          
        }


       


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SudokuGrid.Children.CopyTo(test, 0);

            SudokuEngine.NewGame(SudokuBoard);

            for (int i = 0; i < borders.Length; i++)
            {
                borders[i] = test[i] as Border;


            }


            for (int i = 0; i < borders.Length; i++)
            {
                if (borders[i] != null)
                {
                    texts[i] = borders[i].Child as TextBox;
                }
            }
            int z = 0;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (z > 80)
                    {
                        break;
                    }
                    
                    texts[z].Text = SudokuBoard[i, j].ToString();
                    if (texts[z].Text == "0")
                    {
                        texts[z].Text = "";
                    }
                        z++;
                }



            }

           


        }

        

      

       
    }
}
