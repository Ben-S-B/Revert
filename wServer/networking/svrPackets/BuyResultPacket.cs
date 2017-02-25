namespace wServer.networking.svrPackets
{
    public class BuyResultPacket : ServerPacket
    {
        public BuyResult Result { get; set; }
        public string Message { get; set; }

        public override PacketID ID
        {
            get { return PacketID.BUYRESULT; }
        }

        public override Packet CreateInstance()
        {
            return new BuyResultPacket();
        }

        protected override void Read(Client psr, NReader rdr)
        {
            Result = (BuyResult)rdr.ReadInt32();
            Message = rdr.ReadUTF();
        }

        protected override void Write(Client psr, NWriter wtr)
        {
            wtr.Write((int)Result);
            wtr.WriteUTF(Message);
        }
    }
}