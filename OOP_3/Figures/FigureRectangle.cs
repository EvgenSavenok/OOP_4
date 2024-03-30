using OOP_3.Strategies;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace OOP_3.Figures;

[Serializable]
public class FigureRectangle : FigurePolygon
{
    private List<Point> ListOfPoints { get; }
    public override int NumOfFactory => 3;
    public FigureRectangle(List<Point> listOfPoints, Brush color) 
        : base(listOfPoints, color)
    {
        ListOfPoints = listOfPoints;
        Color = color;
        DrawStrategy = new RectangleDrawStrategy();
    }
}
