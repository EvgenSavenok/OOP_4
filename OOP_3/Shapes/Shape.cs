using OOP_3.Strategies;

namespace OOP_3.Shapes;

public abstract class Shape
{
    public abstract void Draw(Shape square, IDrawStrategy squareStrategy);
}
