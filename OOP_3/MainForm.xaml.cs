using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using OOP_3.Factories;
using OOP_3.Strategies;
using OOP_3.Figures;
using Path = System.IO.Path;

namespace OOP_3;

public partial class MainForm 
{
    private bool _isDrawing;
    private IShapeFactory _curFactory;
    private IDrawStrategy _curStrategy;
    private SolidColorBrush _curColor;
    private bool _isFirstClick = true;
    private readonly List<Point> _listOfPoints = new();
    private bool _isPolygonSelected;
    private bool _isCursorSelected;
    private List<AbstractShape> _abstractShapes = new();
    private MouseButtonEventArgs eForShapeRemoving;
    private const int GwlStyle = -16;
    private const int WsMaximizeBox = 0x10000;
    readonly Dictionary<int, IShapeFactory> _comboBoxFactories = new()
    {
        { 0, new LineFactory() },
        { 1, new EllipseFactory() },
        { 2, new PolygonFactory() },
        { 3, new RectangleFactory() }
    };
    readonly Dictionary<object, IDrawStrategy> _comboBoxDrawStrategies = new()
    {
        { 0, new LineDrawStrategy() },
        { 1, new EllipseDrawStrategy() },
        { 2, new PolygonDrawStrategy() },
        { 3, new RectangleDrawStrategy() }
    };
    [DllImport("user32.dll")]
    private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
    
    [DllImport("user32.dll")]
    private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

    private void Window_SourceInitialized(object sender, EventArgs e)
    {
        var hwnd = new WindowInteropHelper((Window)sender).Handle;
        var value = GetWindowLong(hwnd, GwlStyle);
        SetWindowLong(hwnd, GwlStyle, value & ~WsMaximizeBox);
    }
    private void LoadIcon()
    {
        var path = Path.Combine(Environment.CurrentDirectory, "Icons", "Paint_Icon.png");
        var uri = new Uri(path);
        var bitmap = new BitmapImage(uri);
        Icon = bitmap;
        Canvas.Focus();
    }

    private void InitializeCanvas()
    {
        _isCursorSelected = true;
        _curColor = Brushes.Black;
    }
    public MainForm()
    {
        InitializeComponent();
        WindowState = WindowState.Maximized;
        InitializeCanvas();
        LoadIcon();
    }

    private void DrawShape()
    {
        var shape = _curFactory.CreateShape(Canvas, _listOfPoints, _curColor);
        shape.Draw(shape, _curStrategy);
    }

    private void DeleteShape(KeyEventArgs e)
    {
        if (e.Key == Key.Delete && _isCursorSelected)
        {
            if (eForShapeRemoving is { ClickCount: 1, OriginalSource: Shape shape })//pattern matching
            {
                int tag = (int)shape.Tag;
                for (int i = tag + 1; i < Canvas.Children.Count; i++)
                {
                    if (Canvas.Children[i] is Shape item)
                    {
                        int tagTemp = (int)item.Tag;
                        item.Tag = --tagTemp;
                    }
                }
                Canvas.Children.RemoveAt(tag);
                for (int i = tag + 1; i < _abstractShapes.Count; i++)
                    _abstractShapes[i].CanvasIndex--;
                _abstractShapes.RemoveAt(tag);
            }
        }
    }
    private void Canvas_KeyDown(object sender, KeyEventArgs e)
    {
        DeleteShape(e);
    }

    private void SelectShape()
    {
        
    }
    private void CheckOnShapeSelection(object sender, MouseButtonEventArgs e)
    {
        if (_isCursorSelected)
        {
            if (e is { ClickCount: 1, OriginalSource: Shape shape })
            {
                int tag = (int)shape.Tag;
                for (int i = tag + 1; i < Canvas.Children.Count; i++)
                {
                    if (Canvas.Children[i] is Shape item)
                    {
                        eForShapeRemoving = e;
                        SelectShape();
                    }
                }
            }
        }
    }
    private void CheckLeftBtn(object sender, MouseButtonEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed)
        {
            CheckOnShapeSelection(sender, e);
            _listOfPoints.Add(e.GetPosition((Canvas)sender));
            if (!_isPolygonSelected && !_isCursorSelected)
                _isDrawing = true;
        }
    }

    private void CheckRightBtn(MouseButtonEventArgs e)
    {
        if (e.RightButton == MouseButtonState.Pressed)
        {
            if (_isPolygonSelected && !_isCursorSelected && _listOfPoints.Count > 2)
            {
                DrawShape();
                _listOfPoints.Clear();
            }
        }
    }
    private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
    {
        Canvas.Focus();
        CheckLeftBtn(sender, e);
        CheckRightBtn(e);
    }
    private void CursorBtn_Click(object sender, EventArgs e)
    {
        _isCursorSelected = _isCursorSelected ? false : true;
        if (!_isCursorSelected)
            CursorBtn.BorderBrush = Brushes.Transparent;
        else
            CursorBtn.BorderBrush = Brushes.Blue;
        _listOfPoints.Clear();
    }

    private void Canvas_MouseUp(object sender, MouseEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Released)
        {
            _isDrawing = false;
            _isFirstClick = true;
            if (!_isPolygonSelected)
                _listOfPoints.Clear();
        }
    }
    private void RemoveLastChild(object sender)
    {
        if (((Canvas)sender).Children.Count > 0)
            ((Canvas)sender).Children.RemoveAt(((Canvas)sender).Children.Count - 1);
    }
    private void Canvas_MouseMove(object sender, MouseEventArgs e)
    {
        if (_isDrawing)
        {
            if (!_isFirstClick)
                RemoveLastChild(sender);
            if (_isFirstClick)
                _listOfPoints.Add(e.GetPosition((Canvas)sender));
            if (_listOfPoints.Count > 1)
            {
                _listOfPoints[_listOfPoints.Count - 1] = e.GetPosition((Canvas)sender);
                DrawShape();
                if (_isFirstClick)
                    _isFirstClick = false;
            }
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
        Canvas.Children.Clear();
        _listOfPoints.Clear();
    }

    private void SelectFigure(object sender)
    {
        ComboBox comboBox = (ComboBox)sender;
        int selectedIndex = comboBox.SelectedIndex;
        _curFactory = _comboBoxFactories[selectedIndex];
        _curStrategy = _comboBoxDrawStrategies[selectedIndex];
        if (selectedIndex == 2)
            _isPolygonSelected = true;
        else
            _isPolygonSelected = false;
        _listOfPoints.Clear();
    }
    private void ShapeCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        SelectFigure(sender);
    }
}
