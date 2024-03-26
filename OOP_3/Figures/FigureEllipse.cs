using OOP_3.Strategies;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace OOP_3.Figures;

[Serializable]
public class FigureEllipse : AbstractShape
{
    private List<Point> ListOfPoints { get; }
    private SolidColorBrush Color { get; set; }
    public FigureEllipse(List<Point> listOfPoints, SolidColorBrush color)
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
    public override void Draw(AbstractShape ellipse, IDrawStrategy ellipseStrategy, Canvas canvas)
    {
        Shape shape = ellipseStrategy.DrawShape(ellipse);
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
