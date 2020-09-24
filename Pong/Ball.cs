using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Pong
{
    public class Ball
    {
        public double speedX = 5;
        public double speedY = -5;
        private static Ball instance = null;
        public Point Position { get; set; } = new Point(180, 107);
        public double Size { get; } = 40;

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
                    ImageSource = new BitmapImage(new Uri(@"C:\Users\antoaneta\Downloads\CodeCool\advancedCSharp\1st_TW\c-sharp-pingpong-fireuponthedepth\Pong\Assets\Images\ball.png", UriKind.Absolute))
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

        
    }
}
