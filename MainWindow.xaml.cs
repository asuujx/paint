using System;
using System.Collections.Generic;
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
        Ellipse,
        Rectangle,
        Polygon,
        Path
    }

    public partial class MainWindow : Window
    {
        Point currentPoint = new Point();
        private Color penColor = Colors.Black;

        private DrawingMode currentMode = DrawingMode.Drawing;

        Point startPoint = new Point();
        private IList<Point> polygonVertices = new List<Point>();
        private IList<Point> pathVertices = new List<Point>(); 
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
            else if (currentMode == DrawingMode.Rectangle)
            {
                if (isFirstClick)
                {
                    startPoint = e.GetPosition(paintSurface);
                    isFirstClick = false;
                }
                else
                {
                    Point endPoint = e.GetPosition(paintSurface);
                    DrawRectangle(startPoint, endPoint);
                    isFirstClick = true;
                }
            }
            else if (currentMode == DrawingMode.Polygon)
            {
                if (isFirstClick)
                {
                    startPoint = e.GetPosition(paintSurface);
                    isFirstClick = false;
                }
                else
                {
                    Point vertex = e.GetPosition(paintSurface);
                    polygonVertices.Add(vertex);
                    DrawPolygon(startPoint, polygonVertices);
                    
                }
            }
            else if (currentMode == DrawingMode.Path)
            {
                if (isFirstClick)
                {
                    startPoint = e.GetPosition(paintSurface);
                    isFirstClick = false;
                }
                else
                {
                    Point vertex = e.GetPosition(paintSurface);
                    pathVertices.Add(vertex);
                    DrawPath(startPoint, pathVertices);
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
            isFirstClick = true;
            currentMode = DrawingMode.Line;
        }

        private void EllipseButton_Click(object sender, RoutedEventArgs e)
        {
            isFirstClick = true;
            currentMode = DrawingMode.Ellipse;
        }

        private void RectangleButton_Click(object sender, RoutedEventArgs e)
        {
            isFirstClick = true;
            currentMode = DrawingMode.Rectangle;
        }

        private void PolygonButton_Click(object sender, RoutedEventArgs e)
        {
            isFirstClick = true;
            polygonVertices.Clear();
            currentMode = DrawingMode.Polygon;
        }

        private void PathButton_Click(object sender, RoutedEventArgs e)
        {
            isFirstClick = true;
            pathVertices.Clear();
            currentMode = DrawingMode.Path;
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            paintSurface.Children.Clear();
        }

        private void ConvertButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string rgbText = RGBTextBox.Text;
                string[] rgbValues = rgbText.Split(',');

                if (rgbValues.Length != 3)
                {
                    MessageBox.Show("Invalid RGB values. Please enter three comma-separated values.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                int r = int.Parse(rgbValues[0]);
                int g = int.Parse(rgbValues[1]);
                int b = int.Parse(rgbValues[2]);

                double h, s, v;

                RGBtoHSV(r, g, b, out h, out s, out v);

                HSVTextBox.Text = $"H: {h:F2}, S: {s:F2}, V: {v:F2}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
                    case "RGB -> HSV":
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

        private void DrawRectangle(Point center, Point corner)
        {
            double width = Math.Abs(corner.X - center.X) * 2;
            double height = Math.Abs(corner.Y - center.Y) * 2;

            Rectangle rectangle = new Rectangle();
            rectangle.Stroke = Brushes.Black;
            rectangle.StrokeThickness = 2;
            rectangle.Width = width;
            rectangle.Height = height;

            Canvas.SetLeft(rectangle, center.X - width / 2);
            Canvas.SetTop(rectangle, center.Y - height / 2);

            paintSurface.Children.Add(rectangle);
        }

        private void DrawPolygon(Point center, IList<Point> vertices)
        {
            Polygon polygon = new Polygon();
            polygon.Stroke = Brushes.Black;
            polygon.StrokeThickness = 2;

            // Obliczamy współrzędne wierzchołków wielokąta
            PointCollection points = new PointCollection();
            foreach (Point vertex in vertices)
            {
                points.Add(new Point(center.X + (vertex.X - center.X), center.Y + (vertex.Y - center.Y)));
            }

            polygon.Points = points;
            paintSurface.Children.Add(polygon);
        }

        private void DrawPath(Point center, IList<Point> vertices) 
        {
            Path path = new Path();
            path.Stroke = Brushes.Black;
            path.StrokeThickness = 2;

            // Tworzymy obiekt PathGeometry
            PathGeometry pathGeometry = new PathGeometry();
            PathFigure pathFigure = new PathFigure();

            // Ustawiamy pierwszy punkt na środek
            pathFigure.StartPoint = center;

            // Dodajemy wierzchołki
            foreach (Point vertex in vertices)
            {
                pathFigure.Segments.Add(new LineSegment(vertex, true));
            }

            // Dodajemy figurę do geometrii
            pathGeometry.Figures.Add(pathFigure);

            // Ustawiamy geometrię na ścieżce
            path.Data = pathGeometry;

            paintSurface.Children.Add(path);
        }

        private static void RGBtoHSV(int r, int g, int b, out double h, out double s, out double v)
        {
            double min = Math.Min(Math.Min(r, g), b);
            double max = Math.Max(Math.Max(r, g), b);

            // Odcień (Hue)
            h = 0;
            if (max == r)
            {
                h = (60 * ((g - b) / (max - min)) + 360) % 360;
            }
            else if (max == g)
            {
                h = (60 * ((b - r) / (max - min)) + 120) % 360;
            }
            else if (max == b)
            {
                h = (60 * ((r - g) / (max - min)) + 240) % 360;
            }

            // Nasycenie (Saturation)
            s = (max == 0) ? 0 : (max - min) / max;

            // Jasność (Value)
            v = max / 255.0;
        }
    }
}
