namespace wServer.networking.cliPackets
{
    public class ReskinPetPacket : ClientPacket
    {
        public int PetInstanceId { get; set; }
        public int PickedNewPetType { get; set; }
        public ObjectSlot Item { get; set; }

        public override PacketID ID
        {
            get { return PacketID.PET_CHANGE_FORM_MSG; }
        }

        public override Packet CreateInstance()
        {
            return new ReskinPetPacket();
        }

        protected override void Read(Client psr, NReader rdr)
        {
            PetInstanceId = rdr.ReadInt32();
            PickedNewPetType = rdr.ReadInt32();
            Item = ObjectSlot.Read(psr, rdr);
        }

        protected override void Write(Client psr, NWriter wtr)
        {
            wtr.Write(PetInstanceId);
            wtr.Write(PickedNewPetType);
            Item.Write(psr, wtr);
        }
    }
}