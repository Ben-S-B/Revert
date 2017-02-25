#region

using System;
using wServer.networking.cliPackets;

#endregion

namespace wServer.networking.handlers
{
    internal class KeyInfoRequestHandler : PacketHandlerBase<KeyInfoRequestPacket>
    {
        public override PacketID ID
        {
            get { return PacketID.KEY_INFO_REQUEST; }
        }

        protected override void HandlePacket(Client client, KeyInfoRequestPacket packet)
        {
            throw new NotImplementedException();
        }
    }
}