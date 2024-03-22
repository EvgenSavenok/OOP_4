using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using OOP_3.Factories;
using OOP_3.Strategies;

namespace OOP_3;

public partial class MainForm
{
    private bool _isDrawing;
    private IShapeFactory _curFactory;
    private IDrawStrategy _curStrategy;
    private Point _startPoint; 
    private Point _endPoint;
    private Color _curColor;
    public MainForm()
    {
        InitializeComponent();
         _curFactory = new LineFactory();
         _curStrategy = new LineDrawStrategy();
    }

    private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        _startPoint = e.GetPosition((Canvas)sender);
        _isDrawing = true;
    }

    private void Canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
        _isDrawing = false;
    }
    private void Canvas_MouseMove(object sender, MouseEventArgs e)
    {
        if (_isDrawing)
        {
            _endPoint = e.GetPosition((Canvas)sender);
            ((Canvas)sender).Children.RemoveAt(((Canvas)sender).Children.Count - 1);
        }
    }

    private void ColorCb_MouseDown(object sender, MouseButtonEventArgs e)
    {
        SolidColorBrush selectedColor = ColorCb.SelectedItem as SolidColorBrush;
        if (selectedColor != null)
        {
            _curColor = selectedColor.Color;
        }
    }
    
    private void ShapeCb_SelectionChanged(object sender, SelectionChangedEventArgs  e)
    {
        ComboBox comboBox = (ComboBox)sender;
        int selectedIndex = comboBox.SelectedIndex;
        switch (selectedIndex)
        {
            case 0:
                _curFactory = new LineFactory();
                _curStrategy = new LineDrawStrategy();
                break;
        }
    }
}
