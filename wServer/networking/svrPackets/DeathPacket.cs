namespace wServer.networking.svrPackets
{
    public class DeathPacket : ServerPacket
    {
        public string AccountId { get; set; }
        public int CharId { get; set; }
        public string KilledBy { get; set; }
        public int ZombieType { get; set; }
        public int ZombieId { get; set; }

        public override PacketID ID
        {
            get { return PacketID.DEATH; }
        }

        public override Packet CreateInstance()
        {
            return new DeathPacket();
        }

        protected override void Read(Client psr, NReader rdr)
        {
            AccountId = rdr.ReadUTF();
            CharId = rdr.ReadInt32();
            KilledBy = rdr.ReadUTF();
            ZombieType = rdr.ReadInt32();
            ZombieId = rdr.ReadInt32();
        }

        protected override void Write(Client psr, NWriter wtr)
        {
            wtr.WriteUTF(AccountId);
            wtr.Write(CharId);
            wtr.WriteUTF(KilledBy);
            wtr.Write(ZombieType);
            wtr.Write(ZombieId);
        }
    }
}