using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Web.WebView2.Core;
using Microsoft.Win32;

namespace Timetabled.Forms {
    public partial class ScheduleViewer : Form {
        string URL { get; }
        readonly Size ButtonSize;
        public ScheduleViewer(string url) {
            InitializeComponent();
            URL = url;
            ButtonSize = PrintButton.Size;
            ScheduleViewer_Resize(this, null);
            InitializeAsync();
        }

        async void InitializeAsync() {
            await webView.EnsureCoreWebView2Async();
            webView.CoreWebView2.Navigate(URL);
            Show();
        }

        private void ScheduleViewer_Resize(object sender, EventArgs e) {
            webView.Size = ClientSize - new Size(webView.Location);

            double multiplier = 1 + (ClientSize.Width - 1000) * 0.01;
            PrintButton.Width = ButtonSize.Width;

            PrintButton.Left = ClientSize.Width - PrintButton.Width - 15;
        }

        private async void PrintButton_Click(object sender, EventArgs e) {
            PrintButton.Visible = false;
            await webView.CoreWebView2.ExecuteScriptAsync("window.print();");
            PrintButton.Visible = true;
        }

        public static bool WebViewIsInstalled() {
            string regKey = @"SOFTWARE\WOW6432Node\Microsoft\EdgeUpdate\Clients";
            using (RegistryKey edgeKey = Registry.LocalMachine.OpenSubKey(regKey)) {
                if (edgeKey != null) {
                    string[] productKeys = edgeKey.GetSubKeyNames();
                    if (productKeys.Any()) {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
