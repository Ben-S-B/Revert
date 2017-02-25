#region

using db;
using MySql.Data.MySqlClient;
using System;
using System.Text;
using System.Xml;

#endregion


namespace server.ugc
{
    internal class save : RequestHandler
    {
        protected override void HandleRequest()
        {
            //POST: guid, password, name, description, width, height, mapjm, totalObjects, totalTiles, overwrite, Filename, thumbnail, Upload
            WriteErrorLine("Not implemented!");
            throw new NotImplementedException();
        }
    }
}
