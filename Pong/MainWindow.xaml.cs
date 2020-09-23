using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using WPFCustomMessageBox;
using System.Windows.Shapes;
using System.Windows.Media.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Threading;

namespace Pong
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Paddle GamePaddle { get; set; }
        public Ball PingPongBall { get; }
        private DispatcherTimer gameTicker = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();

            GamePaddle = Paddle.getInstance(GameArea);
            PingPongBall = Ball.getInstance();

            gameTicker.Tick += GameTicker_Tick;
        }

        private void GameTicker_Tick(object sender, EventArgs e)
        {
            PingPongBall.Move();
            PingPongBall.Draw(GameArea);
            PingPongBall.DetectCollision(GameArea, GamePaddle);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                    GamePaddle.Direction = PaddleDirection.Left;
                    GamePaddle.Move(GameArea);
                    GamePaddle.Draw(GameArea);
                    break;
                case Key.Right:
                    GamePaddle.Direction = PaddleDirection.Right;
                    GamePaddle.Move(GameArea);
                    GamePaddle.Draw(GameArea);
                    break;
                case Key.Escape:
                    MessageBoxResult response = CustomMessageBox.ShowOKCancel("Are you sure to quit from the best game ever?", "Close confirmation", "Yes", "HellNO");
                    if (response == MessageBoxResult.OK)
                    {
                        this.Close();
                    }
                    break;
            }
        }

        private void playground_ContentRendered(object sender, EventArgs e)
        {
            Background = new SolidColorBrush(Colors.Gray);

            
            GamePaddle.Draw(GameArea);
            PingPongBall.Draw(GameArea);

            // Start GAME
            StartNewGame();
        }
        
        public void StartNewGame()
        {
            gameTicker.Interval = TimeSpan.FromMilliseconds(30);
            gameTicker.IsEnabled = true;
        }

        
        //private void playground_Loaded(object sender, RoutedEventArgs e)
        //{
        //    DispatcherTimer dispatcherTimer = new DispatcherTimer();
        //    dispatcherTimer.IsEnabled = true;
        //}
    }
}
