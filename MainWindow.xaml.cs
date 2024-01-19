using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace paint
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public enum DrawingMode
    {
        Drawing,
        Line,
        Ellipse
    }

    public partial class MainWindow : Window
    {
        Point currentPoint = new Point();
        private Color penColor = Colors.Black;

        private DrawingMode currentMode = DrawingMode.Drawing;

        Point startPoint = new Point();
        private bool isFirstClick = true;

        public MainWindow()
        {
            InitializeComponent();
            colorComboBox.SelectedIndex = 0;
        }

        private void Canvas_MouseDown_1(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (currentMode == DrawingMode.Line)
            {
                if (isFirstClick)
                {
                    // Pierwsze kliknięcie - ustaw punkt początkowy
                    startPoint = e.GetPosition(paintSurface);
                    isFirstClick = false;
                }
                else
                {
                    // Drugie kliknięcie - rysuj linię i zresetuj flagę isFirstClick
                    Point endPoint = e.GetPosition(paintSurface);
                    DrawLine(startPoint, endPoint);
                    isFirstClick = true;
                }
            }
            else if (currentMode == DrawingMode.Drawing)
            {
                if (e.ButtonState == MouseButtonState.Pressed)
                    currentPoint = e.GetPosition(paintSurface);
            }
            else if (currentMode == DrawingMode.Ellipse)
            {
                if (isFirstClick)
                {
                    startPoint = e.GetPosition(paintSurface);
                    isFirstClick = false;
                }
                else
                {
                    Point endPoint = e.GetPosition(paintSurface);
                    DrawEllipse(startPoint, endPoint);
                    isFirstClick = true;
                }   
            }
        }

        private void Canvas_MouseMove_1(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (currentMode == DrawingMode.Drawing && e.LeftButton == MouseButtonState.Pressed)
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

        private void PenButton_Click(object sender, RoutedEventArgs e)
        {
            currentMode = DrawingMode.Drawing;
        }

        private void FillButton_Click(object sender, RoutedEventArgs e)
        {
            FillCanvas(penColor); // You can set your desired fill color
        }

        private void LineButton_Click(Object sender, RoutedEventArgs e)
        {
            currentMode = DrawingMode.Line;
        }

        private void EllipseButton_Click(object sender, RoutedEventArgs e)
        {
            currentMode = DrawingMode.Ellipse;
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            paintSurface.Children.Clear();
        }

        private void ColorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem selectedItem = (ComboBoxItem)colorComboBox.SelectedItem;

            if (selectedItem != null)
            {
                string colorName = selectedItem.Content.ToString();

                // Ustaw kolor pióra na podstawie wyboru użytkownika
                switch (colorName)
                {
                    case "Black":
                        penColor = Colors.Black;
                        break;
                    case "Red":
                        penColor = Colors.Red;
                        break;
                    case "Blue":
                        penColor = Colors.Blue;
                        break;
                    case "Eraser":
                        penColor = Colors.White;
                        break;
                    default:
                        break;
                }
            }
        }

        private void FillCanvas(System.Windows.Media.Color color)
        {
            paintSurface.Children.Clear();
            System.Windows.Shapes.Rectangle backgroundRect = new System.Windows.Shapes.Rectangle
            {
                Width = paintSurface.ActualWidth,
                Height = paintSurface.ActualHeight,
                Fill = new SolidColorBrush(color)
            };
            paintSurface.Children.Add(backgroundRect);
        }

        private void DrawLine(Point start, Point end)
        {
            Line line = new Line();
            line.Stroke = Brushes.Black;
            line.StrokeThickness = 2;
            line.X1 = start.X;
            line.Y1 = start.Y;
            line.X2 = end.X;
            line.Y2 = end.Y;

            paintSurface.Children.Add(line);
        }

        private void DrawEllipse(Point center, Point end)
        {
            double radiusX = Math.Abs(end.X - center.X);
            double radiusY = Math.Abs(end.Y - center.Y);

            Ellipse ellipse = new Ellipse();
            ellipse.Stroke = Brushes.Black;
            ellipse.StrokeThickness = 2;
            ellipse.Width = 2 * radiusX;
            ellipse.Height = 2 * radiusY;

            Canvas.SetLeft(ellipse, center.X - radiusX);
            Canvas.SetTop(ellipse, center.Y - radiusY);

            paintSurface.Children.Add(ellipse);
        }
    }
}
