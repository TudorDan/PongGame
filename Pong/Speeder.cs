using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Pong
{
    class Speeder
    {
        public double speedX;
        public double speedY;
        public double Size { get; } = 40;
        public Point Position { get; set; }
        private Random rnd = new Random();

        public Speeder(Canvas canvas)
        {
            speedX = 3;
            speedY = -3;
            double X = rnd.Next(1, (int)(canvas.ActualWidth - Size));
            double Y = rnd.Next(1, (int)(canvas.ActualHeight - Size));
            Position = new Point(X, Y);
        }
    }
}
