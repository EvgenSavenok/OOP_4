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
    private SolidColorBrush _curColor;
    private bool _isFirstClick;
    private List<Point> _listOfPoints = new List<Point>();
    public MainForm()
    {
        InitializeComponent();
         _curFactory = new LineFactory();
         _curStrategy = new LineDrawStrategy();
         _curColor = Brushes.Red;
    }

    private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed)
        {
            _listOfPoints.Add(e.GetPosition((Canvas)sender));
            _isDrawing = true;
        }
    }
    private void Canvas_MouseMove(object sender, MouseEventArgs e)
    {
        if (_isDrawing)
        {
            if (!_isFirstClick)
            {
                if (((Canvas)sender).Children.Count > 0)
                    ((Canvas)sender).Children.RemoveAt(((Canvas)sender).Children.Count - 1);
            }
            if (_isFirstClick)
                _listOfPoints.Add(e.GetPosition((Canvas)sender));
            _listOfPoints[1] = e.GetPosition((Canvas)sender);
            var shape = _curFactory.CreateShape(Canvas1, _listOfPoints, _curColor);
            shape.Draw(shape, _curStrategy);
            if (_isFirstClick)
                _isFirstClick = false;
        }
        if (e.LeftButton == MouseButtonState.Released)
        {
            _isDrawing = false;
            _isFirstClick = true;
            _listOfPoints.Clear();
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
            case 1:
                _curFactory = new EllipseFactory();
                _curStrategy = new EllipseDrawStrategy();
                break;
        }
    }
}
