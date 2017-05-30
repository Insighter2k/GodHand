using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Timers;


namespace GodHand.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            string test = "5F5BE1E3";
            List<byte> testArray = System.Convert.FromBase64String(test).ToList();


            List<byte> byteList2 = new List<byte>() { 227, 129,130 };
          
            List<byte> byteList = new List<byte>() {131, 95, 131, 126, 129, 91};
            string filename = "dqm_3ds_table.txt";
            var dict = Shared.IO.Read.EncodingTable(filename);

            Stopwatch timOri = new Stopwatch();
            Stopwatch timCust = new Stopwatch();
           

            timOri.Start();
            var original = Encoding.GetEncoding(932).GetString(byteList.ToArray());
            timOri.Stop();
            timCust.Start();
            var custom = Shared.IO.Convert.ByteValueToCustomEncoding(byteList.ToArray(), dict);
            timCust.Stop();

            var original2 = Encoding.GetEncoding(932).GetString(byteList2.ToArray());
            var custom2 = Shared.IO.Convert.ByteValueToCustomEncoding(byteList2.ToArray(), dict);

            var original3 = Encoding.GetEncoding(932).GetString(testArray.ToArray());
            var custom3 = Shared.IO.Convert.ByteValueToCustomEncoding(testArray.ToArray(), dict);


            Console.WriteLine("HEX: 835F837E815B");
            Console.WriteLine("Original: " + original + " - Time (ms): " + timOri.ElapsedMilliseconds);
            Console.WriteLine("Custom: " + custom + " - Time (ms): " + timCust.ElapsedMilliseconds);
            Console.WriteLine("HEX: E38182");
            Console.WriteLine("Original: " + original2);
            Console.WriteLine("Custom: " + custom2);
            
            Console.WriteLine("HEX: 5F5BE1E3");
            Console.WriteLine("Original: " + original3);
            Console.WriteLine("Custom: " + custom3);
            

            Console.WriteLine();
            Console.WriteLine();
 

            Console.ReadKey();
        }

        static string CustomConverter(List<byte> byteList, Dictionary<string,string> dict)
        {
            var result = string.Empty;

            while (byteList.Count != 0)
            {
                var count = byteList.Count;

                byte[] bytes = new byte[] { };
                if (count > 2) bytes = new byte[3] {byteList[0], byteList[1], byteList[2]};
                if (count == 2) bytes = new byte[2] {byteList[0], byteList[1]};
                if (count == 1) bytes = new byte[1] {byteList[0]};

                var hexString = Shared.IO.Convert.ByteArrayToHex(bytes);
                KeyValuePair<string,string> entry = new KeyValuePair<string, string>();
                string temp;

                var a = hexString.Substring(0, 1);

                if (hexString.Length > 2)
                {
                    var b = hexString.Substring(0, 4);
                    var c = hexString.Substring(0, 2);

                    if (dict.TryGetValue(hexString, out temp)) entry = dict.Single(x => x.Key == hexString);
                    else if (dict.TryGetValue(b, out temp)) entry = dict.Single(x => x.Key == b);
                    else if (dict.TryGetValue(c, out temp)) entry = dict.Single(x => x.Key == c);
                    else if (dict.TryGetValue(a, out temp)) entry = dict.Single(x => x.Key == a);
                }

                else
                {
                    if (dict.TryGetValue(hexString, out temp)) entry = dict.Single(x => x.Key == hexString);
                    else if (dict.TryGetValue(a, out temp)) entry = dict.Single(x => x.Key == a);
                }


                byteList.RemoveRange(0, entry.Key?.Length / 2 ?? 1);

                result = result + entry.Value;
            }

            return result;
        }
    }
}
