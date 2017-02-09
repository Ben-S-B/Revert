#region

using db;
using MySql.Data.MySqlClient;
using System;
using System.Text;
using System.Xml;

#endregion

namespace server.friends
{
    internal class getList : RequestHandler
    {
        protected override void HandleRequest()
        {
            WriteLine("<Friends></Friends>");
            throw new NotImplementedException();
        }
    }
}
