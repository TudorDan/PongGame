using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using WPFCustomMessageBox;
using System.Windows.Shapes;
using System.Windows.Media.Imaging;

namespace Pong
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const int PaddleWidth = 150;
        const int PaddleHeight = 30;
        public class Paddle
        {
            private static Paddle instance = null;
            public Point position { get; set; }

            private Paddle() { }
            public static Paddle getInstance()
            {
                if (instance == null)
                {
                    instance = new Paddle();
                }
                return instance;
            }

            public void Draw(Canvas canvas)
            {
                Rectangle rect = new Rectangle
                {
                    Width = PaddleWidth,
                    Height = PaddleHeight,
                    Fill = new ImageBrush
                    {
                        ImageSource = new BitmapImage(new Uri(@"C:\Users\antoaneta\Downloads\CodeCool\advancedCSharp\1st_TW\c-sharp-pingpong-fireuponthedepth\Pong\paddle.png", UriKind.Absolute))
                    }
            };
                canvas.Children.Add(rect);
                Canvas.SetTop(rect, 380);
                Canvas.SetLeft(rect, 307);
            }
        }

        public MainWindow()
        {
            InitializeComponent();
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
            Paddle paddle = Paddle.getInstance();
            paddle.Draw(GameArea);
        }


        //private void playground_Loaded(object sender, RoutedEventArgs e)
        //{
        //    DispatcherTimer dispatcherTimer = new DispatcherTimer();
        //    dispatcherTimer.IsEnabled = true;
        //}
    }
}
