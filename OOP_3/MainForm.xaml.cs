using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using OOP_3.Factories;
using OOP_3.Strategies;
using OOP_3.Figures;

namespace OOP_3;

public partial class MainForm 
{
    private bool _isDrawing;
    private IShapeFactory _curFactory;
    private IDrawStrategy _curStrategy;
    private SolidColorBrush _curColor;
    private bool _isFirstClick;
    private List<Point> _listOfPoints = new List<Point>();
    private bool _isPolygonChoosed;
    private AbstractShape _selectedShape;
    private bool _isCursorChoosed;

    private void LoadIcon()
    {
        var path = Path.Combine(Environment.CurrentDirectory, "Icons", "Paint_Icon.png");
        var uri = new Uri(path);
        var bitmap = new BitmapImage(uri);
        Icon = bitmap;
    }
    [DllImport("user32.dll")]
    private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
    [DllImport("user32.dll")]
    private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

    private const int GwlStyle = -16;
    private const int WsMaximizeBox = 0x10000;

    private void Window_SourceInitialized(object sender, EventArgs e)
    {
        var hwnd = new WindowInteropHelper((Window)sender).Handle;
        var value = GetWindowLong(hwnd, GwlStyle);
        SetWindowLong(hwnd, GwlStyle, value & ~WsMaximizeBox);
    }
    public MainForm()
    {
        InitializeComponent();
        _isCursorChoosed = true;
         _curColor = Brushes.Black;
         WindowState = WindowState.Maximized;
         LoadIcon();
    }

    private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed)
        {
            _listOfPoints.Add(e.GetPosition((Canvas)sender));
            if (!_isPolygonChoosed && !_isCursorChoosed)
                _isDrawing = true;
        }
        if (e.RightButton == MouseButtonState.Pressed)
        {
            if (_isPolygonChoosed && !_isCursorChoosed)
            {
                var shape = _curFactory.CreateShape(Canvas1, _listOfPoints, _curColor);
                shape.Draw(shape, _curStrategy);
                _listOfPoints.Clear();
            }
        }
        foreach (var shape in Canvas1.Children)
        {
            if (shape is AbstractShape && ((FrameworkElement)shape).IsMouseOver)
            {
                // Если клик попал на фигуру, выделяем ее
                _selectedShape = (AbstractShape)shape;
                // Меняем свойства выделенной фигуры, например, цвет обводки
                //_selectedShape.Stroke = Brushes.Red;
                // Выход из цикла, так как фигура уже выбрана
                return;
            }
        }

        // Если клик не попал ни на одну фигуру, сбрасываем выбор
        _selectedShape = null;
    }
    private void CursorBtn_Click(object sender, EventArgs e)
    {
        _isCursorChoosed = _isCursorChoosed ? false : true;
        Button clickedButton = sender as Button;
        if (!_isCursorChoosed)
            clickedButton.BorderBrush = Brushes.Transparent;
        else
        {
            _listOfPoints.Clear();
            clickedButton.BorderBrush = Brushes.Blue;
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
            if (_listOfPoints.Count > 1)
            {
                _listOfPoints[_listOfPoints.Count - 1] = e.GetPosition((Canvas)sender);
                var shape = _curFactory.CreateShape(Canvas1, _listOfPoints, _curColor);
                shape.Draw(shape, _curStrategy);
                if (_isFirstClick)
                    _isFirstClick = false;
            }
        }
        if (e.LeftButton == MouseButtonState.Released)
        {
            _isDrawing = false;
            _isFirstClick = true;
            if (!_isPolygonChoosed)
                _listOfPoints.Clear();
        }
    }

    private void ColorCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selectedColor = ColorCb.SelectedItem as SolidColorBrush;
        if (selectedColor != null)
            _curColor = selectedColor;
    }

    private void ClearBtn_Click(object sender, RoutedEventArgs e)
    {
        Canvas1.Children.Clear();
        _listOfPoints.Clear();
    }
    private void ShapeCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        ComboBox comboBox = (ComboBox)sender;
        int selectedIndex = comboBox.SelectedIndex;
        _listOfPoints.Clear();
        switch (selectedIndex)
        {
            case 0:
                _isPolygonChoosed = false;
                _curFactory = new LineFactory();
                _curStrategy = new LineDrawStrategy();
                break;
            case 1:
                _isPolygonChoosed = false;
                _curFactory = new EllipseFactory();
                _curStrategy = new EllipseDrawStrategy();
                break;
            case 2:
                _isPolygonChoosed = true;
                _curFactory = new PolygonFactory();
                _curStrategy = new PolygonDrawStrategy();
                break;
            case 3:
                _isPolygonChoosed = false;
                _curFactory = new RectangleFactory();
                _curStrategy = new RectangleDrawStrategy();
                break; 
        }
    }
}
