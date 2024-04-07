using System.Windows.Shapes;

namespace OOP_4;


public interface IDrawStrategy
{
    Shape DrawShape(AbstractShape abstractShape);
}
