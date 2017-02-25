#region

using System;
using wServer.networking.cliPackets;

#endregion

namespace wServer.networking.handlers
{
    internal class ReskinPetHandler : PacketHandlerBase<ReskinPetPacket>
    {
        public override PacketID ID
        {
            get { return PacketID.PET_CHANGE_FORM_MSG; }
        }

        protected override void HandlePacket(Client client, ReskinPetPacket packet)
        {
            throw new NotImplementedException();
        }
    }
}