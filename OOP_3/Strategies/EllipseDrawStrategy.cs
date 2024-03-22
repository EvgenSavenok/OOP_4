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
        Rect rect = new Rect(ellipse.GetStartPoint(), ellipse.GetEndPoint());
        EllipseGeometry ellipseGeometry = new EllipseGeometry(rect);
        Path path = new Path
        {
            Stroke = color,
            StrokeThickness = 2,
            Data = ellipseGeometry
        };
        canvas.Children.Add(path);
    }
}
