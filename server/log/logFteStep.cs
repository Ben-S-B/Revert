#region

using db;
using MySql.Data.MySqlClient;
using System;
using System.Text;
using System.Xml;

#endregion

namespace server.log
{
    internal class logFteStep : RequestHandler
    {
        protected override void HandleRequest()
        {
            //TODO: track completion of the tutorial for an account
            WriteLine("<OK/>");
        }
    }
}
