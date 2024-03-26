using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using OOP_3.Factories;
using OOP_3.Strategies;
using OOP_3.Figures;
using Path = System.IO.Path;
using Microsoft.Win32;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

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
    private Shape _selectedShape;
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
    readonly List<IDrawStrategy> _strategiesForShapes = new ();
    
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
        // var shapeEditorWindow = new ShapeEditor();
        // shapeEditorWindow.ShowDialog();
    }
    public MainForm()
    {
        InitializeComponent();
        WindowState = WindowState.Maximized;
        InitializeCanvas();
        LoadIcon();
    }

    private AbstractShape DrawShape()
    {
        /////////////////////////////////////////подумай
        var shape = _curFactory.CreateShape(new (_listOfPoints), _curColor);
        shape.Draw(shape, _curStrategy, Canvas);
        return shape;
    }

    private void DeleteShape(KeyEventArgs e)
    {
        if (e.Key == Key.Delete && _isCursorSelected && _selectedShape != null)
        {
            if (_abstractShapes.Count > 0)
            {
                int tag = (int)_selectedShape.Tag;
                for (int i = tag + 1; i < Canvas.Children.Count; i++)
                {
                    if (Canvas.Children[i] is Shape item)
                    {
                        int tagTemp = (int)item.Tag;
                        item.Tag = --tagTemp;
                    }
                }
                for (int i = tag + 1; i < _abstractShapes.Count; i++)
                    _abstractShapes[i].CanvasIndex--;
                _abstractShapes.RemoveAt(tag);
                _strategiesForShapes.RemoveAt(tag);
                Canvas.Children.RemoveAt(tag);
            }
        }
    }
    private void Canvas_KeyDown(object sender, KeyEventArgs e)
    {
        DeleteShape(e);
    }

    private void SelectShape(object sender)
    {
        if (sender is Shape selectedShape)
        {
            var shadowEffect = new DropShadowEffect
            {
                BlurRadius = 10,
                ShadowDepth = 3,
                RenderingBias = RenderingBias.Quality,
                Color = Colors.DarkRed
            };
            selectedShape.Effect = shadowEffect;
            foreach (var child in Canvas.Children)
            {
                if (child is Shape shape && shape != selectedShape && shape.Effect != null)
                    shape.Effect = null;
            }
        }
    }
    private void CheckOnShapeSelection(MouseButtonEventArgs e)
    {
        if (_isCursorSelected)
        {
            if (e is { OriginalSource: Shape shape })
            {
                int tag = (int)shape.Tag;
                for (int i = tag; i < Canvas.Children.Count; i++)
                {
                    if (Canvas.Children[i] is Shape)
                    {
                        _selectedShape = shape;
                        SelectShape(shape);
                    }
                }
            }
        }
    }
    
    void Canvas_OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (e is { ClickCount: 2, OriginalSource: Shape clickedShape })
        {
             int tag = (int)clickedShape.Tag;
             var shape = _abstractShapes[tag];
            // SolidColorBrush color = shape.Color;
             shape.Color = _curColor;
             shape.Draw(shape, _strategiesForShapes[tag], Canvas);
             var shapeEditorWindow = new ShapeEditor();
             shapeEditorWindow.ShowDialog();
        }
    }
    private void CheckLeftBtn(object sender, MouseButtonEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed)
        {
            CheckOnShapeSelection(e);
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
                var shape = DrawShape();
                _listOfPoints.Clear();
                _abstractShapes.Add(shape);
                _strategiesForShapes.Add(_curStrategy);
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
        {
            CursorBtn.BorderBrush = Brushes.Transparent;
            CursorBtn.BorderThickness = new Thickness(1);
        }
        else
        {
            CursorBtn.BorderBrush = Brushes.Blue;
            CursorBtn.BorderThickness = new Thickness(3);
        }
        _listOfPoints.Clear();
    }

    private void Canvas_MouseUp(MouseEventArgs e, AbstractShape shape)
    {
        if (e.LeftButton == MouseButtonState.Released)
        {
            _isDrawing = false;
            _isFirstClick = true;
            if (!_isPolygonSelected)
                _listOfPoints.Clear();
            _abstractShapes.Add(shape);
            _strategiesForShapes.Add(_curStrategy);
        }
    }
    private void RemoveLastChild(object sender)
    {
        if (((Canvas)sender).Children.Count > 0)
            ((Canvas)sender).Children.RemoveAt(((Canvas)sender).Children.Count - 1);
    }
    private void Canvas_MouseMove(object sender, MouseEventArgs e)
    {
        AbstractShape shape = null;
        if (_isDrawing)
        {
            if (!_isFirstClick)
            {
                RemoveLastChild(sender);
            }
            else
            {
                _listOfPoints.Add(e.GetPosition((Canvas)sender));
                _isFirstClick = false;
            }
            if (_listOfPoints.Count > 1)
            {
                _listOfPoints[_listOfPoints.Count - 1] = e.GetPosition((Canvas)sender);
                shape = DrawShape();
            }
            Canvas_MouseUp(e, shape);
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
        _abstractShapes.Clear();
        _strategiesForShapes.Clear();
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

    private void OpenJSON_Click(object sender, EventArgs e)
    {
        
    }
    
    private void OpenBinary_Click(object sender, EventArgs e)
    {
        
    }
    private void SaveToJSON_Click(object sender, EventArgs e)
    {
        SaveFileDialog saveFileDialog = new()
        {
            Filter = "JSON файлы (*.json)|*.json|Все файлы (*.*)|*.*"
        };
        if (saveFileDialog.ShowDialog() == true)
        {
            if (!saveFileDialog.FileName.EndsWith(".json"))
            {
                saveFileDialog.FileName += ".json";
            }
            using FileStream fs = new FileStream(saveFileDialog.FileName, FileMode.Create);
            
            var settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                TypeNameHandling = TypeNameHandling.Objects
            };
            string json = JsonConvert.SerializeObject(_abstractShapes, settings);
            byte[] bytes = Encoding.UTF8.GetBytes(json);
            fs.Write(bytes, 0, bytes.Length);
        }
    }
    
    private void SaveToBinary_Click(object sender, EventArgs e)
    {
        
    }
}