namespace wServer.networking.svrPackets
{
    public class ArenaNextWavePacket : ServerPacket
    {
        public int CurrentRuntime { get; set; }

        public override PacketID ID
        {
            get { return PacketID.IMMINENT_ARENA_WAVE; }
        }

        public override Packet CreateInstance()
        {
            return new ArenaNextWavePacket();
        }

        protected override void Read(Client client, NReader rdr)
        {
            CurrentRuntime = rdr.ReadInt32();
        }

        protected override void Write(Client client, NWriter wtr)
        {
            wtr.Write(CurrentRuntime);
        }
    }
}