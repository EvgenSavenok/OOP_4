using System.Windows;
using System.Windows.Media;
using OOP_3.Strategies;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace OOP_3.Figures;

[Serializable]
public abstract class AbstractShape
{
    public List<Point> ListOfPoints { get; set; }
    public SolidColorBrush Color { get; set; }
    
    [Newtonsoft.Json.JsonIgnore]
    public IDrawStrategy DrawStrategy { get; protected set; }

    [Newtonsoft.Json.JsonIgnore]
    [NonSerialized]
    public int CanvasIndex = -1;

    protected AbstractShape(List<Point> listOfPoints, SolidColorBrush color)
    {
        Color = color;
        ListOfPoints = listOfPoints;
    }

    public void Draw(Canvas canvas)
    {
        Shape shape = DrawStrategy.DrawShape(this);
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
