using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using GodHand.Shared.Models;

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
    }
}
