using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace FlashPlayerTrust
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var file = new FileInfo(Path.Combine(
                        Environment.GetFolderPath(
                        Environment.SpecialFolder.ApplicationData),
                        @"Macromedia\Flash Player\#Security\FlashPlayerTrust\winetrust.cfg"));
                file.Directory.Create();
                File.WriteAllText(file.FullName,
                    Path.GetPathRoot(AppDomain.CurrentDomain.BaseDirectory),
                    Encoding.UTF8);
                MessageBox.Show("Success!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
