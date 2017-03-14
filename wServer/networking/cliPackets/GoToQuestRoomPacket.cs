namespace wServer.networking.cliPackets
{
    public class GoToQuestRoomPacket : ClientPacket
    {
        public override PacketID ID
        {
            get { return PacketID.QUEST_ROOM_MSG; }
        }

        public override Packet CreateInstance()
        {
            return new GoToQuestRoomPacket();
        }

        protected override void Read(Client psr, NReader rdr)
        {
        }

        protected override void Write(Client psr, NWriter wtr)
        {
        }
    }
}