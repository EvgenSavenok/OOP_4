using System.Windows.Media;
using System.Windows.Shapes;
using OOP_3.Figures;

namespace OOP_3.Strategies;

public class LineDrawStrategy : IDrawStrategy
{
    public void DrawShape(AbstractShape abstractShape)
    {
        var line = abstractShape as FigureLine;
        var canvas = line.GetCanvas();
        var color = line.GetColor();
        LineGeometry lineGeometry = new LineGeometry(line.GetStartPoint(), line.GetEndPoint());
        Path path = new Path
        {
            Stroke = color,
            StrokeThickness = 2,
            Data = lineGeometry
        };
        canvas.Children.Add(path);
    }
}
