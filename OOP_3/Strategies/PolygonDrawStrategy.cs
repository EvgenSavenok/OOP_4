using System.Windows.Media;
using System.Windows.Shapes;
using OOP_3.Figures;
using System.Windows;

namespace OOP_3.Strategies;

public class PolygonDrawStrategy : IDrawStrategy
{
    public void DrawShape(AbstractShape abstractShape)
    {
        var polygon = abstractShape as FigurePolygon;
        var canvas = polygon.GetCanvas();
        var color = polygon.GetColor();
        List<Point> listOfPoints = polygon.GetListOfPoints();
        var polygonFigure = new Polygon {StrokeThickness = 1, Fill = color};
        foreach (var point in listOfPoints)
        {
            polygonFigure.Points.Add(point);
        }
        polygonFigure.Tag = polygon.CanvasIndex;
        canvas.Children.Add(polygonFigure);
    }
}
