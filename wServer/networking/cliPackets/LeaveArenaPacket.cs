using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wServer.networking.cliPackets
{
    public class LeaveArenaPacket : ClientPacket
    {
        public override PacketID ID
        {
            get { return PacketID.ACCEPT_ARENA_DEATH; }
        }

        public override Packet CreateInstance()
        {
            return new LeaveArenaPacket();
        }

        protected override void Read(Client client, NReader rdr)
        {
        }

        protected override void Write(Client client, NWriter wtr)
        {
        }
    }
}
