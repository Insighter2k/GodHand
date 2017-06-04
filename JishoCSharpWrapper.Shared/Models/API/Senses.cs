using System.Collections.Generic;

namespace JishoCSharpWrapper.Shared.Models.API
{
    public class Senses
    {
        public List<string> english_definitions { get; set; }
        public List<string> parts_of_speech { get; set; }
        public List<object> links { get; set; }
        public List<object> tags { get; set; }
        public List<object> restrictions { get; set; }
        public List<object> see_also { get; set; }
        public List<object> antonyms { get; set; }
        public List<object> source { get; set; }
        public List<object> info { get; set; }
        public List<object> sentences { get; set; }
    }
}
