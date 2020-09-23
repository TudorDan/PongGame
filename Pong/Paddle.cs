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
    public enum PaddleDirection { Left, Right };
    public class Paddle
    {
        private static Paddle instance = null;
        private static Point Position = new Point(380, 307);
        private int Width { get; } = 150;
        private int Height { get; } = 30;
        public PaddleDirection Direction { get; set; }


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
                    ImageSource = new BitmapImage(new Uri(@"C:\Users\antoaneta\Downloads\CodeCool\advancedCSharp\1st_TW\c-sharp-pingpong-fireuponthedepth\Pong\paddle.png", UriKind.Absolute))
                }
            };
            canvas.Children.Add(rect);
            Canvas.SetTop(rect, Position.X);
            Canvas.SetLeft(rect, Position.Y);
        }

        public void Move()
        {
            double nextY = Position.Y;
            switch (Direction)
            {
                case PaddleDirection.Left:
                    Position = new Point(380, nextY -= 10);
                    break;
                case PaddleDirection.Right:
                    Position = new Point(380, nextY += 10);
                    break;
            }
        }
    }
}
