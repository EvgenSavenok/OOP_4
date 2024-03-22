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
    private SolidColorBrush _curColor;
    public MainForm()
    {
        InitializeComponent();
         _curFactory = new LineFactory();
         _curStrategy = new LineDrawStrategy();
         _curColor = ColorCb.SelectedItem as SolidColorBrush;
    }

    private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed)
        {
            _startPoint = e.GetPosition((Canvas)sender);
            _isDrawing = true;
        }
    }

    private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
    {
        if (e.ChangedButton == MouseButton.Left)
        {
            _isDrawing = false;
        }
    }
    private void Canvas_MouseMove(object sender, MouseEventArgs e)
    {
        if (_isDrawing)
        {
            if (((Canvas)sender).Children.Count > 0) 
                ((Canvas)sender).Children.RemoveAt(((Canvas)sender).Children.Count - 1);
            _endPoint = e.GetPosition((Canvas)sender);
            var shape = _curFactory.CreateShape(Canvas1, _startPoint, _endPoint, _curColor);
            shape.Draw(shape, _curStrategy);
        }
    }

    private void ColorCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selectedColor = ColorCb.SelectedItem as SolidColorBrush;
        if (selectedColor != null)
        {
            _curColor = selectedColor;
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
