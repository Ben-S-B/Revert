namespace wServer.networking.cliPackets
{
    public class SetConditionPacket : ClientPacket
    {
        public int ConditionEffect { get; set; }
        public float ConditionDuration { get; set; }

        public override PacketID ID
        {
            get { return PacketID.SETCONDITION; }
        }

        public override Packet CreateInstance()
        {
            return new SetConditionPacket();
        }

        protected override void Read(Client psr, NReader rdr)
        {
            ConditionEffect = (int)rdr.ReadByte();
            ConditionDuration = rdr.ReadSingle();
        }

        protected override void Write(Client psr, NWriter wtr)
        {
            wtr.Write((byte)ConditionEffect);
            wtr.Write(ConditionDuration);
        }
    }
}