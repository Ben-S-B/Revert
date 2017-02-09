#region

using System;
using System.IO;
using System.Net;

#endregion

namespace server.sfx
{
    internal class Sfx : RequestHandler
    {
        protected override void HandleRequest()
        {
            var localPath = Context.Request.Url.LocalPath;
            string file = localPath.StartsWith("/music") ? "sfx" + localPath : localPath.Substring(1);

            if (File.Exists(file))
            {
                using (FileStream i = File.OpenRead(file))
                {
                    byte[] buff = new byte[i.Length];
                    int c;
                    while ((c = i.Read(buff, 0, buff.Length)) > 0)
                        Context.Response.OutputStream.Write(buff, 0, c);
                }
            }
            else
            {
                Program.Logger.Error($"Redirecting for resource: {file}");
                Context.Response.Redirect("http://realmofthemadgodhrd.appspot.com" + localPath);
            }
        }
    }
}