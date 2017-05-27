using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using Kakasi.NET.Interop;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Kakasi.NET.TestWebApp.Startup))]

namespace Kakasi.NET.TestWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {               
            ConfigureAuth(app);
            ConfigureKakasi();
        }

        /// <summary>
        /// Configure Kakasi
        /// </summary>
        public void ConfigureKakasi()
        {

            /* IMPORTANT:
                The native libkakasi.dll library will look for these 2 dictionary files:
                
                itaijidict
                kanwadict

                When running as a web application, the search path used by the library will be the same as the
                running host process (e.g: iisexpress.exe, w3wp.exe) and not the path of the running web application.
                
                If it can't find any one of these file, the library will exit the running process, which is far from ideal.
                
                It's possible to set 2 environment variables that the native library will use instead of the executing path.
                
                KANWADICTPATH for the full path to the dictionary file: kanwadict
                ITAIJIDICTPATH for the full path to the dictionary file: itaijidict

                Make sure these variables are set in the running host process otherwise it won't work and any call to
                the native library will kill the running process.                
            */

            // Get the bin folder of the current app domain
            var applicationBinPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin");

            // Init library using 
            KakasiLib.Init(applicationBinPath);

            // Set params to get Furigana
            // NOTE: Use EUC-JP encoding as the wrapper will encode/decode using it
            KakasiLib.SetParams(new[] { "kakasi", "-ieuc", "-f", "-JH", "-w" });

        }
        
    }
}
