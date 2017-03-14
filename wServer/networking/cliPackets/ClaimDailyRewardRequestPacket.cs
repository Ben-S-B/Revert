namespace wServer.networking.cliPackets
{
    public class ClaimDailyRewardRequestPacket : ClientPacket
    {
        public string ClaimKey { get; set; }
        public string Type { get; set; }

        public override PacketID ID
        {
            get { return PacketID.CLAIM_LOGIN_REWARD_MSG; }
        }

        public override Packet CreateInstance()
        {
            return new ClaimDailyRewardRequestPacket();
        }

        protected override void Read(Client psr, NReader rdr)
        {
            ClaimKey = rdr.ReadUTF();
            Type = rdr.ReadUTF();
        }

        protected override void Write(Client psr, NWriter wtr)
        {
            wtr.WriteUTF(ClaimKey);
            wtr.WriteUTF(Type);
        }
    }
}