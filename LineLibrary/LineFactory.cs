using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;

namespace OOP_4;

public class LineFactory : IShapeFactory
{
    public AbstractShape CreateShape(List<Point> listOfPoints, Brush color)
    {
        return new FigureLine(listOfPoints, color);
    }
}
