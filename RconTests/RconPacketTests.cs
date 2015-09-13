using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rcon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rcon.Tests
{
    [TestClass()]
    public class RconPacketTests
    {
        [TestMethod()]
        public void RconPacketTest()
        {
            RconPacket packet = new RconPacket(PacketType.ServerdataExeccommand, "Body");
            Assert.IsTrue(packet.Id >= 0);
            Assert.AreEqual(PacketType.ServerdataExeccommand, packet.Type);
            Assert.AreEqual("Body", packet.Body);
            Assert.AreEqual("Body".Length + 10, packet.Size);
            Assert.AreEqual(packet.Size + 4, ((byte[])packet).Length);
            
            //Assert.AreSame(new byte[] { 14, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 66, 111, 100, 121, 0, 0 }, (byte[])packet);
        }
    }
}