using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wServer.realm.entities.player;

namespace wServer.realm.commands
{
    //http://www.mpgh.net/forum/showthread.php?t=1022217
    internal class SetGoldCommand : Command
    {
        public SetGoldCommand() : base("setgold", 1) { }

        protected override bool Process(Player player, RealmTime time, string[] args)
        {
            if (string.IsNullOrEmpty(args[0]))
            {
                player.SendHelp("Usage: /setgold <gold>");
                return false;
            }
            player.Manager.Database.DoActionAsync(db =>
            {
                var cmd = db.CreateQuery();
                cmd.CommandText = "UPDATE `stats` SET `credits`=@cre WHERE accId=@accId";
                cmd.Parameters.AddWithValue("@cre", args[0]);
                cmd.Parameters.AddWithValue("@accId", player.AccountId);
                if (cmd.ExecuteNonQuery() == 0)
                {
                    player.SendError("Error setting gold!");
                }
                else
                {
                    player.SendInfo("Success!");
                }
            });
            return true;
        }
    }
}
