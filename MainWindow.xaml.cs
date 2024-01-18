using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace paint
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
        
    public partial class MainWindow : Window
    {
        Point currentPoint = new Point();
        private bool isEraserMode = false;
        private Color penColor = Colors.Black;

        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void Canvas_MouseDown_1(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
            currentPoint = e.GetPosition(paintSurface);
        }

        private void Canvas_MouseMove_1(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (isEraserMode)
                {
                    Ellipse eraser = new Ellipse();
                    eraser.Fill = Brushes.White;
                    eraser.Width = 20; // Set the size of your eraser
                    eraser.Height = 20;
                    eraser.Margin = new Thickness(e.GetPosition(paintSurface).X - 10, e.GetPosition(paintSurface).Y - 10, 0, 0);
                    paintSurface.Children.Add(eraser);
                }
                else
                {
                    Line line = new Line();
                    line.Stroke = new SolidColorBrush(penColor);
                    line.X1 = currentPoint.X;
                    line.Y1 = currentPoint.Y;
                    line.X2 = e.GetPosition(paintSurface).X;
                    line.Y2 = e.GetPosition(paintSurface).Y;
                    currentPoint = e.GetPosition(paintSurface);
                    paintSurface.Children.Add(line);
                    }
                }
            }
        
        private void EraserButton_Click(object sender, RoutedEventArgs e)
        {
            isEraserMode = !isEraserMode;
            if (isEraserMode)
            {
                eraserButton.Content = "Pen";
            }
            else
            {
                eraserButton.Content = "Eraser";
            }
        }

        private void FillButton_Click(object sender, RoutedEventArgs e)
        {
            FillCanvas(penColor); // You can set your desired fill color
        }

        private void FillCanvas(Color color)
        {
            paintSurface.Children.Clear();
            Rectangle backgroundRect = new Rectangle
            {
                Width = paintSurface.ActualWidth,
                Height = paintSurface.ActualHeight,
                Fill = new SolidColorBrush(color)
            };
            paintSurface.Children.Add(backgroundRect);
        }
        
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            paintSurface.Children.Clear();
        }
        
        private void ColorRedButton_Click(object sender, RoutedEventArgs e)
        {
            penColor = Colors.Red;
        }
        
        private void ColorGreenButton_Click(object sender, RoutedEventArgs e)
        {
            penColor = Colors.Green;
        }
        
        private void ColorBlueButton_Click(object sender, RoutedEventArgs e)
        {
            penColor = Colors.Blue;
        }
        
        private void ColorBlackButton_Click(object sender, RoutedEventArgs e)
        {
            penColor = Colors.Black;
        }

    }
}
