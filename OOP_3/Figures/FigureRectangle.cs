using OOP_3.Strategies;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;

namespace OOP_3.Figures;

public class FigureRectangle : FigurePolygon
{
    private Canvas _canvas { get; }
    private List<Point> _listOfPoints { get; }
    private SolidColorBrush _color { get; }
    public FigureRectangle(Canvas canvas, List<Point> listOfPoints, SolidColorBrush color) : base(canvas, listOfPoints, color)
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
    public override void Draw(AbstractShape rectangle, IDrawStrategy rectangleStrategy)
    {
        rectangleStrategy.DrawShape(rectangle);
    }
}
