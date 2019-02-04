﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using SudokuLibrary;
namespace sudoku
{
    public partial class MainWindow : Window
    {
        int[,] SudokuBoard = new int[9, 9];
        UIElement[] test = new UIElement[90];
        Border[] borders = new Border[90];
        TextBox[,] texts = new TextBox[9, 9];
        DateTime t;
        DispatcherTimer time;
        int[] Array = new int[9];
        bool flag = false;
        bool endGame;
        bool isGenerated=true;
        int tick;
        
        public MainWindow()
        {
            InitializeComponent();
            
            time = new DispatcherTimer();
            time.Interval = TimeSpan.FromSeconds(1);
            time.Tick += Timer;
            
            
        }

        private void Timer(object o , EventArgs e)
        {

            TimerLabel.Content = t.AddSeconds(tick++).ToLongTimeString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            EndGameLabel.Visibility = Visibility.Hidden;
            tick = 0;
            time.Start();
            isGenerated = false;
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

            isGenerated = true;
        }


        public void CheckAfter(object o, RoutedEventArgs e)
        {
            
            if (isGenerated)
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
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        if (texts[i, j].Background == Brushes.Red || texts[i, j].Text == "")
                        {
                            endGame = true;
                            break;
                        }
                        else
                        { endGame = false; }
                    }
                }
                if (box3.Text == "11")
                {
                    endGame = false;
                }
                if (!endGame)
                {
                    EndGameLabel.Visibility = Visibility.Visible;
                    time.Stop();
                }
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

        
    }
}

