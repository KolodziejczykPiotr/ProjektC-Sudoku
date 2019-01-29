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
        TextBox[,] texts = new TextBox[9,9];
        public int[] Array = new int[9];
        bool flag = false;
        public MainWindow()
        {
            InitializeComponent();
            
          
        }


       


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SudokuGrid.Children.CopyTo(test, 0);

            SudokuEngine.NewGame(SudokuBoard,Array);

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
                    
                    if(texts[i,j]!=null)
                    texts[i,j].Text = SudokuBoard[i, j].ToString();
                    if (texts[i,j].Text == "0")
                    {
                        texts[i, j].Text = "";
                    }
                        
                }



            }


            
            foreach(var i in texts)
            {
                if(i!=null)
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
       
        
        public void CheckAfter(object o , RoutedEventArgs e)
        {
            flag = false;
            l = (TextBox)e.OriginalSource;
            string[] NameArray = new string[2];
            NameArray =l.Name.Split('_');
            if(texts[Convert.ToInt32(NameArray[1]), Convert.ToInt32(NameArray[2])]!=null)
            texts[Convert.ToInt32(NameArray[1]), Convert.ToInt32(NameArray[2])].Text = l.Text;

            texts[Convert.ToInt32(NameArray[1]), Convert.ToInt32(NameArray[2])].Background = Brushes.White;

            for (int i = 0; i < 9; i++)
            {
                if(texts[i,Convert.ToInt32(NameArray[2])].Text!="")
                Array[i] =Convert.ToInt32(texts[i,Convert.ToInt32(NameArray[2])].Text);
                if (texts[i, Convert.ToInt32(NameArray[2])].Text == "")
                {
                    Array[i] = 0;
                }

                
       
            }
            flag = SudokuEngine.CheckAfterChangeNumbers(Array);
            if(!flag)
            for (int i = 0; i < 9; i++)
            {
                if (texts[Convert.ToInt32(NameArray[1]),i].Text != "")
                    Array[i] = Convert.ToInt32(texts[Convert.ToInt32(NameArray[1]), i].Text);
                if (texts[Convert.ToInt32(NameArray[1]), i].Text == "")
                {
                    Array[i] = 0;
                }



            }

            flag = SudokuEngine.CheckAfterChangeNumbers(Array);
            if (flag)
            {
                texts[Convert.ToInt32(NameArray[1]), Convert.ToInt32(NameArray[2])].Background = Brushes.Red;
            }


        }

    }
}
