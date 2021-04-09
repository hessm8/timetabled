using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Timetabled.Data {
    public class AppSettings {
        public bool AutosaveOnCategoryChange { get; set; }
        public bool UseViewerForm { get; set; }
        public string DefaultBrowser { get; set; }
        
        public void CheckDefaultBrowser(string testFilePath) {
            var browser = Process.Start(new ProcessStartInfo(testFilePath) {
                CreateNoWindow = true
            });
            DefaultBrowser = browser.ProcessName;
            browser.Kill();
        }
    }
}
