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
        private const string host = "server.xunion.net";
        private const int port = 49477;
        private const string password = "***REMOVED***";

        [TestMethod()]
        public void ConnectTest()
        {
            RconBase client = new RconBase();
            Assert.IsTrue(client.Connect(host, port));
            Assert.IsTrue(client.Connected);
            client.Disconnect();
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConnectTestNullHost()
        {
            RconBase client = new RconBase();
            client.Connect(null, port);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConnectTestEmptyHost()
        {
            RconBase client = new RconBase();
            client.Connect("", port);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ConnectTestNegativePort()
        {
            RconBase client = new RconBase();
            client.Connect(host, -1);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ConnectTestInvalidPort()
        {
            RconBase client = new RconBase();
            client.Connect(host, 66666);
        }

        [TestMethod()]
        public void DisconnectTest()
        {
            RconBase client = new RconBase();
            client.Connect(host, port);
            Assert.IsTrue(client.Connected);
            client.Disconnect();
            Assert.IsFalse(client.Connected);
        }

        [TestMethod()]
        public void AuthenticateTest()
        {
            RconBase client = new RconBase();
            client.Connect(host, port);
            Assert.IsTrue(client.Authenticate(password));
            Assert.IsTrue(client.Authenticated);
            client.Disconnect();
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes = false)]
        public void AuthenticateTestEmptyPassword()
        {
            RconBase client = new RconBase();
            client.Connect(host, port);
            client.Authenticate("");
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException), AllowDerivedTypes = false)]
        public void AuthenticateTestNullPassword()
        {
            RconBase client = new RconBase();
            client.Connect(host, port);
            client.Authenticate(null);
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception), AllowDerivedTypes = false)]
        public void AuthenticateTestNotConnected()
        {
            RconBase client = new RconBase();
            client.Authenticate(password);
        }

        [TestMethod()]
        public void SendReceiveTest()
        {
            RconBase client = new RconBase();
            client.Connect(host, port);
            client.Authenticate(password);
            RconPacket request = new RconPacket(PacketType.ServerdataExeccommand, new Commands.ListPlayers().ToString());
            RconPacket response = client.SendReceive(request);
            Assert.IsInstanceOfType(response, typeof(RconPacket));
            Assert.AreEqual(request.Id, response.Id);
            client.Disconnect();
        }
    }
}