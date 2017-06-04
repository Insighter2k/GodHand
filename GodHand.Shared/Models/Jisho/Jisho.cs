using System.Collections.Generic;

namespace GodHand.Shared.Models.Jisho
{
    public class Jisho
    {
        public Japanese Japanese { get; set; }
        public List<Sense> EnglishTranslations { get; set; }
        public List<Japanese> OtherForms { get; set; }

        public Jisho()
        {
            Japanese = new Japanese();
            EnglishTranslations = new List<Sense>();
            OtherForms = new List<Japanese>();
        }
    }
}
