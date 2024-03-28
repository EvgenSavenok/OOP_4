using System.Windows;
using System.Windows.Media;
using OOP_3.Strategies;

namespace OOP_3.Figures;

[Serializable]
public class FigureEllipse : AbstractShape
{
    private List<Point> ListOfPoints { get; }
    public FigureEllipse(List<Point> listOfPoints, SolidColorBrush color)
    : base(listOfPoints, color)
    {
        ListOfPoints = listOfPoints;
        Color = color;
        DrawStrategy = new EllipseDrawStrategy();
    }
}
