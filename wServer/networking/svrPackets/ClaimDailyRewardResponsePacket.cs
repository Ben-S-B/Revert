namespace wServer.networking.svrPackets
{
    public class ClaimDailyRewardResponsePacket : ServerPacket
    {
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public int Gold { get; set; }

        public override PacketID ID
        {
            get { return PacketID.LOGIN_REWARD_MSG; }
        }

        public override Packet CreateInstance()
        {
            return new ClaimDailyRewardResponsePacket();
        }

        protected override void Read(Client psr, NReader rdr)
        {
            ItemId = rdr.ReadInt32();
            Quantity = rdr.ReadInt32();
            Gold = rdr.ReadInt32();
        }

        protected override void Write(Client psr, NWriter wtr)
        {
            wtr.Write(ItemId);
            wtr.Write(Quantity);
            wtr.Write(Gold);
        }
    }
}