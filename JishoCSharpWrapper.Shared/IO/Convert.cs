using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using JishoCSharpWrapper.Shared.Models;
using JishoCSharpWrapper.Shared.Models.API;

namespace JishoCSharpWrapper.Shared.IO
{
    internal class Convert
    {
        public static Message<RootObject> JsonToObject(string json)
        {
            Message<RootObject> message = new Message<RootObject>();

            RootObject rootObject = null;

            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(RootObject));

            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                rootObject = (RootObject)ser.ReadObject(ms);
            }

            message.SetResult(rootObject);

            
            return message;
        }
    }
}
