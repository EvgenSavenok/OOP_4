using OOP_3.Strategies;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Text;

namespace OOP_3.Figures;

[Serializable]
public class FigureLine : AbstractShape
{
    private List<Point> ListOfPoints { get; }
    private SolidColorBrush Color { get; set; }
    public FigureLine(List<Point> listOfPoints, SolidColorBrush color) 
        : base(listOfPoints, color)
    {
        ListOfPoints = listOfPoints;
        Color = color;
    }
    public List<Point> GetListOfPoints()
    {
        return ListOfPoints;
    }
    public SolidColorBrush GetColor()
    {
        return Color;
    }
    public override void Draw(AbstractShape line, IDrawStrategy lineStrategy, Canvas canvas)
    {
        Shape shape = lineStrategy.DrawShape(line);
        if (CanvasIndex < 0)
        {
            CanvasIndex = canvas.Children.Count;
            canvas.Children.Add(shape);
        }
        else
        {
            canvas.Children.RemoveAt(CanvasIndex);
            canvas.Children.Insert(CanvasIndex, shape);
        }
        shape.Tag = CanvasIndex;
    }
}
