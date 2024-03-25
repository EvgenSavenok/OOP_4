using System.Windows.Media;
using System.Windows.Shapes;
using OOP_3.Figures;
using System.Windows;

namespace OOP_3.Strategies;

public class RectangleDrawStrategy : IDrawStrategy
{
    public void DrawShape(AbstractShape abstractShape)
    {
        var rectangle = abstractShape as FigureRectangle;
        var canvas = rectangle.GetCanvas();
        var color = rectangle.GetColor();
        List<Point> listOfPoints = rectangle.GetListOfPoints();
        Point startPoint = listOfPoints[0];
        Point endPont = listOfPoints[1];
        Rect rect = new Rect(startPoint, endPont);
        RectangleGeometry lineGeometry = new RectangleGeometry(rect);
        Path path = new Path
        {
            Stroke = color,
            StrokeThickness = 5,
            Data = lineGeometry,
            Tag = rectangle.CanvasIndex
        };
        canvas.Children.Add(path);
    }
}
