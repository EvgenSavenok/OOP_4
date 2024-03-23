using OOP_3.Strategies;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;

namespace OOP_3.Figures;

public class FigurePolygon : AbstractShape
{
    private Canvas Canvas { get; }
    private List<Point> ListOfPoints { get; }
    private SolidColorBrush Color { get; }
    public FigurePolygon(Canvas canvas, List<Point> listOfPoints, SolidColorBrush color)
    {
        ListOfPoints = listOfPoints;
        Canvas = canvas;
        Color = color;
    }
    public List<Point> GetListOfPoints()
    {
        return ListOfPoints;
    }

    public Canvas GetCanvas()
    {
        return Canvas;
    }
    public SolidColorBrush GetColor()
    {
        return Color;
    }
    public override void Draw(AbstractShape circle, IDrawStrategy circleStrategy)
    {
        circleStrategy.DrawShape(circle);
    }
}
