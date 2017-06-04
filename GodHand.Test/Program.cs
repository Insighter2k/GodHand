using System;
using System.Collections.Generic;
using System.IO;
using GodHand.Shared.Models.TreeView;


namespace GodHand.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //string test = "E31B";
            //List<byte> testArray = new List<byte>(){227,27};


            //List<byte> byteList2 = new List<byte>() { 227, 129,130 };

            //List<byte> byteList = new List<byte>() {131, 95, 131, 126, 129, 91};
            //string filename = "dqm_3ds_table.txt";
            //var dict = Shared.IO.Read.EncodingTable(filename);

            //Stopwatch timOri = new Stopwatch();
            //Stopwatch timCust = new Stopwatch();


            ////timOri.Start();
            ////var original = Encoding.GetEncoding(932).GetString(byteList.ToArray());
            ////timOri.Stop();
            ////timCust.Start();
            ////var custom = Shared.IO.Convert.ByteValueToCustomEncoding(byteList.ToArray(), dict);
            ////timCust.Stop();

            ////var original2 = Encoding.GetEncoding(932).GetString(byteList2.ToArray());
            ////var custom2 = Shared.IO.Convert.ByteValueToCustomEncoding(byteList2.ToArray(), dict);

            //var original3 = Encoding.GetEncoding(932).GetString(testArray.ToArray());
            //var custom3 = Shared.IO.Convert.ByteValueToCustomEncoding(testArray.ToArray(), dict);
            //custom3 = custom3.Replace("\\", "\\");

            ////Console.WriteLine("HEX: 835F837E815B");
            ////Console.WriteLine("Original: " + original + " - Time (ms): " + timOri.ElapsedMilliseconds);
            ////Console.WriteLine("Custom: " + custom + " - Time (ms): " + timCust.ElapsedMilliseconds);
            ////Console.WriteLine("HEX: E38182");
            ////Console.WriteLine("Original: " + original2);
            ////Console.WriteLine("Custom: " + custom2);

            //Console.WriteLine("HEX: E31B");
            //Console.WriteLine("Original: " + original3);
            //Console.WriteLine("Custom: " + custom3);

            var ro = JishoCSharpWrapper.Shared.Client.RequestValuesFromJisho("name", false).Result;
 




             Console.WriteLine();
            Console.WriteLine();
 

            Console.ReadKey();
        }

        private static List<TreeViewItem> GetDicItems(string path)
        {
            var items = new List<TreeViewItem>();

            var dirInfo = new DirectoryInfo(path);

            foreach (var directory in dirInfo.GetDirectories())
            {
                var item = new DirectoryItem
                {
                    Name = directory.Name,
                    Fullpath = directory.FullName,
                    Items = GetDicItems(directory.FullName)
                };

                items.Add(item);
            }

            return items;
        }
    }
}
