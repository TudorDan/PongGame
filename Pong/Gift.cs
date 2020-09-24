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
using System.Windows.Threading;

namespace Pong
{
    public class Gift
    {
        public double speedY;
        public double Size { get; } = 40;
        public Point Position { get; set; }
        private Random rnd = new Random();
        public GiftType Type;

        public Gift(Canvas canvas)
        {
            speedY = rnd.NextDouble() * 5 + 1;
            double X = rnd.Next(1, (int)(canvas.ActualWidth - Size));
            double Y = rnd.Next(1, (int)(canvas.ActualHeight - Size) / 2);
            Position = new Point(X, Y);
            Type = (GiftType)rnd.Next(0, 4);
        }

        public void Draw(Canvas canvas)
        {
            var previusGiftImage = canvas.Children?.OfType<Rectangle>()?.Any(c => c.Name == "gift");
            if (previusGiftImage.Value)
            {
                var foundPreviousImage = canvas.Children.OfType<Rectangle>().Single(c => c.Name == "gift");
                canvas.Children.Remove(foundPreviousImage);
            }

            ImageSource image = null;
            switch (Type)
            {
                case (GiftType.Speeder):
                    image = new BitmapImage(new Uri(@"C:\Users\antoaneta\Downloads\CodeCool\advancedCSharp\1st_TW\c-sharp-pingpong-fireuponthedepth\Pong\Assets\Images\Meterite.png", UriKind.Absolute));
                    break;
                case (GiftType.Extender):
                    image = new BitmapImage(new Uri(@"C:\Users\antoaneta\Downloads\CodeCool\advancedCSharp\1st_TW\c-sharp-pingpong-fireuponthedepth\Pong\Assets\Images\Satellite.png", UriKind.Absolute));
                    break;
                case (GiftType.Shortener):
                    image = new BitmapImage(new Uri(@"C:\Users\antoaneta\Downloads\CodeCool\advancedCSharp\1st_TW\c-sharp-pingpong-fireuponthedepth\Pong\Assets\Images\NeutronStar.png", UriKind.Absolute));
                    break;
                case (GiftType.Slower):
                    image = new BitmapImage(new Uri(@"C:\Users\antoaneta\Downloads\CodeCool\advancedCSharp\1st_TW\c-sharp-pingpong-fireuponthedepth\Pong\Assets\Images\BlackHole.png", UriKind.Absolute));
                    break;
            }
            Rectangle rect = new Rectangle
            {
                Width = Size,
                Height = Size,
                Name = "gift",
                Fill = new ImageBrush
                {
                    ImageSource = image
                }
            };
            canvas.Children.Add(rect);
            Canvas.SetTop(rect, Position.Y);
            Canvas.SetLeft(rect, Position.X);
        }

        public void Move()
        {
            double nextY = Position.Y;
            Position = new Point(Position.X, nextY + speedY);
        }
    }
}
