using System.Text.Json.Serialization;
using System.Windows;
using System.Windows.Media;
using OOP_3.Strategies;
using System.Windows.Controls;
using Newtonsoft.Json;

namespace OOP_3.Figures;

[Serializable]
public abstract class AbstractShape
{
    public List<Point> ListOfPoints { get; set; }
    public SolidColorBrush Color { get; set; }
    
    [Newtonsoft.Json.JsonIgnore]
    public int CanvasIndex { get; set; } = -1;
    public abstract void Draw(AbstractShape polygon, IDrawStrategy lineStrategy, Canvas canvas);

    protected AbstractShape(List<Point> listOfPoints, SolidColorBrush color)
    {
        Color = color;
        ListOfPoints = listOfPoints;
    }
}
