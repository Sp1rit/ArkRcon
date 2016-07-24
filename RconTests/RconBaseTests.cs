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
    public class RconBaseTests
    {
        public TestContext TestContext { get; set; }

        private string Host => TestContext.Properties["Host"].ToString();
        private int Port => int.Parse(TestContext.Properties["Port"].ToString());
        private string Password => TestContext.Properties["Password"].ToString();

        [TestMethod()]
        public void ConnectTest()
        {
            RconBase client = new RconBase();
            Assert.IsTrue(client.Connect(Host, Port));
            Assert.IsTrue(client.Connected);
            client.Disconnect();
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConnectTestNullHost()
        {
            RconBase client = new RconBase();
            client.Connect(null, Port);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConnectTestEmptyHost()
        {
            RconBase client = new RconBase();
            client.Connect("", Port);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ConnectTestNegativePort()
        {
            RconBase client = new RconBase();
            client.Connect(Host, -1);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ConnectTestInvalidPort()
        {
            RconBase client = new RconBase();
            client.Connect(Host, 66666);
        }

        [TestMethod()]
        public void DisconnectTest()
        {
            RconBase client = new RconBase();
            client.Connect(Host, Port);
            Assert.IsTrue(client.Connected);
            client.Disconnect();
            Assert.IsFalse(client.Connected);
        }

        [TestMethod()]
        public void AuthenticateTest()
        {
            RconBase client = new RconBase();
            client.Connect(Host, Port);
            Assert.IsTrue(client.Authenticate(Password));
            Assert.IsTrue(client.Authenticated);
            client.Disconnect();
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes = false)]
        public void AuthenticateTestEmptyPassword()
        {
            RconBase client = new RconBase();
            client.Connect(Host, Port);
            client.Authenticate("");
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes = false)]
        public void AuthenticateTestNullPassword()
        {
            RconBase client = new RconBase();
            client.Connect(Host, Port);
            client.Authenticate(null);
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception), AllowDerivedTypes = false)]
        public void AuthenticateTestNotConnected()
        {
            RconBase client = new RconBase();
            client.Authenticate(Password);
        }

        [TestMethod()]
        public void SendReceiveTest()
        {
            RconBase client = new RconBase();
            client.Connect(Host, Port);
            client.Authenticate(Password);
            RconPacket request = new RconPacket(PacketType.ServerdataExeccommand, new Rcon.Commands.ListPlayers().ToString());
            RconPacket response = client.SendReceive(request);
            Assert.IsInstanceOfType(response, typeof(RconPacket));
            Assert.AreEqual(request.Id, response.Id);
            client.Disconnect();
        }
    }
}