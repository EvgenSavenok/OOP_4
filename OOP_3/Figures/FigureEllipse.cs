using OOP_3.Strategies;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;

namespace OOP_3.Figures;

public class FigureEllipse : AbstractShape
{
    private Canvas Canvas { get; }
    private Point StartPoint { get; }
    private Point EndPoint { get; }
    private SolidColorBrush Color { get; }
    public FigureEllipse(Canvas canvas, Point startPoint, Point endPoint, SolidColorBrush color)
    {
        StartPoint = startPoint;
        EndPoint = endPoint;
        Canvas = canvas;
        Color = color;
    }
    public Point GetStartPoint()
    {
        return StartPoint;
    }

    public Point GetEndPoint()
    {
        return EndPoint;
    }

    public Canvas GetCanvas()
    {
        return Canvas;
    }
    public SolidColorBrush GetColor()
    {
        return Color;
    }
    public override void Draw(AbstractShape ellipse, IDrawStrategy ellipseStrategy)
    {
        ellipseStrategy.DrawShape(ellipse);
    }
}
