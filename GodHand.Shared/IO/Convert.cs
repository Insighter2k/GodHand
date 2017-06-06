using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Windows.Forms;
using GodHand.Shared.Models;
using GodHand.Shared.Models.Jisho;
using JishoCSharpWrapper.Shared.Models.API;

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
                if(value.Contains("\\")) value = HttpUtility.UrlEncode(value);
                var url = $"https://translate.googleapis.com/translate_a/single?client=gtx&sl=ja&tl=en&dt=t&q={value}";
                   
             
                WebClient webClient = new WebClient();
                webClient.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 " +
                                             "(KHTML, like Gecko) Chrome/41.0.2228.0 Safari/537.36");

                webClient.Encoding = Encoding.UTF8;

                var result = webClient.DownloadString(url);
                result = result.Replace(",null,null,3", "");
                result = result.Replace(",null,\"ja\"", "");
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

        public static List<Jisho> ToJisho(string value)
        {
            List<Jisho> jishoList = new List<Jisho>();
            RootObject ro = JishoCSharpWrapper.Shared.Client.RequestValuesFromJisho(value, false).Result;

            if (ro.data.Count > 0)
            {
                foreach (var item in ro.data)
                {
                    Jisho jisho = new Jisho();

                    jisho.Japanese.Reading = item.japanese[0].reading;
                    jisho.Japanese.Word = item.japanese[0].word;

                    if (item.japanese.Count > 1)
                    {
                        for (int i = 1; i < item.japanese.Count; i++)
                        {
                            jisho.OtherForms.Add(new Models.Jisho.Japanese()
                            {
                                Word = item.japanese[i].word,
                                Reading = item.japanese[i].reading
                            });
                        }
                    }

                    for (int i = 0; i < item.senses.Count; i++)
                    {
                        Sense sense = new Sense();


                        sense.EnglishDefinitions = string.Join(",", item.senses[i].english_definitions);
                        sense.PartsOfSpeech = string.Join(",", item.senses[i].parts_of_speech);
                        jisho.EnglishTranslations.Add(sense);
                    }
                    jishoList.Add(jisho);
                }

            }

            return jishoList;
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

        public static string StringToHex(string hexString)
        {
            var sb = new StringBuilder();

            var bytes = Encoding.ASCII.GetBytes(hexString);
            foreach (var t in bytes)
            {
                sb.Append(t.ToString("X2"));
            }

            return sb.ToString();
        }

        public static string HexToString(string hexString)
        {
            var bytes = new byte[hexString.Length / 2];
            for (int i = 0; i < bytes.Length; i++)
            {
                string currentHex = hexString.Substring(i * 2, 2);
                bytes[i] = System.Convert.ToByte(currentHex, 16);
            }

            return Encoding.ASCII.GetString(bytes);
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
