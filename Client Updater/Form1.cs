using System;
using System.Net;
using System.Text;
using System.Windows.Forms;
using MetroFramework.Forms;

namespace Client_Updater
{
    public partial class Form1 : MetroForm
    {
        private ClientUpdater updater;

        public Form1()
        {
            InitializeComponent();
            Load += (s, e) => Activate();
        }

        private void c453_btn_Click(object sender, EventArgs e) => runUpdater(textBoxDomain.Text);

        private void runUpdater(string ip)
        {
            if (checkBoxDownloadClient.Checked)
            {
                labelStatus.Text = "Status: Downloading latest client...";
                labelStatus.Update();
                var webCli = new WebClient();
                var clientVersion = Encoding.UTF8.GetString(webCli.DownloadData("https://realmofthemadgodhrd.appspot.com/version.txt"));
                webCli.DownloadFile($"https://realmofthemadgodhrd.appspot.com/AssembleeGameClient{clientVersion}.swf", "client.swf");
            }

            updater = new ClientUpdater(ip, labelStatus);
            updater.UpdateClient();
        }
    }
}
