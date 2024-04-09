using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Serialization;
using OOP_3.Figures;
using Path = System.IO.Path;
using Microsoft.Win32;
using Newtonsoft.Json;
using BaseShapesClasses;

namespace OOP_3;

public partial class MainForm
{
    private bool _isDrawing;
    private IShapeFactory _curFactory;
    private Brush _curColor;
    private bool _isFirstClick = true;
    private readonly List<Point> _listOfPoints = new();
    private bool _isPolygonSelected;
    private bool _isCursorSelected;
    private List<AbstractShape> _abstractShapes = new();
    private Shape _selectedShape;
    private const int GwlStyle = -16;
    private const int WsMaximizeBox = 0x10000;
    Point _previousMousePosition = new(-1, -1);
    private readonly ObservableCollection<object> _comboBoxItems = new();
    private readonly Dictionary<int, IShapeFactory> _comboBoxFactories = new();

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
        ShapesCb.ItemsSource = _comboBoxItems;
    }

    private bool CheckExistingModules(IShapeFactory factory)
    {
        foreach (var item in _comboBoxFactories)
        {
            if (item.Value.GetType() == factory.GetType())
                return false;
        }
        return true;
    }
    
    private void LoadAssemblies(string assembly)
    {
        Assembly pluginAssembly = Assembly.LoadFrom(assembly);
        foreach (Type type in pluginAssembly.GetTypes())
        {
            if (typeof(IShapeFactory).IsAssignableFrom(type))
            {
                IShapeFactory factory = (IShapeFactory)Activator.CreateInstance(type);
                Type t = factory.GetType();
                if (CheckExistingModules(factory))
                {
                    _comboBoxFactories.Add(_comboBoxFactories.Count, factory);
                    _comboBoxItems.Add(factory.GetFactoryName());
                }
            }
        }
    }
    
    private void LoadFactories(string path)
    {
        if (path != null)
        {
            LoadAssemblies(path);
            //_curFactory = _comboBoxFactories[0];
            _selectedShape = null;
        }
    }

    private string HandleOpenedFile()
    {
        OpenFileDialog openFileDialog = new()
        {
            Filter = "DLL (*.dll)|*.dll"
        };
        if (openFileDialog.ShowDialog() == true)
        {
            return openFileDialog.FileName;
        }
        return null;
    }

    private void LoadModule_Click(object sender, EventArgs e)
    {
        string path = HandleOpenedFile();
        LoadFactories(path);
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
        var shape = _curFactory.CreateShape(new List<Point>(_listOfPoints), _curColor);
        shape.Draw(Canvas);
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
        bool isShapeSelected = false;
        if (_isCursorSelected && (e is { OriginalSource: Shape shape }))
        {
            int tag = (int)shape.Tag;
            for (int i = tag; i < Canvas.Children.Count; i++)
            {
                if (Canvas.Children[i] is Shape)
                {
                    _selectedShape = shape;
                    SelectShape(shape);
                    isShapeSelected = true;
                }
            }
        }
        if (!isShapeSelected)
            _selectedShape = null;
    }

    private void CheckLeftBtn(object sender, MouseButtonEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed)
        {
            CheckOnShapeSelection(e);
            _listOfPoints.Add(e.GetPosition((Canvas)sender));
            _previousMousePosition = new Point(-1, -1);
            if (!_isPolygonSelected && !_isCursorSelected && _curFactory != null)
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
            CursorBtn.BorderBrush = Brushes.Black;
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
        }
    }

    private void RemoveLastChild(object sender)
    {
        if (((Canvas)sender).Children.Count > 0)
            ((Canvas)sender).Children.RemoveAt(((Canvas)sender).Children.Count - 1);
    }

    private void SetFirstClick(object sender, MouseEventArgs e)
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
    }

    private AbstractShape RedrawShapeAccordingNewPoints(object sender, MouseEventArgs e)
    {
        AbstractShape shape = null;
        if (_listOfPoints.Count > 1)
        {
            _listOfPoints[_listOfPoints.Count - 1] = e.GetPosition((Canvas)sender);
            shape = DrawShape();
        }
        return shape;
    }

    private void MoveSelectedShape(object sender, MouseEventArgs e)
    {
        Point currentMousePosition = e.GetPosition((Canvas)sender);
        double deltaX, deltaY;
        if (_previousMousePosition.X == -1)
        {
            _previousMousePosition = e.GetPosition((Canvas)sender);
            deltaX = 0;
            deltaY = 0;
        }
        else
        {
            deltaX = currentMousePosition.X - _previousMousePosition.X;
            deltaY = currentMousePosition.Y - _previousMousePosition.Y;
        }
        int tag = (int)_selectedShape.Tag;
        var movingShape = _abstractShapes[tag];
        List<Point> movingShapeCoordinates = movingShape.ListOfPoints;
        for (int i = 0; i < movingShapeCoordinates.Count; i++)
        {
            Point point = movingShapeCoordinates[i];
            movingShapeCoordinates[i] = new Point(point.X + deltaX, point.Y + deltaY);
        }
        movingShape.ListOfPoints = movingShapeCoordinates;
        movingShape.Draw(Canvas);
        _previousMousePosition = currentMousePosition;
    }

    private void Canvas_MouseMove(object sender, MouseEventArgs e)
    {
        if (_isDrawing)
        {
            SetFirstClick(sender, e);
            AbstractShape shape = RedrawShapeAccordingNewPoints(sender, e);
            Canvas_MouseUp(e, shape);
        }
        if (e.LeftButton == MouseButtonState.Pressed && _selectedShape != null)
            MoveSelectedShape(sender, e);
    }

    private void ChangeFuguresColor()
    {
        if (_selectedShape != null)
        {
            int tag = (int)_selectedShape.Tag;
            var shape = _abstractShapes[tag];
            shape.Color = _curColor;
            shape.Draw(Canvas);
        }
    }

    private void ColorCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selectedColor = ColorCb.SelectedItem as Brush;
        if (selectedColor != null)
            _curColor = selectedColor;
        ChangeFuguresColor();
    }

    private void ClearBtn_Click(object sender, RoutedEventArgs e)
    {
        Canvas.Children.Clear();
        _listOfPoints.Clear();
        _abstractShapes.Clear();
    }

    private void SelectFigure(object sender)
    {
        ComboBox comboBox = (ComboBox)sender;
        int selectedIndex = comboBox.SelectedIndex;
        if (_comboBoxFactories.Count > 0)
            _curFactory = _comboBoxFactories[selectedIndex];
        if (_comboBoxItems[selectedIndex] == "Многоугольник") //Пофикси эту дичь
            _isPolygonSelected = true;
        else
            _isPolygonSelected = false;
        _listOfPoints.Clear();
    }

    private void ShapeCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        SelectFigure(sender);
    }

    private void AboutDeveloper_Click(object sender, EventArgs e)
    {
        const string aboutDev = "Евгений Савенок, группа 251004";
        MessageBox.Show(aboutDev, "О разработчике");
    }

    private void Help_Click(object sender, EventArgs e)
    {
        const string helpInfo1 = "1) Для отрисовки фигуры уберите синюю рамку с курсора, кликнув на него. " +
                                 "Вы сможете выбрать из списка одну из фигур и нарисовать её.";
        const string helpInfo2 = "\n2) Для того, чтобы нарисовать многоугольник, кликните в тех местах холста, " +
                                 "где Вы хотите видеть углы многоугольника, после чего нажмите ПКМ.";
        const string helpInfo3 = "\n3) Для удаления фигуры кликните по ней и намите Delete.";
        const string helpInfo4 =
            "\n4) Для изменения цвета фигуры кликните по ней, а потом выберите их списка цветов нужный. " +
            "Для изменения положения фигуры зажмите ЛКМ на ней и передвигайте.";
        const string helpInfo5 =
            "\n5) Для сохранения результата работы нажмите Файл->Сохранить в формате:->JSON или XML " +
            "(Бинарный формат устарел и не поддерживается, поэтому не рекомендуется его использовать)." +
            "\nДля того, чтобы открыть предыдущую работу, выполните аналогичные действия с Открыть.";
        const string helpInfo6 =
            "\n6) Для использования полной версии приложения пришлите разработчикам банку сгущенки.";
        MessageBox.Show(helpInfo1 + helpInfo2 + helpInfo3 + helpInfo4 + helpInfo5 + helpInfo6, "Помощь",
            MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.Yes);
    }

    private void OpenJSON_Click(object sender, EventArgs e)
    {
        OpenFileDialog openFileDialog = new()
        {
            Filter = "JSON файлы (*.json)|*.json"
        };
        if (openFileDialog.ShowDialog() == true)
        {
            try
            {
                string json = File.ReadAllText(openFileDialog.FileName);
                var settings = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Objects,
                };
                _abstractShapes = JsonConvert.DeserializeObject<List<AbstractShape>>(json, settings);
                Canvas.Children.Clear();
                foreach (var shape in _abstractShapes)
                {
                    shape.Draw(Canvas);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка номер 52 при открытии файла. Возможно, " +
                                "Вы загрузили не все модули фигур.");
            }
        }
    }

    private void OpenBinary_Click(object sender, EventArgs e)
    {
        OpenFileDialog openFileDialog = new()
        {
            Filter = "Бинарные файлы (*.dat)|*.dat"
        };
        if (openFileDialog.ShowDialog() == true)
        {
            try
            {
                using FileStream fs = new FileStream(openFileDialog.FileName, FileMode.Open);
#pragma warning disable SYSLIB0011
                BinaryFormatter formatter = new BinaryFormatter();
#pragma warning restore SYSLIB0011
                if ((List<SerializedShape>)formatter.Deserialize(fs) is List<SerializedShape> { Count: not 0 } loadedShapes)
                {
                    _abstractShapes.Clear();
                    Canvas.Children.Clear();
                    foreach (var item in loadedShapes)
                    {
                        
                        var shape = _curFactory.CreateShape(item.ListOfPoints, item.Color);
                        _abstractShapes.Add(shape);
                        shape.CanvasIndex = -1;
                        shape.Draw(Canvas);
                    }
                }
            }
            catch (Exception)
            { 
                MessageBox.Show("Ошибка номер 52 при открытии файла. Возможно, " +
                                "Вы загрузили не все модули фигур.");
            }
        }
    }

    private void OpenXML_Click(object sender, EventArgs e)
    {
        OpenFileDialog openFileDialog = new()
        {
            Filter = "XML файлы (*.xml)|*.xml"
        };
        if (openFileDialog.ShowDialog() == true)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<SerializedShape>));
                using FileStream fs = new FileStream(openFileDialog.FileName, FileMode.OpenOrCreate);
                if (serializer.Deserialize(fs) is List<SerializedShape> { Count: not 0 } loadedShapes)
                {
                    _abstractShapes.Clear();
                    Canvas.Children.Clear();
                    foreach (var item in loadedShapes)
                    {
                        var shape = _curFactory.CreateShape(item.ListOfPoints, item.Color);
                        _abstractShapes.Add(shape);
                        shape.CanvasIndex = -1;
                        shape.Draw(Canvas);
                    }
                }
            }
            catch (Exception)
            { 
                MessageBox.Show("Ошибка номер 52 при открытии файла. Возможно, " +
                                "Вы загрузили не все модули фигур.");
            }
        }
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
                saveFileDialog.FileName += ".json";
            using FileStream fs = new FileStream(saveFileDialog.FileName, FileMode.Create);
            var settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                TypeNameHandling = TypeNameHandling.Objects
            };
            try
            {
                string json = JsonConvert.SerializeObject(_abstractShapes, settings);
                byte[] bytes = Encoding.UTF8.GetBytes(json);
                fs.Write(bytes, 0, bytes.Length);
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка номер 52 при сохранении файла.");
            }
        }
    }

    private void SaveToBinary_Click(object sender, EventArgs e)
    {
        SaveFileDialog saveFileDialog = new()
        {
            Filter = "Бинарные файлы (*.dat)|*.dat|Все файлы (*.*)|*.*"
        };
        if (saveFileDialog.ShowDialog() == true)
        {
            List<SerializedShape> xmlShapes = new();
            foreach (var shape in _abstractShapes)
            {
                xmlShapes.Add(new()
                {
                    ListOfPoints = shape.ListOfPoints, Color = shape.Color, NumOfFactory = shape.NumOfFactory
                });
            }
            if (!saveFileDialog.FileName.EndsWith(".dat"))
                saveFileDialog.FileName += ".dat";
            using FileStream fs = new FileStream(saveFileDialog.FileName, FileMode.OpenOrCreate);
#pragma warning disable SYSLIB0011
            BinaryFormatter formatter = new BinaryFormatter();
#pragma warning restore SYSLIB0011
            formatter.Serialize(fs, xmlShapes);
        }
    }

    private void SaveToXML_Click(object sender, EventArgs e)
    {
        SaveFileDialog saveFileDialog = new()
        {
            Filter = "XML файлы (*.xml)|*.xml|Все файлы (*.*)|*.*"
        };
        if (saveFileDialog.ShowDialog() == true)
        {
            if (!saveFileDialog.FileName.EndsWith(".xml"))
                saveFileDialog.FileName += ".xml";
            try
            {
                using FileStream fs = new FileStream(saveFileDialog.FileName, FileMode.OpenOrCreate);
                List<SerializedShape> xmlShapes = new();
                foreach (var shape in _abstractShapes)
                {
                    xmlShapes.Add(new()
                    {
                        ListOfPoints = shape.ListOfPoints, Color = shape.Color, NumOfFactory = shape.NumOfFactory
                    });
                }
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<SerializedShape>));
                xmlSerializer.Serialize(fs, xmlShapes);
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка номер 52 при сохранении файла.");
            }
        }
    }
}
