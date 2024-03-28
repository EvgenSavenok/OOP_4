using System.Windows.Media;
using System.Windows.Shapes;
using OOP_3.Figures;
using System.Windows;
using System.Windows.Controls;

namespace OOP_3.Strategies;
[Serializable]
public class PolygonDrawStrategy : IDrawStrategy
{
    public Shape DrawShape(AbstractShape abstractShape)
    {
        var polygon = abstractShape as FigurePolygon;
        List<Point> listOfPoints = polygon.ListOfPoints;
        var polygonFigure = new Polygon {StrokeThickness = 1, Fill = abstractShape.Color};
        foreach (var point in listOfPoints)
            polygonFigure.Points.Add(point);
        return polygonFigure;
    }
}
