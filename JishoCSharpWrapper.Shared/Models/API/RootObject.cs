using System.Collections.Generic;

namespace JishoCSharpWrapper.Shared.Models.API
{
    public class RootObject
    {
        public Meta meta { get; set; }
        public List<Data> data { get; set; }
    }
}
