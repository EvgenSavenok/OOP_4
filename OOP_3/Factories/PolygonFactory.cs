using OOP_3.Figures;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;

namespace OOP_3.Factories;

public class PolygonFactory : IShapeFactory
{
    public AbstractShape CreateShape(Canvas canvas, List<Point> listOfPoints, SolidColorBrush color)
    {
        return new FigurePolygon(canvas, listOfPoints, color);
    }
}
