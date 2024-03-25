using OOP_3.Strategies;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Text;

namespace OOP_3.Figures;

[Serializable]
public class FigureLine : AbstractShape
{
    private Canvas Canvas { get; }
    private List<Point> ListOfPoints { get; }
    private SolidColorBrush Color { get; set; }
    public override object TagShape => "0";
    public FigureLine(Canvas canvas, List<Point> listOfPoints, SolidColorBrush color)
    {
        ListOfPoints = listOfPoints;
        Canvas = canvas;
        Color = color;
    }
    public List<Point> GetListOfPoints()
    {
        return ListOfPoints;
    }

    public Canvas GetCanvas()
    {
        return Canvas;
    }
    public SolidColorBrush GetColor()
    {
        return Color;
    }
    public override void Draw(AbstractShape line, IDrawStrategy lineStrategy)
    {
        lineStrategy.DrawShape(line);
    }
}
