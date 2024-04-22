using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Serialization;
using BaseShapesClasses;
using Microsoft.Win32;
using OOP_3.Figures;

namespace OOP_3.Deserialization;

public class CustomXmlDeserializer
{
    private DeserializationDrawer DeserializationDrawer { get; } = new();
    public void XmlDeserialize(Dictionary<int, IShapeFactory> comboBoxFactories, List<AbstractShape> abstractShapes, Canvas canvas)
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
                    abstractShapes.Clear();
                    canvas.Children.Clear();
                    DeserializationDrawer.DrawDeserializedFigures(loadedShapes, comboBoxFactories, abstractShapes, canvas);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка номер 52 при открытии файла. Возможно, " +
                                "Вы загрузили не все модули фигур.");
            }
        }
    }
}
