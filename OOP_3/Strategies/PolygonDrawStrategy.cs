using System.Windows.Media;
using System.Windows.Shapes;
using OOP_3.Figures;
using System.Windows;

namespace OOP_3.Strategies;

public class PolygonDrawStrategy : IDrawStrategy
{
    public void DrawShape(AbstractShape abstractShape)
    {
        var polygon = abstractShape as FigurePolygon;
        var canvas = polygon.GetCanvas();
        var color = polygon.GetColor();
        List<Point> listOfPoints = polygon.GetListOfPoints();
        /*Point startPoint = listOfPoints[0];
        Point endPont = listOfPoints[1];
        LineGeometry lineGeometry = new LineGeometry(startPoint, endPont);
        Path path = new Path
        {
            Stroke = color,
            StrokeThickness = 1,
            Data = lineGeometry
        };
        canvas.Children.Add(path);*/
    }
}
