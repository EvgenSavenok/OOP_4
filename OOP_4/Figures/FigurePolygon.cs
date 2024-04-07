using OOP_3.Strategies;
using System.Windows;
using System.Windows.Media;

namespace OOP_3.Figures;

[Serializable]
public class FigurePolygon : AbstractShape
{
    private List<Point> ListOfPoints { get; }
    public override int NumOfFactory => 2;
    public FigurePolygon(List<Point> listOfPoints, Brush color)
        : base(listOfPoints, color)
    {
        ListOfPoints = listOfPoints;
        Color = color;
        DrawStrategy = new PolygonDrawStrategy();
    }
}
