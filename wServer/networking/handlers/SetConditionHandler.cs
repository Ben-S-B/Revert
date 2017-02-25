#region

using System;
using wServer.networking.cliPackets;

#endregion

namespace wServer.networking.handlers
{
    internal class SetConditionHandler : PacketHandlerBase<SetConditionPacket>
    {
        public override PacketID ID
        {
            get { return PacketID.SETCONDITION; }
        }

        protected override void HandlePacket(Client client, SetConditionPacket packet)
        {
            throw new NotImplementedException();
        }
    }
}