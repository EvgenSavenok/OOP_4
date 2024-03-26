using System.Windows.Media;
using System.Windows.Shapes;
using OOP_3.Figures;
using System.Windows;

namespace OOP_3.Strategies;

public class RectangleDrawStrategy : IDrawStrategy
{
    public Shape DrawShape(AbstractShape abstractShape)
    {
        var rectangle = abstractShape as FigureRectangle;
        List<Point> listOfPoints = rectangle.ListOfPoints;
        Point startPoint = listOfPoints[0];
        Point endPont = listOfPoints[1];
        Rect rect = new Rect(startPoint, endPont);
        RectangleGeometry lineGeometry = new RectangleGeometry(rect);
        Path path = new Path
        {
            Stroke = rectangle.Color,
            StrokeThickness = 5,
            Data = lineGeometry,
        };
        return path;
    }
}
