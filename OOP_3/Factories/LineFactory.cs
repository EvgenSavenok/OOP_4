using OOP_3.Shapes;

namespace OOP_3.Factories;

public class LineFactory : IShapeFactory
{
    public Shape CreateShape()
    {
        return new Line();
    }
}
