using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Security.AccessControl;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;
using GodHand.Shared.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace GodHand.Shared.IO
{
    public class Write
    {
        public static void ValueToFile(string path, ObservableCollection<ByteInformation> biCollection)
        {
            using (Stream fStr = new FileStream(path, FileMode.Open, FileAccess.Write, FileShare.None))
            {
                BinaryWriter bWriter = new BinaryWriter(fStr);

                foreach (var bi in biCollection)
                {
                    byte[] byteArray = new byte[bi.ByteValue.Length];

                    for (int i = 0; i < bi.NewValueLength; i++)
                    {
                        byteArray[i] = System.Convert.ToByte(bi.NewValue[i]);
                    }

                    bWriter.Seek(bi.StartPosition, SeekOrigin.Begin);
                    bWriter.Write(byteArray, 0, byteArray.Length);
                }


            }
        }

        public static void Xml<T>(T item, string path)
        {
            using (var sw = new StreamWriter(path, false, Encoding.ASCII))
            {
                XmlSerializer xml = new XmlSerializer(typeof(T));
                xml.Serialize(sw,item);
            }
        }

        public static void CroppedPicToJpeg(Canvas _canvas, int x1, int y1, int x2, int y2)
        {
            string path = Environment.CurrentDirectory + @"\temp\";
            string filename = "temp.jpg";

            Directory.CreateDirectory(path);

            RenderTargetBitmap renderBitmap = new RenderTargetBitmap(
                (int) _canvas.ActualWidth,
                (int) _canvas.ActualHeight,
                96d,
                96d,
                PixelFormats.Pbgra32);

            Size size = new Size(_canvas.ActualWidth, _canvas.ActualHeight);

            Rectangle rect = new Rectangle() { Width = _canvas.ActualWidth, Height = _canvas.ActualHeight, Fill = new VisualBrush(_canvas) };
            rect.Measure(size);
            rect.Arrange(new Rect(size));
            rect.UpdateLayout();

            renderBitmap.Render(rect);

            var crop = new CroppedBitmap(renderBitmap, new Int32Rect(x1, y1, x2, y2));

            BitmapEncoder pngEncoder = new JpegBitmapEncoder();
            pngEncoder.Frames.Add(BitmapFrame.Create(crop));

            using (var fs = File.OpenWrite(path+filename))
            {
                pngEncoder.Save(fs);
            }
        }
    }
}
