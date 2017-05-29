namespace GodHand.Shared.Models
{
    public class Settings
    {
        public bool EnableRomajiTranslation { get; set; }
        public bool EnableGoogleTranslation { get; set; }
        public bool EnableKanjiToAscii { get; set; }
        public bool EnableHiraganaToAscii { get; set; }
        public bool EnableKatakanaToAscii { get; set; }
        public bool EnableKigouToAscii { get; set; }
        public bool EnableJisRomanToAscii { get; set; }
        public bool EnableKanaToAscii { get; set; }
        public bool EnableGraphicToAscii { get; set; }
        public bool InsertSeparateCharacters { get; set; }
        public bool CapitalizeRomaji { get; set; }
        public bool UpscaleRomaji { get; set; }
        public bool EnableWakitagaki { get; set; }
        public bool EnableHepburn { get; set; }
    }
}
