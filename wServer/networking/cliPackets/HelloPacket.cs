namespace wServer.networking.cliPackets
{
    public class HelloPacket : ClientPacket
    {
        public string BuildVersion { get; set; }
        public int GameId { get; set; }
        public string GUID { get; set; }
        public string Password { get; set; }
        public string Secret { get; set; }
        public int randomint1 { get; set; }
        public int KeyTime { get; set; }
        public byte[] Key { get; set; }
        public byte[] MapInfo { get; set; }
        public string EntryTag { get; set; }
        public string GameNet { get; set; }
        public string GameNetUserId { get; set; }
        public string PlayPlatform { get; set; }
        public string PlatformToken { get; set; }
        public string UserToken { get; set; }

        public override PacketID ID
        {
            get { return PacketID.HELLO; }
        }

        public override Packet CreateInstance()
        {
            return new HelloPacket();
        }

        protected override void Read(Client psr, NReader rdr)
        {
            BuildVersion = rdr.ReadUTF();
            GameId = rdr.ReadInt32();
            GUID = RSA.Instance.Decrypt(rdr.ReadUTF());
            rdr.ReadInt32();
            Password = RSA.Instance.Decrypt(rdr.ReadUTF());
            randomint1 = rdr.ReadInt32();
            Secret = RSA.Instance.Decrypt(rdr.ReadUTF());
            KeyTime = rdr.ReadInt32();
            Key = rdr.ReadBytes(rdr.ReadInt16());
            MapInfo = rdr.ReadBytes(rdr.ReadInt32());
            EntryTag = rdr.ReadUTF();
            GameNet = rdr.ReadUTF();
            GameNetUserId = rdr.ReadUTF();
            PlayPlatform = rdr.ReadUTF();
            PlatformToken = rdr.ReadUTF();
            UserToken = rdr.ReadUTF();
        }

        protected override void Write(Client psr, NWriter wtr)
        {
            wtr.WriteUTF(BuildVersion);
            wtr.Write(GameId);
            wtr.Write(0);
            wtr.WriteUTF(RSA.Instance.Encrypt(GUID));
            wtr.Write(randomint1);
            wtr.WriteUTF(RSA.Instance.Encrypt(Password));
            wtr.WriteUTF(RSA.Instance.Encrypt(Secret));
            wtr.Write(KeyTime);
            wtr.Write((ushort)Key.Length);
            wtr.Write(Key);
            wtr.Write(MapInfo.Length);
            wtr.Write(MapInfo);
            wtr.WriteUTF(EntryTag);
            wtr.WriteUTF(GameNet);
            wtr.WriteUTF(GameNetUserId);
            wtr.WriteUTF(PlayPlatform);
            wtr.WriteUTF(PlatformToken);
            wtr.WriteUTF(UserToken);
        }
    }
}