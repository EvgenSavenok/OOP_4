using OOP_3.Strategies;

namespace OOP_3.Figures;

public abstract class AbstractShape
{
    public abstract void Draw(AbstractShape circle, IDrawStrategy circleStrategy);
}
