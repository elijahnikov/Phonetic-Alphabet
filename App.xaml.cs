using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Phonetics
{

    public partial class App : Application
    {

        public void initGame()
        {

            ///((MainWindow)System.Windows.Application.Current.MainWindow).
            ((MainWindow)System.Windows.Application.Current.MainWindow).entryBox.IsEnabled = true;
            initTimer();
            gameTimer.Start();
            ((MainWindow)System.Windows.Application.Current.MainWindow).MainLetter.Content = getRandomChar();
            letterDict = new Dictionary<string, string>
            {
                { "A", "alfa"},
                { "B", "bravo"},
                { "C", "charlie"},
                { "D", "delta"},
                { "E", "echo"},
                { "F", "foxtrot"},
                { "G", "golf"},
                { "H", "hotel"},
                { "I", "india"},
                { "J", "juliett"},
                { "K", "kilo"},
                { "L", "lima"},
                { "M", "mike"},
                { "N", "november"},
                { "O", "oscar"},
                { "P", "papa"},
                { "Q", "quebec"},
                { "R", "romeo"},
                { "S", "sierra"},
                { "T", "tango"},
                { "U", "uniform"},
                { "V", "victor"},
                { "W", "whiskey"},
                { "X", "x-ray"},
                { "Y", "yankee"},
                { "Z", "zulu"}
            };
        }

        public String getRandomChar()
        {
            String alphabetStr = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            char[] alphabetArr = alphabetStr.ToCharArray();

            Random rnd = new Random();

            String letter = alphabetArr[rnd.Next(0, alphabetArr.Length)].ToString();

            return letter;

        }

        public void initTimer()
        {
        
            int msPerTurn = getSecondsPerTurn() * 1000;
            gameTimer = new DispatcherTimer();
            gameTimer.Tick += new EventHandler(mainTimer);
            gameTimer.Interval = TimeSpan.FromMilliseconds(msPerTurn);

        }

        public void mainTimer(object source, EventArgs e)
        {
            ((MainWindow)System.Windows.Application.Current.MainWindow).MainLetter.Content = getRandomChar();
        }


        public void resetGame()
        {
            gameTimer.Stop();
            currentTurn = 0;
            numberIncorrect = 0;
            numberCorrect = 0;
            ((MainWindow)System.Windows.Application.Current.MainWindow).StartBtn.Visibility = Visibility.Visible;
            ((MainWindow)System.Windows.Application.Current.MainWindow).PhoneticsLabel.Visibility = Visibility.Visible;
            ((MainWindow)System.Windows.Application.Current.MainWindow).StopBtn.Visibility = Visibility.Hidden;
            ((MainWindow)System.Windows.Application.Current.MainWindow).MainLetter.Visibility = Visibility.Hidden;
            ((MainWindow)System.Windows.Application.Current.MainWindow).entryBox.IsEnabled = false;
        }

        public void mainLoop()
        {

            var entryField = ((MainWindow)System.Windows.Application.Current.MainWindow).entryBox;
            string currentLetter = (string)((MainWindow)System.Windows.Application.Current.MainWindow).MainLetter.Content;

            string letterKey = letterDict[currentLetter].ToLower();

            if (!string.IsNullOrWhiteSpace(entryField.Text))
            {
                String userInput = entryField.Text.Trim().ToLower();
                entryField.Text = String.Empty;
                ((MainWindow)System.Windows.Application.Current.MainWindow).MainLetter.Content = getRandomChar();

                //if correct
                if (userInput == letterKey)
                {
                    Debug.WriteLine(true);
                    //((MainWindow)System.Windows.Application.Current.MainWindow).MainLetter.Content = getRandomChar();

                    gameTimer.Stop();
                    gameTimer.Start();

                    currentTurn++;
                    numberCorrect++;

                } 
                //if incorrect                
                else if (userInput != letterKey)
                {
                    Debug.WriteLine(false);

                    gameTimer.Stop();
                    gameTimer.Start();

                    currentTurn++;
                    numberIncorrect++;
                }
            }

            if (currentTurn == getTurns()) 
            {
                Debug.WriteLine("Game has finished");
                Debug.WriteLine("Number correct: " + numberCorrect);
                Debug.WriteLine("Number incorrect: " + numberIncorrect);

                ((MainWindow)System.Windows.Application.Current.MainWindow).correctLbl.Content = "Correct: " + numberCorrect;
                ((MainWindow)System.Windows.Application.Current.MainWindow).incorrectLbl.Content = "Incorrect: " + numberIncorrect;
                ((MainWindow)System.Windows.Application.Current.MainWindow).correctLbl.Visibility = Visibility.Visible;
                ((MainWindow)System.Windows.Application.Current.MainWindow).incorrectLbl.Visibility = Visibility.Visible;

                resetGame();
            }
        }

        public void setTurns(int turns)
        {
            numberOfTurns = turns;
        }

        public void setSecondsPerTurn(int seconds)
        {
            secondsPerTurn = seconds;
        }

        public int getTurns()
        {
            return numberOfTurns;
        }

        public int getSecondsPerTurn()
        {
            return secondsPerTurn;
        }

        public DispatcherTimer gameTimer;
        int secondsPerTurn = 4;
        int numberOfTurns = 10;
        int currentTurn = 0;
        int numberCorrect = 0;
        int numberIncorrect = 0;
        public Dictionary<string, string> letterDict;
    }
}
