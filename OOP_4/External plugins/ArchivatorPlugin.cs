using System.IO;
using System.IO.Compression;
using System.Windows;
using BaseShapesClasses;
using Microsoft.Win32;
using OOP_3.Functionality;

namespace ArchivatorPlugin;

// public class ArchivatorPlugin : IFunctionality
// {
//     public string Name => "Архиватор XML";
//
//     public void SaveToFile(List<AbstractShape> abstractShapes)
//     {
//         MyXMLSerializer myXmlSerializer = new();
//         var fileName = myXmlSerializer.Serialize(abstractShapes);
//         //Изначальный
//         using (FileStream sourceStream = new FileStream(fileName, FileMode.Open))
//         {
//             using FileStream fileArchive = File.Create(fileName + ".gz");
//
//             using (GZipStream compressionStream = new GZipStream(fileArchive, CompressionLevel.Optimal, true))
//             {
//                 sourceStream.CopyTo(compressionStream);
//             }
//
//             MessageBox.Show($"Сжатие прошло успешно.\nИсходный размер: {sourceStream.Length}\nСжатый размер: {fileArchive.Length}");
//         }
//         File.Delete(fileName);
//     }
//
//     public bool LoadFile(List<AbstractShape> abstractShapes, Dictionary<object, IShapeFactory> dictionary)
//     {
//         OpenFileDialog openFileDialog = new()
//         {
//             Filter = "gz файлы (*.gz)|*.gz"
//         };
//         if (openFileDialog.ShowDialog() == true)
//         {
//             using FileStream sourceStream = new FileStream(openFileDialog.FileName, FileMode.OpenOrCreate);
//             using GZipStream decompressionStream = new GZipStream(sourceStream, CompressionMode.Decompress);
//             using MemoryStream memoryStream = new(); //Для сохранения потока в памяти
//             decompressionStream.CopyTo(memoryStream);
//             memoryStream.Seek(0, SeekOrigin.Begin);
//             MyXMLSerializer myXmlSerializer = new();
//             var listShapes = myXmlSerializer.Deserialize(memoryStream);
//             if (listShapes is not null)
//             {
//                 abstractShapes.Clear();
//                 AbstractShape.Canvas.Children.Clear();
//                 foreach (var item in listShapes)
//                 {
//                     if (dictionary.TryGetValue(item.TagShape, out var factory))
//                     {
//                         var shape = factory.CreateShape(item.TopLeft, item.DownRight, item.BackgroundColor, item.PenColor, item.Angle);
//                         shape.StrokeThickness = item.StrokeThickness;
//
//                         abstractShapes.Add(shape);
//                         shape.DrawAlgorithm();
//
//                     }
//                 }
//
//                 MessageBox.Show("Список фигур успешно загружен!");
//                 return true;
//             }
//         }
//         return false;
//     }
// }