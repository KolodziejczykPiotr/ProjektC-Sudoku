using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        UIElement[] UIs = new UIElement[90];
        Border[] borders = new Border[90];
        TextBox[,] texts = new TextBox[9, 9];
        DateTime time;
        DispatcherTimer timer;
        int[] Array = new int[9];
        bool flag = false;
        bool endGame;
        bool isGenerated = true;
        int tick;
        /// <summary>
        /// Poczatkowa metoda
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer;


        }

        private void Timer(object o, EventArgs e)
        {
            TimerLabel.Content = time.AddSeconds(tick++).ToLongTimeString();
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Ukrywa label z informacja końca gry
            EndGameLabel.Visibility = Visibility.Hidden;
            //Zeruje zegar
            tick = 0;
            //Startuje zegar
            timer.Start();

            isGenerated = false;
            //Kopiuje wszystkie dzieci glownego Grida
            SudokuGrid.Children.CopyTo(UIs, 0);
            //Uruchamia losowanie
            SudokuEngine.NewGame(SudokuBoard, Array);
            //Przypisuje wszystkie bordery do tablicy borderów
            for (int i = 0; i < borders.Length; i++)
            {
                borders[i] = UIs[i] as Border;


            }

            int z = 0;
            //szuka z wszystkich borderów textboxy
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


            //Zdarzenie ktore ma sie wykonac po zmianie w TextBox
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
        /// <summary>
        /// Metoda ktora sie wykonuje gdy zajdzie zdarzenie
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
       
        public void CheckAfter(object o, RoutedEventArgs e)
        {


            if (isGenerated)
            {
                flag = false;
                //pobiera informacje w ktorym textbox zostala zmieniona liczba
                l = (TextBox)e.OriginalSource;
                //pobiera nazwe textboxa gdzie byla zmieniona liczba
                string[] NameArray = new string[2];

                NameArray = l.Name.Split('_');

                if (texts[Convert.ToInt32(NameArray[1]), Convert.ToInt32(NameArray[2])] != null)
                    texts[Convert.ToInt32(NameArray[1]), Convert.ToInt32(NameArray[2])].Text = l.Text;

                //sprawdza czy jest poprawny format
                if (l.Text != "1" && l.Text != "2" && l.Text != "3" && l.Text != "4" && l.Text != "5" && l.Text != "6" && l.Text != "7" && l.Text != "8" && l.Text != "9" && l.Text != "")
                {
                    ErrorWindow.Visibility = Visibility.Visible;
                    l.Text = "";

                }
                else
                {
                    //zmienia kolor tła na biały
                    texts[Convert.ToInt32(NameArray[1]), Convert.ToInt32(NameArray[2])].Background = Brushes.White;

                    // Sprawdza Maly kwadrat
                    if (!flag)
                    {
                        SudokuEngine.CheckSmallSquare(NameArray, texts, Array);
                    }
                    flag = SudokuEngine.CheckAfterChangeNumbers(Array);

                    //Sprawdza w pionie
                    if (!flag)
                        SudokuEngine.CheckVertical(NameArray, texts, Array);

                    flag = SudokuEngine.CheckAfterChangeNumbers(Array);
                    //Sprawdza w poziomie
                    if (!flag)
                    {
                        SudokuEngine.CheckHorizontal(NameArray, texts, Array);
                    }

                    flag = SudokuEngine.CheckAfterChangeNumbers(Array);

                    //Zmienia kolor tła jeżeli liczba się powtarza
                    if (flag)
                    {
                        texts[Convert.ToInt32(NameArray[1]), Convert.ToInt32(NameArray[2])].Background = Brushes.Red;
                    }

                    //Przypisuje do Boarda wszystkie textBoxy
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
                    //Sprawdza czy koniec gry poprzez sprawdzenie czy żadna liczba sie nie powtarza ,
                    //w swoim obrębie i czy nie ma pusych pól
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
                    //Kończy Gre wyswietlajac YouWin i zatrzymuje czas 
                    if (!endGame)
                    {
                        EndGameLabel.Visibility = Visibility.Visible;
                        timer.Stop();
                    }
                }
            }
        }
        //Wyswietla label z błędem gdy zły format
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ErrorWindow.Visibility = Visibility.Hidden;
        }
    }








}


