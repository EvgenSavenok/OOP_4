﻿using OOP_3.Figures;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;

namespace OOP_3.Factories;

public class LineFactory : IShapeFactory
{
    public AbstractShape CreateShape(Canvas canvas, Point startPoint, Point endPoint, SolidColorBrush color)
    {
        return new FigureLine(canvas, startPoint, endPoint, color);
    }
}