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
        int[,] SudokuBoard = new int[9, 9];
        UIElement[] test = new UIElement[90];
        Border[] borders = new Border[90];
        TextBox[,] texts = new TextBox[9, 9];
        int[] Array = new int[9];
        bool flag = false;
        public MainWindow()
        {
            InitializeComponent();


        }





        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SudokuGrid.Children.CopyTo(test, 0);

            SudokuEngine.NewGame(SudokuBoard, Array);

            for (int i = 0; i < borders.Length; i++)
            {
                borders[i] = test[i] as Border;


            }

            int z = 0;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (z > 81)
                    {
                        break;
                    }
                    if (borders[z] != null)
                    {
                        texts[i, j] = borders[z].Child as TextBox;

                    }
                    z++;
                }

            }

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {

                    if (texts[i, j] != null)
                        texts[i, j].Text = SudokuBoard[i, j].ToString();
                    if (texts[i, j].Text == "0")
                    {
                        texts[i, j].Text = "";
                    }

                }



            }



            foreach (var i in texts)
            {
                if (i != null)
                    i.TextChanged += CheckAfter;
            }
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    texts[i, j].Name = String.Format("_{0}_{1}", i, j);
                }
            }
        }


        public void CheckAfter(object o, RoutedEventArgs e)
        {

            flag = false;

            l = (TextBox)e.OriginalSource;

            string[] NameArray = new string[2];

            NameArray = l.Name.Split('_');

            if (texts[Convert.ToInt32(NameArray[1]), Convert.ToInt32(NameArray[2])] != null)
                texts[Convert.ToInt32(NameArray[1]), Convert.ToInt32(NameArray[2])].Text = l.Text;


            texts[Convert.ToInt32(NameArray[1]), Convert.ToInt32(NameArray[2])].Background = Brushes.White;

            // Sprawdza Maly kwadrat
            if (!flag)
            {
                CheckSmallSquare(NameArray);
            }
            flag = SudokuEngine.CheckAfterChangeNumbers(Array);

            //Sprawdza w pionie
            if (!flag)
                CheckVertical(NameArray);

            flag = SudokuEngine.CheckAfterChangeNumbers(Array);
            //Sprawdza w poziomie
            if (!flag)
            {
                CheckHorizontal(NameArray);
            }

            flag = SudokuEngine.CheckAfterChangeNumbers(Array);

            if (flag)
            {
                texts[Convert.ToInt32(NameArray[1]), Convert.ToInt32(NameArray[2])].Background = Brushes.Red;
            }

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (texts[i, j].Text != "")
                        SudokuBoard[i, j] = Convert.ToInt32(texts[i, j].Text);
                    else
                        SudokuBoard[i, j] = 0;
                }
            }
            

            if (SudokuEngine.EndGame(SudokuBoard))
            {
                EndGameLabel.Visibility = Visibility.Visible;
            }


        }

        private void CheckHorizontal(string[] NameArray)
        {
            for (int i = 0; i < 9; i++)
            {
                if (texts[i, Convert.ToInt32(NameArray[2])].Text != "")
                    Array[i] = Convert.ToInt32(texts[i, Convert.ToInt32(NameArray[2])].Text);
                if (texts[i, Convert.ToInt32(NameArray[2])].Text == "")
                {
                    Array[i] = 0;
                }



            }
        }

        private void CheckVertical(string[] NameArray)
        {
            for (int i = 0; i < 9; i++)
            {
                if (texts[Convert.ToInt32(NameArray[1]), i].Text != "")
                    Array[i] = Convert.ToInt32(texts[Convert.ToInt32(NameArray[1]), i].Text);
                if (texts[Convert.ToInt32(NameArray[1]), i].Text == "")
                {
                    Array[i] = 0;
                }



            }
        }

        private void CheckSmallSquare(string[] NameArray)
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
                        Array[number] = Convert.ToInt32(texts[r, l].Text);
                    }
                    if (texts[r, l].Text == "")
                    {
                        Array[number] = 0;
                    }

                    number++;

                }

            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            texts[0, 0].Text = 4.ToString();
            texts[0, 1].Text = 2.ToString();
            texts[0, 2].Text = 8.ToString();
            texts[0, 3].Text = 3.ToString();
            texts[0, 4].Text = 5.ToString();
            texts[0, 5].Text = 1.ToString();
            texts[0, 6].Text = 6.ToString();
            texts[0, 7].Text = 9.ToString();
            texts[0, 8].Text = 7.ToString();
            texts[1, 0].Text = 1.ToString();
            texts[1, 1].Text = 4.ToString();

            texts[1, 2].Text = 5.ToString();
            texts[1, 3].Text = 6.ToString();
            texts[1, 4].Text = 9.ToString();
            texts[1, 5].Text = 8.ToString();
            texts[1, 6].Text = 7.ToString();
            texts[1, 7].Text = 3.ToString();

            texts[1, 8].Text = 2.ToString();
            texts[2, 0].Text = 5.ToString();
            texts[2, 1].Text = 1.ToString();

            texts[2, 2].Text = 3.ToString();
            texts[2, 3].Text = 9.ToString();
            texts[2, 4].Text = 8.ToString();
            texts[2, 5].Text = 7.ToString();
            texts[2, 6].Text = 4.ToString();
            texts[2, 7].Text = 2.ToString();

            texts[2, 8].Text = 6.ToString();
            texts[3, 0].Text = 8.ToString();
            texts[3, 1].Text = 7.ToString();

            texts[3, 2].Text = 6.ToString();
            texts[3, 3].Text = 5.ToString();
            texts[3, 4].Text = 2.ToString();
            texts[3, 5].Text = 9.ToString();
            texts[3, 6].Text = 1.ToString();
            texts[3, 7].Text = 4.ToString();

            texts[3, 8].Text = 3.ToString();
            texts[4, 0].Text = 6.ToString();
            texts[4, 1].Text = 9.ToString();

            texts[4, 2].Text = 2.ToString();
            texts[4, 3].Text = 1.ToString();
            texts[4, 4].Text = 7.ToString();
            texts[4, 5].Text = 4.ToString();
            texts[4, 6].Text = 3.ToString();
            texts[4, 7].Text = 8.ToString();

            texts[4, 8].Text = 5.ToString();
            texts[5, 0].Text = 9.ToString();
            texts[5, 1].Text = 6.ToString();

            texts[5, 2].Text = 7.ToString();
            texts[5, 3].Text = 4.ToString();
            texts[5, 4].Text = 3.ToString();
            texts[5, 5].Text = 5.ToString();
            texts[5, 6].Text = 2.ToString();
            texts[5, 7].Text = 1.ToString();

            texts[5, 8].Text = 8.ToString();

            texts[6, 0].Text = 3.ToString();
            texts[6, 1].Text = 8.ToString();

            texts[6, 2].Text = 9.ToString();
            texts[6, 3].Text = 2.ToString();
            texts[6, 4].Text = 4.ToString();
            texts[6, 5].Text = 6.ToString();
            texts[6, 6].Text = 5.ToString();
            texts[6, 7].Text = 7.ToString();

            texts[6, 8].Text = 1.ToString();
            texts[7, 0].Text = 2.ToString();
            texts[7, 1].Text = 5.ToString();

            texts[7, 2].Text = 4.ToString();
            texts[7, 3].Text = 7.ToString();
            texts[7, 4].Text = 1.ToString();
            texts[7, 5].Text = 3.ToString();
            texts[7, 6].Text = 8.ToString();
            texts[7, 7].Text = 6.ToString();

            texts[7, 8].Text = 9.ToString();
            texts[8, 0].Text = 7.ToString();
            texts[8, 1].Text = 3.ToString();

            texts[8, 2].Text = 1.ToString();
            texts[8, 3].Text = 8.ToString();
            texts[8, 4].Text = 6.ToString();
            texts[8, 5].Text = 2.ToString();
            texts[8, 6].Text = 9.ToString();
            texts[8, 7].Text = 5.ToString();

            texts[8, 8].Text = 4.ToString();
        }
    }
}

