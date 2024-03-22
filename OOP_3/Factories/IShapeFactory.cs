﻿using OOP_3.Figures;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;

namespace OOP_3.Factories;

public interface IShapeFactory
{
    AbstractShape CreateShape(Canvas canvas, Point startPoint, Point endPoint, SolidColorBrush color);
}