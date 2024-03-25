using System.Windows;
using System.Windows.Media;
using OOP_3.Strategies;

namespace OOP_3.Figures;

public abstract class AbstractShape
{
    private List<Point> ListOfPoints { get; set; }
    public virtual object TagShape { get; }
    public int CanvasIndex { get; set; }
    public abstract void Draw(AbstractShape shape, IDrawStrategy lineStrategy);
}
