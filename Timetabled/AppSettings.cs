using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Timetabled {
    public class AppSettings {
        public string DefaultBrowser { get; private set; }
        public void CheckDefaultBrowser(string testFilePath) {
            var browser = Process.Start(new ProcessStartInfo(testFilePath) {
                CreateNoWindow = true
            });
            DefaultBrowser = browser.ProcessName;
            browser.Kill();
        }
    }
}
