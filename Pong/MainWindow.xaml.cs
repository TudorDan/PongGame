using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using WPFCustomMessageBox;
using System.Windows.Shapes;
using System.Windows.Media.Imaging;
using System.Runtime.InteropServices;

namespace Pong
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Paddle GamePaddle { get; set; }
        public Ball PingPongBall { get; }

        public MainWindow()
        {
            InitializeComponent();

            GamePaddle = Paddle.getInstance();
            PingPongBall = Ball.getInstance();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                MessageBoxResult response = CustomMessageBox.ShowOKCancel("Are you sure to quit from the best game ever?", "Close confirmation", "Yes", "HellNO");
                if (response == MessageBoxResult.OK)
                {
                    this.Close();
                }
            }
        }

        private void playground_ContentRendered(object sender, EventArgs e)
        {
            Background = new SolidColorBrush(Colors.Gray);

            
            GamePaddle.Draw(GameArea);
            PingPongBall.Draw(GameArea);
        }

        private void playground_KeyUp(object sender, KeyEventArgs e)
        {
            switch(e.Key)
            {
                case Key.Left:
                    GamePaddle.Direction = PaddleDirection.Left;
                    GamePaddle.Move();
                    GamePaddle.Draw(GameArea);
                    break;
                case Key.Right:
                    GamePaddle.Direction = PaddleDirection.Right;
                    GamePaddle.Move();
                    GamePaddle.Draw(GameArea);
                    break;
            }
        }


        //private void playground_Loaded(object sender, RoutedEventArgs e)
        //{
        //    DispatcherTimer dispatcherTimer = new DispatcherTimer();
        //    dispatcherTimer.IsEnabled = true;
        //}
    }
}
