using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace Phonetics
{

    public partial class MainWindow : Window
    {   
        //generic list of turn buttons
        List<Button> turnList;
        //generic list of seconds buttons
        List<Button> secList;
        public MainWindow()
        {
            InitializeComponent();
            //add buttons to list
            turnList = new List<Button>
            {
                numberOfTurns1, numberOfTurns2, numberOfTurns3
            };

            secList = new List<Button>
            {
                lengthPeriod1, lengthPeriod2, lengthPeriod3
            };
        }


        private void StartBtn_Click(object sender, RoutedEventArgs e)
        {
            ((App)Application.Current).initGame();
            entryBox.Focus();
            PhoneticsLabel.Visibility = Visibility.Hidden;   
            StartBtn.Visibility = Visibility.Hidden;
            StopBtn.Visibility = Visibility.Visible;
            MainLetter.Visibility = Visibility.Visible;
            correctLbl.Visibility = Visibility.Hidden;
            incorrectLbl.Visibility = Visibility.Hidden;
        }

        private void StopBtn_Click(object sender, RoutedEventArgs e)
        {
            ((App)Application.Current).resetGame();
        }

        private void SettingsBtn_Click(object sender, RoutedEventArgs e)
        {
            PhoneticsLabel.Visibility = Visibility.Hidden;
            entryBox.Visibility = Visibility.Hidden;
            entryRect.Visibility = Visibility.Hidden;
            StartBtn.Visibility = Visibility.Hidden;

            secondsLabel.Visibility = Visibility.Visible;
            turnsLabel.Visibility = Visibility.Visible;

            lengthPeriod1.Visibility = Visibility.Visible;
            lengthPeriod2.Visibility = Visibility.Visible;
            lengthPeriod3.Visibility = Visibility.Visible;

            numberOfTurns1.Visibility = Visibility.Visible;
            numberOfTurns2.Visibility = Visibility.Visible;
            numberOfTurns3.Visibility = Visibility.Visible;

            saveBtn.Visibility = Visibility.Visible;
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            PhoneticsLabel.Visibility = Visibility.Visible;
            entryBox.Visibility = Visibility.Visible;
            entryRect.Visibility = Visibility.Visible;
            StartBtn.Visibility = Visibility.Visible;

            secondsLabel.Visibility = Visibility.Hidden;
            turnsLabel.Visibility = Visibility.Hidden;

            lengthPeriod1.Visibility = Visibility.Hidden;
            lengthPeriod2.Visibility = Visibility.Hidden;
            lengthPeriod3.Visibility = Visibility.Hidden;

            numberOfTurns1.Visibility = Visibility.Hidden;
            numberOfTurns2.Visibility = Visibility.Hidden;
            numberOfTurns3.Visibility = Visibility.Hidden;

            saveBtn.Visibility = Visibility.Hidden;
        }

        private void entryBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                ((App)Application.Current).mainLoop();
            }
        }

        private void numberOfTurns1_Click(object sender, RoutedEventArgs e)
        {
            ((App)Application.Current).setTurns(10);
            setColor(numberOfTurns1, 255, 255, 255);
        }

        private void numberOfTurns2_Click(object sender, RoutedEventArgs e)
        {
            ((App)Application.Current).setTurns(25);
            setColor(numberOfTurns2, 255, 255, 255);
        }

        private void numberOfTurns3_Click(object sender, RoutedEventArgs e)
        {
            ((App)Application.Current).setTurns(50);
            setColor(numberOfTurns3, 255, 255, 255);
        }

        private void lengthPeriod1_Click(object sender, RoutedEventArgs e)
        {
            ((App)Application.Current).setSecondsPerTurn(2);
            setColor(lengthPeriod1, 255, 255, 255);
        }

        private void lengthPeriod2_Click(object sender, RoutedEventArgs e)
        {
            ((App)Application.Current).setSecondsPerTurn(4);
            setColor(lengthPeriod2, 255, 255, 255);
        }

        private void lengthPeriod3_Click(object sender, RoutedEventArgs e)
        {
            ((App)Application.Current).setSecondsPerTurn(6);
            setColor(lengthPeriod3, 255, 255, 255);
        }

        //set color of clicked button to white and set color of rest to default 
        private void setColor(Button clickedBtn, byte r, byte g, byte b)
        {

            //if given button is lengthperiod button then change rest length period to default colours
            //else if turn button then change rest of turn buttons to default colours
            if (clickedBtn.Name.StartsWith("l")){
                foreach (var button in secList)
                {
                    button.Background = new SolidColorBrush(Color.FromArgb(255, 220, 220, 220));
                }
            } else
            {
                foreach (var button in turnList)
                {
                    button.Background = new SolidColorBrush(Color.FromArgb(255, 220, 220, 220));
                }
            }
            
            clickedBtn.Background = new SolidColorBrush(Color.FromArgb(255, r, g, b));
        }
    }
}
