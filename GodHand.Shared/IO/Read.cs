using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Xml;
using System.Xml.Serialization;
using GodHand.Shared.Models;

namespace GodHand.Shared.IO
{
    public class Read
    {
        public static ObservableCollection<ByteInformation> File(string path, long start, long length)
        {            
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

        public static T Xml<T>(string path) where T: new()
        {
            var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None );
            var returnItem = new T();

            XmlSerializer xml = null;

            using (var rd = new XmlTextReader(fs))
            {
                xml = new XmlSerializer(typeof(T));
                returnItem = (T)xml.Deserialize(rd);
            }

            return returnItem;
        }

        public static string Picture()
        {
            string c2tPath = (Environment.Is64BitProcess) ? @"\lib\Capture2Text\x64\Capture2Text_CLI.exe" : @"\lib\Capture2Text\x86\Capture2Text_CLI.exe";
            string ocrPath= Environment.CurrentDirectory + @"\temp\ocr.txt";
            string result = string.Empty;

            try
            {
                Process proc = new Process();
                ProcessStartInfo processStartInfo = new ProcessStartInfo();
                processStartInfo.CreateNoWindow = true;
                processStartInfo.UseShellExecute = false;

                processStartInfo.FileName = Environment.CurrentDirectory + c2tPath;
                processStartInfo.Arguments =
                    $@" -l ""Japanese"" -i ""{Environment.CurrentDirectory + @"\temp\temp.jpg"}"" -o ""{ocrPath}""";              
                proc.StartInfo = processStartInfo;
                proc.Start();
                proc.WaitForExit();

                result = System.IO.File.ReadAllText(ocrPath, Encoding.UTF8);

            }

            catch (Exception e)
            {
                MessageBox.Show("There has been an error while reading picture: " + e);
            }

            return result;
        }
    }
}
