using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using OOP_3.Figures;

namespace OOP_3.Strategies;

public class EllipseDrawStrategy : IDrawStrategy
{
    public void DrawShape(AbstractShape abstractShape)
    {
        var ellipse = abstractShape as FigureEllipse;
        var canvas = ellipse.GetCanvas();
        var color = ellipse.GetColor();
        List<Point> listOfPoints = ellipse.GetListOfPoints();
        Point startPoint = listOfPoints[0];
        Point endPont = listOfPoints[1];
        Rect rect = new Rect(startPoint, endPont);
        EllipseGeometry ellipseGeometry = new EllipseGeometry(rect);
        Path path = new Path
        {
            Stroke = color,
            StrokeThickness = 1,
            Data = ellipseGeometry
        };
        path.Tag = canvas.Children.Count;
        canvas.Children.Add(path);
    }
}
