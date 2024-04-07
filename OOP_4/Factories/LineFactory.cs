using OOP_3.Figures;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;

namespace OOP_3.Factories;

public class LineFactory : AbstractShapeFactory
{
    public override AbstractShape CreateShape(List<Point> listOfPoints, Brush color)
    {
        return new FigureLine(listOfPoints, color);
    }
}
