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
    public class RconClientTests
    {
        private const string host = "server.xunion.net";
        private const int port = 49477;
        private const string password = "***REMOVED***";

        [TestMethod()]
        public void RconClientTest()
        {
            RconClient client = new RconClient();

            Assert.IsInstanceOfType(client, typeof(RconClient));
            Assert.IsFalse(client.IsConnected);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConnectTestNullHost()
        {
            RconClient client = new RconClient();
            client.Connect(null, port, password);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConnectTestEmptyHost()
        {
            RconClient client = new RconClient();
            client.Connect("", port, password);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ConnectTestNegativePort()
        {
            RconClient client = new RconClient();
            client.Connect(host, -1, password);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ConnectTestInvalidPort()
        {
            RconClient client = new RconClient();
            client.Connect(host, 66666, password);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConnectTestNullPassword()
        {
            RconClient client = new RconClient();
            client.Connect(host, port, null);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConnectTestEmptyPassword()
        {
            RconClient client = new RconClient();
            client.Connect(host, port, "");
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception), AllowDerivedTypes = false)]
        public void ConnectTestInvalidHost()
        {
            RconClient client = new RconClient();

            Assert.IsTrue(client.Connect("InvalidHost", port, password));
            Assert.IsTrue(client.IsConnected);

            client.Disconnect();
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception), AllowDerivedTypes = false)]
        public void ConnectTestWrongHost()
        {
            RconClient client = new RconClient();

            Assert.IsTrue(client.Connect("google.de", port, password));
            Assert.IsTrue(client.IsConnected);

            client.Disconnect();
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception), AllowDerivedTypes = false)]
        public void ConnectTestWrongPort()
        {
            RconClient client = new RconClient();

            Assert.IsTrue(client.Connect(host, 1337, password));
            Assert.IsTrue(client.IsConnected);

            client.Disconnect();
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception), AllowDerivedTypes = false)]
        public void ConnectTestWrongPassword()
        {
            RconClient client = new RconClient();

            Assert.IsTrue(client.Connect(host, port, "password"));
            Assert.IsTrue(client.IsConnected);

            client.Disconnect();
        }

        [TestMethod()]
        public void ConnectTest()
        {
            RconClient client = new RconClient();

            Assert.IsTrue(client.Connect(host, port, password));
            Assert.IsTrue(client.IsConnected);

            client.Disconnect();
        }

        [TestMethod()]
        public void DisconnectTest()
        {
            RconClient client = new RconClient();

            Assert.IsTrue(client.Connect(host, port, password));
            Assert.IsTrue(client.IsConnected);

            client.Disconnect();

            Assert.IsFalse(client.IsConnected);
        }

        [TestMethod()]
        public void ExecuteCommandAsyncTest()
        {
            RconClient client = new RconClient();

            Assert.IsTrue(client.Connect(host, port, password));
            Assert.IsTrue(client.IsConnected);

            client.ExecuteCommandAsync(new Commands.ListPlayers(), (s, e) => Assert.IsTrue(e.Successful));

            client.Disconnect();
        }

        [TestMethod()]
        public void ExecuteLowPrioCommandAsyncTest()
        {
            RconClient client = new RconClient();

            Assert.IsTrue(client.Connect(host, port, password));
            Assert.IsTrue(client.IsConnected);

            client.ExecuteLowPrioCommandAsync(new Commands.ListPlayers(), (s, e) => Assert.IsTrue(e.Successful));

            client.Disconnect();
        }
    }
}