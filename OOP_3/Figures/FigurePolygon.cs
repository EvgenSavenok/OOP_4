using OOP_3.Strategies;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;

namespace OOP_3.Figures;

public class FigurePolygon : AbstractShape
{
    private Canvas _canvas { get; }
    private List<Point> _listOfPoints { get; }
    private SolidColorBrush _color { get; }
    public FigurePolygon(Canvas canvas, List<Point> listOfPoints, SolidColorBrush color)
    {
        _listOfPoints = listOfPoints;
        _canvas = canvas;
        _color = color;
    }
    public List<Point> GetListOfPoints()
    {
        return _listOfPoints;
    }

    public Canvas GetCanvas()
    {
        return _canvas;
    }
    public SolidColorBrush GetColor()
    {
        return _color;
    }
    public override void Draw(AbstractShape polygon, IDrawStrategy polygonStrategy)
    {
        polygonStrategy.DrawShape(polygon);
    }
}
