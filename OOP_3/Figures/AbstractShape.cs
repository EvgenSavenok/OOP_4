using System.Runtime.Serialization;
using System.Windows;
using OOP_3.Strategies;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace OOP_3.Figures;

[Serializable]
[XmlInclude(typeof(SolidColorBrush))]
[XmlInclude(typeof(MatrixTransform))]
public abstract class AbstractShape
{
    public List<Point> ListOfPoints { get; set; }
    public Brush Color;
    
    public abstract int NumOfFactory { get; }
    
    [Newtonsoft.Json.JsonIgnore]
    public IDrawStrategy DrawStrategy { get; protected set; }

    [Newtonsoft.Json.JsonIgnore]
    [NonSerialized]
    public int CanvasIndex = -1;
    public AbstractShape() {}

    protected AbstractShape(List<Point> listOfPoints, Brush color)
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
