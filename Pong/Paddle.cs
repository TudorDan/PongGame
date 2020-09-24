using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Pong
{
    public enum PaddleDirection { Left, Right };
    public class Paddle
    {
        public double PaddleSpeed { get; set; } = 15;
        private static Paddle instance = null;
        public Point Position { get; set; }
        public double Width { get; } = 150;
        public double Height { get; } = 60;
        public PaddleDirection Direction { get; set; }


        private Paddle(Canvas canvas) 
        {
            double paddleX = (canvas.ActualWidth / 2) - (Width / 2);
            double paddleY = canvas.ActualHeight - 10 - Height;
            Position = new Point(paddleX, paddleY);
        }
        public static Paddle getInstance(Canvas canvas)
        {
            if (instance == null)
            {
                instance = new Paddle(canvas);
            }
            return instance;
        }

        public void Draw(Canvas canvas)
        {
            var previusPaddleImage = canvas.Children?.OfType<Rectangle>()?.Any(c => c.Name == "paddle");
            if (previusPaddleImage.Value)
            {
                var foundPreviousImage = canvas.Children.OfType<Rectangle>().Single(c => c.Name == "paddle");
                canvas.Children.Remove(foundPreviousImage);
            }
            Rectangle rect = new Rectangle
            {
                Width = this.Width,
                Height = this.Height,
                Name = "paddle",
                Fill = new ImageBrush
                {
                    ImageSource = new BitmapImage(new Uri(@"C:\Users\antoaneta\Downloads\CodeCool\advancedCSharp\1st_TW\c-sharp-pingpong-fireuponthedepth\Pong\Assets\Images\spaceship.png", UriKind.Absolute))
                }
            };
            canvas.Children.Add(rect);
            Canvas.SetTop(rect, Position.Y);
            Canvas.SetLeft(rect, Position.X);
        }

        public void Move(Canvas canvas)
        {
            double nextX = Position.X;
            double canvasY = canvas.ActualHeight;
            switch (Direction)
            {
                case PaddleDirection.Left:
                    Position = new Point(nextX -= PaddleSpeed, canvasY - PaddleSpeed - Height);
                    PlaySound();
                    break;
                case PaddleDirection.Right:
                    Position = new Point(nextX += PaddleSpeed, canvasY - PaddleSpeed - Height);
                    PlaySound();
                    break;
            }
        }

        private void PlaySound()
        {
            Uri uri = new Uri(@"C:\Users\antoaneta\Downloads\CodeCool\advancedCSharp\1st_TW\c-sharp-pingpong-fireuponthedepth\Pong\Assets\Sounds\Ship.mp3");
            var player = new MediaPlayer();
            player.Open(uri);
            player.Play();
        }
    }
}
