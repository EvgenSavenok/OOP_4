using OOP_3.Strategies;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;

namespace OOP_3.Figures;

public class FigureLine : AbstractShape
{
    private Canvas _canvas { get; }
    private Point _startPoint { get; }
    private Point _endPoint { get; }
    private SolidColorBrush _color { get; }
    public FigureLine(Canvas canvas, Point startPoint, Point endPoint, SolidColorBrush color)
    {
        _startPoint = startPoint;
        _endPoint = endPoint;
        _canvas = canvas;
        _color = color;
    }
    public Point GetStartPoint()
    {
        return _startPoint;
    }

    public Point GetEndPoint()
    {
        return _endPoint;
    }

    public Canvas GetCanvas()
    {
        return _canvas;
    }
    public SolidColorBrush GetColor()
    {
        return _color;
    }
    public override void Draw(AbstractShape line, IDrawStrategy lineStrategy)
    {
        lineStrategy.DrawShape(line);
    }
}
