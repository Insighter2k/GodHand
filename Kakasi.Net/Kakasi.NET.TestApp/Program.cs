using System.Diagnostics;
using Kakasi.NET.Interop;

namespace Kakasi.NET.TestApp
{

    /// <summary>
    /// Test program
    /// </summary>
    class Program
    {

        /// <summary>
        /// Main entry point
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {

            // Initialize library
            KakasiLib.Init();

            // Set params to get Furigana
            // NOTE: Use EUC-JP encoding as the wrapper will encode/decode using it
            KakasiLib.SetParams(new[] { "kakasi", "-ieuc", "-f", "-JH", "-w" });

            // Get furigana from this book: アンブラとパン先生 by 伊藤 綾子
            // http://itunes.apple.com/us/book/id1144287620

            // Set source sentence
            var value1 = @"この森は昼でも薄暗く、気味が悪いので、村人は誰も近づかないのでした。";

            // Get furigana
            var resultValue1 = KakasiLib.DoKakasi(value1);

            // Expected result
            // この 森[もり] は 昼[ひる] でも 薄暗く[うすぐらく] 、 気味[きみ] が 悪い[わるい] ので 、 村人[むらびと] は 誰も[だれも] 近づ[ちかづ] かないのでした 。
            Debug.Assert(resultValue1 == @"この 森[もり] は 昼[ひる] でも 薄暗く[うすぐらく] 、 気味[きみ] が 悪い[わるい] ので 、 村人[むらびと] は 誰も[だれも] 近づ[ちかづ] かないのでした 。");

            // Get furigana from this book: プカプカ島の謎 by 伊藤 綾子
            // http://itunes.apple.com/us/book/id1146306209

            // Set source sentence
            var value2 = @"この島の住人を紹介しましょう";

            // Get furigana
            var resultValue2 = KakasiLib.DoKakasi(value2);

            // Expected result
            // この 島[しま] の 住人[じゅうにん] を 紹介[しょうかい] しましょう
            Debug.Assert(resultValue2 == @"この 島[しま] の 住人[じゅうにん] を 紹介[しょうかい] しましょう");

            // Dispose of library
            KakasiLib.Dispose();            

        }

    }
}
