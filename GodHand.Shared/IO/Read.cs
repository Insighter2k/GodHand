using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using GodHand.Shared.Models;

namespace GodHand.Shared.IO
{
    public class Read
    {

        public static ObservableCollection<ByteInformation> File(string path, long start, long length)
        {
            Kakasi.NET.Interop.KakasiLib.Init();
            Kakasi.NET.Interop.KakasiLib.SetParams(new[] {"kakasi", "-U", "-Ka", "-Ha", "-Ja", "-Ea", "-ka", "-s", "-C"});

            Encoding utf8Encoding = Encoding.UTF8;
            using (Stream fStr = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                BinaryReader bReader = new BinaryReader(fStr);
                ObservableCollection<ByteInformation> lByteInformation = new ObservableCollection<ByteInformation>();

                byte[] bytes;

                if (start != -1) bReader.BaseStream.Seek(start,SeekOrigin.Begin);
                if (length == -1) bytes = bReader.ReadBytes(System.Convert.ToInt32(bReader.BaseStream.Length));
                else bytes = bReader.ReadBytes(System.Convert.ToInt32(length));

                List<byte> tempByteList = new List<byte>();
                int i = -1;
                for (int j = 0; j < bytes.Length; j++)
                {
                    if (bytes[j] == 0 && tempByteList.Count == 0) continue;
                    else if (int.TryParse(bytes[j].ToString(), out i) == false) continue;
                    else if (bytes[j] == 0 && tempByteList.Count > 0)
                    {
                        ByteInformation bi = new ByteInformation(tempByteList.ToArray(), j - tempByteList.Count,
                            Encoding.GetEncoding(932).GetString(tempByteList.ToArray()));
                        lByteInformation.Add(bi);
                        tempByteList.Clear();
                    }
                    else tempByteList.Add(bytes[j]);
                }

                Regex regEx =
                    new Regex(
                        @"\p{IsHiragana}|\p{IsKatakana}|\p{IsKatakanaPhoneticExtensions}|\p{IsEnclosedCJKLettersandMonths}|\p{IsCJKSymbolsandPunctuation}");
                for (int j = 0; j < lByteInformation.Count; j++)
                {
                    if (!regEx.IsMatch(lByteInformation[j].CurrentValue))
                    {
                        lByteInformation.RemoveAt(j);
                        j--;
                    }
                }
                return lByteInformation;
            }
        }
    }
}
