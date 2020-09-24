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
using System.Timers;

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
        private Random rnd = new Random();
        private static int currentScore = 0;
        private const int maxScore = 21;
        private static int lives = 3;

        public MainWindow()
        {
            InitializeComponent();

            //GamePaddle = Paddle.getInstance(GameArea);
            PingPongBall = Ball.getInstance();

            gameTicker.Tick += GameTicker_Tick;
        }

        private void GameTicker_Tick(object sender, EventArgs e)
        {
            PingPongBall.Move(GamePaddle);
            PingPongBall.Draw(GameArea);
            DetectCollision();
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
                case Key.Space:
                    gameTicker.IsEnabled = false;
                    MessageBoxResult responseSpace = MessageBox.Show("Press OK to continue", "GAME PAUSED");
                    if (responseSpace == MessageBoxResult.OK)
                    {
                        gameTicker.IsEnabled = true;
                    }
                    break;
            }
        }

        private void Playground_ContentRendered(object sender, EventArgs e)
        {
            Background = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri(@"C:\Users\antoaneta\Downloads\CodeCool\advancedCSharp\1st_TW\c-sharp-pingpong-fireuponthedepth\Pong\Assets\Images\earthBakgr.jpg", UriKind.Absolute))
            };
            GamePaddle = Paddle.getInstance(GameArea);
            GamePaddle.Draw(GameArea);
            PingPongBall.Draw(GameArea);

            bdrWelcomePanel.Visibility = Visibility.Visible;
        }

        public void StartNewGame(double ballSpeedX, double ballSpeedY)
        {
            bdrWelcomePanel.Visibility = Visibility.Collapsed;

            PingPongBall.speedX = ballSpeedX;
            PingPongBall.speedY = ballSpeedY;

            gameTicker.Interval = TimeSpan.FromMilliseconds(30);
            gameTicker.IsEnabled = true;
        }

        public void DetectCollision()
        {
            // left or right
            if (PingPongBall.Position.X <= 0 || PingPongBall.Position.X + PingPongBall.Size >= GameArea.ActualWidth)
            {
                PingPongBall.speedX = -PingPongBall.speedX;

                PlaySoundRicoChet();
            }
            //top
            if (PingPongBall.Position.Y <= 0)
            {
                PingPongBall.speedY = -PingPongBall.speedY;

                PlaySoundRicoChet();
            }
            // paddle
            if (GamePaddle.Position.X <= PingPongBall.Position.X && PingPongBall.Position.X <= GamePaddle.Position.X + GamePaddle.Width
                && GamePaddle.Position.Y <= PingPongBall.Position.Y + PingPongBall.Size)
            {
                PingPongBall.speedY = -PingPongBall.speedY;

                PlaySoundRicoChet();

                // update score
                currentScore++;
                UpdateGameStatus();
            }
            // bottom 
            if (PingPongBall.Position.Y > GameArea.ActualHeight)
            {
                double x = rnd.NextDouble() * (GameArea.ActualWidth - PingPongBall.Size);
                double y = rnd.NextDouble() * (GameArea.ActualHeight / 2);
                PingPongBall.Position = new Point(x, y);

                PingPongBall.speedX = rnd.NextDouble() * 5 + 1;
                PingPongBall.speedY = rnd.NextDouble() * 5 + 1;

                PlaySoundSplash();

                lives--;
                this.tbStatusLives.Text = lives.ToString();
            }

            // check end game
            EndGame();
        }

        private void PlaySoundSplash()
        {
            Uri uri = new Uri(@"C:\Users\antoaneta\Downloads\CodeCool\advancedCSharp\1st_TW\c-sharp-pingpong-fireuponthedepth\Pong\Assets\Sounds\splash.mp3");
            var player = new MediaPlayer();
            player.Open(uri);
            player.Play();
        }

        private void PlaySoundRicoChet()
        {
            Uri uri = new Uri(@"C:\Users\antoaneta\Downloads\CodeCool\advancedCSharp\1st_TW\c-sharp-pingpong-fireuponthedepth\Pong\Assets\Sounds\laser.mp3");
            var player = new MediaPlayer();
            player.Open(uri);
            player.Play();
        }

        private void EndGame()
        {
            if (currentScore >= maxScore)
            {
                gameTicker.IsEnabled = false;
                MessageBoxResult responseEnd = MessageBox.Show($"Congratulations! Your you reached {currentScore} points!", "GAME FINISHED!!");
                this.Close();
            }
            else if (lives <= 0)
            {
                gameTicker.IsEnabled = false;
                MessageBoxResult responseEnd = MessageBox.Show($"No lives left! Your you reached {currentScore} points!", "GAME FINISHED!!");
                this.Close();
            }
        }

        private void UpdateGameStatus()
        {
            this.tbStatusScore.Text = currentScore.ToString();
            this.pbStatus.Value = (currentScore * 100) / maxScore;
        }

        private void basic_Click(object sender, RoutedEventArgs e)
        {
            // Start GAME
            StartNewGame(5, -5);
            this.tbStatusLevel.Text = "Basic";
        }

        private void intermediate_Click(object sender, RoutedEventArgs e)
        {
            // Start GAME
            StartNewGame(7, -7);
            this.tbStatusLevel.Text = "Intermediate";
        }

        private void hard_Click(object sender, RoutedEventArgs e)
        {
            // Start GAME
            StartNewGame(9, -9);
            this.tbStatusLevel.Text = "Hard";

            GamePaddle.PaddleSpeed += 15;

            // increase ball speed every 5 seconds
            Timer myTimer = new Timer();
            myTimer.Elapsed += new ElapsedEventHandler(DisplayTimeEvent);
            myTimer.Interval = 5000; // 1000 ms is one second
            myTimer.Start();
        }

        private void DisplayTimeEvent(object sender, ElapsedEventArgs e)
        {
            if (PingPongBall.speedX > 0)
            {
                PingPongBall.speedX += 1;
            } 
            else
            {
                PingPongBall.speedX -= 1;
            }

            if (PingPongBall.speedY > 0)
            {
                PingPongBall.speedY += 1;
            }
            else
            {
                PingPongBall.speedY -= 1;
            }
        }

        private void GameArea_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            GamePaddle = Paddle.getInstance(GameArea);
            double paddleX = (GameArea.ActualWidth / 2) - (GamePaddle.Width / 2);
            double paddleY = GameArea.ActualHeight - 10 - GamePaddle.Height;
            GamePaddle.Position = new Point(paddleX, paddleY);

            GamePaddle.Draw(GameArea);
        }
    }
}
