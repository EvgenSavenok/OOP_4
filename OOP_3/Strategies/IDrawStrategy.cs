using OOP_3.Figures;
using System.Windows.Shapes;

namespace OOP_3.Strategies;

public interface IDrawStrategy
{
    Shape DrawShape(AbstractShape abstractShape);
}
