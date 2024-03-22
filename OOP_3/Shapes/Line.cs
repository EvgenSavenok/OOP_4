using OOP_3.Strategies;

namespace OOP_3.Shapes;

public class Line : Shape
{
    public override void Draw(Shape square, IDrawStrategy lineStrategy)
    {
        lineStrategy.DrawShape(square);
    }
}
