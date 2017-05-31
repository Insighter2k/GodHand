using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using GodHand.Shared.Models;

namespace GodHand.Shared.IO
{
    public class Convert
    {
        private static string[] KakasiParamshelper(Shared.Models.Settings settings)
        {
            List<string> list = new List<string>();
            list.Add("kakasi");
            if(settings.CapitalizeRomaji) list.Add("-C");
            list.Add(settings.EnableHepburn ? "-rhepburn" : "-rkunrei");
            if (settings.EnableGraphicToAscii) list.Add("-ga");
            if (settings.EnableHiraganaToAscii) list.Add("-Ha");
            if (settings.EnableWakitagaki) list.Add("-w");
            if (settings.EnableJisRomanToAscii) list.Add("-ja");
            if (settings.EnableKanaToAscii) list.Add("-ka");
            if (settings.EnableKatakanaToAscii) list.Add("-Ka");
            if (settings.EnableKigouToAscii) list.Add("-Ea");
            if (settings.EnableKanjiToAscii) list.Add("-Ja");
            if (settings.InsertSeparateCharacters) list.Add("-s");
            if (settings.UpscaleRomaji) list.Add("-U");

            return list.ToArray();
        }

        public static string ToRomaji(string value, Settings settings)
        {
            Kakasi.NET.Interop.KakasiLib.Init();
            Kakasi.NET.Interop.KakasiLib.SetParams(KakasiParamshelper(settings));

            var result = Kakasi.NET.Interop.KakasiLib.DoKakasi(value);

            Kakasi.NET.Interop.KakasiLib.Dispose();

            return result;
        }

        private static Regex regEx = new Regex("\\[\\\"(.+?)\\\"\\]", RegexOptions.Compiled);
        public static string ToEnglish(string value)
        {
            string filteredResult = string.Empty;
            try
            {
                var url = $"https://translate.googleapis.com/translate_a/single?client=gtx&sl=ja&tl=en&dt=t&q={value}";

                WebClient webClient = new WebClient();
                webClient.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0");
                webClient.Headers.Add(HttpRequestHeader.AcceptCharset, "UTF-8");

                webClient.Encoding = System.Text.Encoding.UTF8;

                var result = webClient.DownloadString(url);
                result = result.Replace(",null,null,3", "");
                result = result.Replace(",null,\"ja\"", "");
                result = result.Replace("\\n", "");
                result = result.Replace("\\n", "");

                MatchCollection matchCollection = regEx.Matches(result);

                for (int i = 0; i < matchCollection.Count; i++)
                {

                    filteredResult = filteredResult +
                                     string.Join(" - ",
                                         matchCollection[i]
                                             .Value.Split(new string[] {"\",\""}, StringSplitOptions.None)) +
                                     Environment.NewLine;
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("There has been an error:" + Environment.NewLine +
                                "Convert - ToEnglish" + Environment.NewLine + ex);
            }

            return filteredResult;
        }

        public static string ByteArrayToHex(byte[] bytes)
        {            
                StringBuilder Result = new StringBuilder(bytes.Length * 2);
                string HexAlphabet = "0123456789ABCDEF";

                foreach (byte B in bytes)
                {
                    Result.Append(HexAlphabet[(int)(B >> 4)]);
                    Result.Append(HexAlphabet[(int)(B & 0xF)]);
                }

                return Result.ToString();
           
        }

        private static string _hexString = null;
        public static string ByteValueToCustomEncoding(byte[] byteArray, Dictionary<string, string> dict)
        {
            var result = string.Empty;

            _hexString = Shared.IO.Convert.ByteArrayToHex(byteArray);

            while (_hexString.Length > 0)
            {
                string entry = string.Empty;

                if (_hexString.Length >= 6)
                {
                    entry = Helper(dict);
                }

                else if (_hexString.Length >= 4)
                {
                    entry = Helper(dict);
                }

                else if (_hexString.Length >= 2)
                {
                    entry = Helper(dict);
                }

                else if (_hexString.Length >= 1)
                {
                    entry = Helper(dict);
                }
                if (entry == "\\n") entry = entry.Replace("\\n", Environment.NewLine);
                if (entry == "\\e") entry = entry.Replace("\\e", ((Char) 3).ToString());
                result = result + entry;
            }

            return result;
        }

        private static string Helper(Dictionary<string,string> dict)
        {
            string temp = String.Empty;
            
            for (int i = (_hexString.Length > 6) ? 6: _hexString.Length; i >= 0; i = i - 2)
            {
                if (dict.TryGetValue(_hexString.Substring(0, (i == 0) ? 1 : i), out temp))
                {
                    _hexString = _hexString.Remove(0, i);
                    return temp;
                }
            }

            _hexString = _hexString.Remove(0, 1);

            return temp;
        }
    }
}
