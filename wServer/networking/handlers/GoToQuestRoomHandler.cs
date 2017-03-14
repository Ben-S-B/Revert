#region

using System;
using wServer.networking.cliPackets;

#endregion

namespace wServer.networking.handlers
{
    internal class GoToQuestRoomHandler : PacketHandlerBase<GoToQuestRoomPacket>
    {
        public override PacketID ID
        {
            get { return PacketID.QUEST_ROOM_MSG; }
        }

        protected override void HandlePacket(Client client, GoToQuestRoomPacket packet)
        {
            throw new NotImplementedException();
        }
    }
}