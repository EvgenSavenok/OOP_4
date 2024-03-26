﻿using OOP_3.Figures;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;

namespace OOP_3.Factories;

public class EllipseFactory : IShapeFactory
{
    public AbstractShape CreateShape(List<Point> listOfPoints, SolidColorBrush color)
    {
        return new FigureEllipse(listOfPoints, color);
    }
}
