using OOP_3.Strategies;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;

namespace OOP_3.Figures;

public class FigureEllipse : AbstractShape
{
    private Canvas Canvas { get; }
    private List<Point> _listOfPoints { get; }
    private SolidColorBrush Color { get; }
    public FigureEllipse(Canvas canvas, List<Point> listOfPoints, SolidColorBrush color)
    {
        _listOfPoints = listOfPoints;
        Canvas = canvas;
        Color = color;
    }

    public List<Point> GetListOfPoints()
    {
        return _listOfPoints;
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
