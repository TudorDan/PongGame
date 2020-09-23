using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Pong
{
    public class Ball
    {
        private double speedX = 5;
        private double speedY = -5;
        private static Ball instance = null;
        public Point Position { get; set; } = new Point(180, 107);
        private double Size { get; } = 40;
        private Random rnd = new Random();

        private Ball() { }
        public static Ball getInstance()
        {
            if (instance == null)
            {
                instance = new Ball();
            }
            return instance;
        }

        public void Draw(Canvas canvas)
        {
            var previusBallImage = canvas.Children?.OfType<Rectangle>()?.Any(c => c.Name == "ball");
            if (previusBallImage.Value)
            {
                var foundPreviousImage = canvas.Children.OfType<Rectangle>().Single(c => c.Name == "ball");
                canvas.Children.Remove(foundPreviousImage);
            }
            Rectangle rect = new Rectangle
            {
                Width = Size,
                Height = Size,
                Name = "ball",
                Fill = new ImageBrush
                {
                    ImageSource = new BitmapImage(new Uri(@"C:\Users\antoaneta\Downloads\CodeCool\advancedCSharp\1st_TW\c-sharp-pingpong-fireuponthedepth\Pong\ball.png", UriKind.Absolute))
                }
            };
            canvas.Children.Add(rect);
            Canvas.SetTop(rect, Position.Y);
            Canvas.SetLeft(rect, Position.X);
        }

        public void Move()
        {
            double nextX = Position.X;
            double nextY = Position.Y;
            Position = new Point(nextX += speedX, nextY += speedY);
        }

        public void DetectCollision(Canvas canvas, Paddle paddle)
        {
            // left or right
            if (Position.X <= 0 || Position.X + Size >= canvas.ActualWidth)
            {
                speedX = -speedX;
            }
            //top
            if (Position.Y <= 0)
            {
                speedY = -speedY;
            }
            // paddle
            if (paddle.Position.X <= Position.X && Position.X <= paddle.Position.X + paddle.Width 
                && paddle.Position.Y <= Position.Y + Size)
            {
                speedY = -speedY;
            }
            // bottom 
            if (Position.Y > canvas.ActualHeight)
            {
                double x = rnd.NextDouble() * (canvas.ActualWidth - Size);
                double y = rnd.NextDouble() * (canvas.ActualHeight / 2);
                Position = new Point(x, y);

                speedX = rnd.NextDouble() * 5 + 1;
                speedY = rnd.NextDouble() * 5 + 1;
            }
        }
    }
}
