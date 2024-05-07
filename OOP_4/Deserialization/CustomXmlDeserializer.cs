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
    public List<AbstractShape> XmlDeserialize(Dictionary<int, IShapeFactory> comboBoxFactories, Stream stream)
    {
        List<AbstractShape> abstractShapes = new();
        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<SerializedShape>));
            if (serializer.Deserialize(stream) is List<SerializedShape> { Count: not 0 } loadedShapes)
            {
                foreach (var item in loadedShapes)
                {
                    //comboBoxFactories[0].
                    if (comboBoxFactories.TryGetValue(item.TagShape, out var factory))
                    {
                        var shape = factory.CreateShape(item.ListOfPoints, item.Color);
                        abstractShapes.Add(shape);
                    }
                }
                return abstractShapes;
            }
        }
        catch (Exception)
        {
            MessageBox.Show("Ошибка номер 52 при открытии файла. Возможно, " +
                            "Вы загрузили не все модули фигур.");
        }
        return null;
    }
}
