using System.Windows.Media;
using System.Windows.Shapes;
using OOP_3.Figures;
using System.Windows;

namespace OOP_3.Strategies;

public class LineDrawStrategy : IDrawStrategy
{
    public void DrawShape(AbstractShape abstractShape)
    {
        var line = abstractShape as FigureLine;
        var canvas = line.GetCanvas();
        var color = line.GetColor();
        List<Point> listOfPoints = line.GetListOfPoints();
        Point startPoint, endPont;
        startPoint = listOfPoints[0];
        endPont = listOfPoints[1];
        LineGeometry lineGeometry = new LineGeometry(startPoint, endPont);
        Path path = new Path
        {
            Stroke = color,
            StrokeThickness = 5,
            Data = lineGeometry,
            Tag = line.CanvasIndex
        };
        canvas.Children.Add(path);
    }
}
