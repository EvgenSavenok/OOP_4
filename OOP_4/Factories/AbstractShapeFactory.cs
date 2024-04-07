using OOP_3.Figures;
using System.Windows;
using System.Windows.Media;

namespace OOP_3.Factories;

public abstract class AbstractShapeFactory
{
    public abstract AbstractShape CreateShape(List<Point> listOfPoints, Brush color);
}
