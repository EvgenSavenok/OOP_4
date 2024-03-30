using System.Windows;
using System.Windows.Media;
using OOP_3.Strategies;

namespace OOP_3.Figures;

[Serializable]
public class FigureEllipse : AbstractShape
{
    private List<Point> ListOfPoints { get; }
    public override int NumOfFactory => 1;

    public FigureEllipse(List<Point> listOfPoints, Brush color)
    : base(listOfPoints, color)
    {
        ListOfPoints = listOfPoints;
        Color = color;
        DrawStrategy = new EllipseDrawStrategy();
    }
}
