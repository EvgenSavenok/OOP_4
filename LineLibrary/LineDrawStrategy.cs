using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;

namespace OOP_4;
[Serializable]
public class LineDrawStrategy : IDrawStrategy
{
    public Shape DrawShape(AbstractShape abstractShape)
    {
        List<Point> listOfPoints = abstractShape.ListOfPoints;
        Point startPoint, endPont;
        startPoint = listOfPoints[0];
        endPont = listOfPoints[1];
        LineGeometry lineGeometry = new LineGeometry(startPoint, endPont);
        Path path = new Path
        {
            Stroke = abstractShape.Color,
            StrokeThickness = 5,
            Data = lineGeometry,
        };
        return path;
    }
}
