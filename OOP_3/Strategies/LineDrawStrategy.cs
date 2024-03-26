using System.Formats.Asn1;
using System.Windows.Media;
using System.Windows.Shapes;
using OOP_3.Figures;
using System.Windows;
using System.Windows.Controls;

namespace OOP_3.Strategies;

public class LineDrawStrategy : IDrawStrategy
{
    public Shape DrawShape(AbstractShape abstractShape)
    {
        var line = abstractShape as FigureLine;
        List<Point> listOfPoints = line.ListOfPoints;
        Point startPoint, endPont;
        startPoint = listOfPoints[0];
        endPont = listOfPoints[1];
        LineGeometry lineGeometry = new LineGeometry(startPoint, endPont);
        Path path = new Path
        {
            Stroke = line.Color,
            StrokeThickness = 5,
            Data = lineGeometry,
        };
        return path;
    }
}
