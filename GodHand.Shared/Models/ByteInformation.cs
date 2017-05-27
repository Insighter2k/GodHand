namespace GodHand.Shared.Models
{
    public class ByteInformation
    {
        public byte[] ByteValue { get; }
        public int ByteValueLength => ByteValue.Length;
        public int StartPosition { get; }
        public string CurrentValue { get; }
        public string NewValue { get; set; }
        public int NewValueLength => NewValue.Length;
        public string RomajiTranslation { get; }
        public bool HasChange { get; set; }

        public ByteInformation(byte[] byteValue, int startPosition, string currentValue)
        {
            ByteValue = byteValue;
            StartPosition = startPosition;
            CurrentValue = currentValue;
            NewValue = currentValue;
            RomajiTranslation = Kakasi.NET.Interop.KakasiLib.DoKakasi(currentValue);
            HasChange = false;
        }
    }
}
