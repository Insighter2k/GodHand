using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Kakasi.NET.Interop;

namespace Kakasi.NET.TestWebApp.Controllers
{

    /// <summary>
    /// Kakasi API controller
    /// </summary>
    public class KakasiController : ApiController
    {

        /// <summary>
        /// Get parsed value
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string Get()
        {

            // Set source sentence            
            var value = @"この森は昼でも薄暗く、気味が悪いので、村人は誰も近づかないのでした。";

            // Get furigana
            var result = KakasiLib.DoKakasi(value);

            // Return
            return result;

        }
        
    }

    [Authorize]
    public class ValuesController : ApiController
    {

        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
