#region

using System;
using wServer.networking.cliPackets;

#endregion

namespace wServer.networking.handlers
{
    internal class ClaimDailyRewardRequestHandler : PacketHandlerBase<ClaimDailyRewardRequestPacket>
    {
        public override PacketID ID
        {
            get { return PacketID.CLAIM_LOGIN_REWARD_MSG; }
        }

        protected override void HandlePacket(Client client, ClaimDailyRewardRequestPacket packet)
        {
            throw new NotImplementedException();
        }
    }
}