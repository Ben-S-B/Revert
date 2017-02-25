#region

using System;
using wServer.networking.cliPackets;

#endregion

namespace wServer.networking.handlers
{
    internal class FailureHandler : PacketHandlerBase<FailurePacket>
    {
        public override PacketID ID
        {
            get { return PacketID.FAILURE; }
        }

        protected override void HandlePacket(Client client, FailurePacket packet)
        {
            throw new NotImplementedException();
        }
    }
}