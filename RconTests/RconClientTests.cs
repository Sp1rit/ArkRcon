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
        public TestContext TestContext { get; set; }

        private string Host => TestContext.Properties["Host"].ToString();
        private int Port => int.Parse(TestContext.Properties["Port"].ToString());
        private string Password => TestContext.Properties["Password"].ToString();


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
            client.Connect(null, Port, Password);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConnectTestEmptyHost()
        {
            RconClient client = new RconClient();
            client.Connect("", Port, Password);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ConnectTestNegativePort()
        {
            RconClient client = new RconClient();
            client.Connect(Host, -1, Password);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ConnectTestInvalidPort()
        {
            RconClient client = new RconClient();
            client.Connect(Host, 66666, Password);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConnectTestNullPassword()
        {
            RconClient client = new RconClient();
            client.Connect(Host, Port, null);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConnectTestEmptyPassword()
        {
            RconClient client = new RconClient();
            client.Connect(Host, Port, "");
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception), AllowDerivedTypes = false)]
        public void ConnectTestInvalidHost()
        {
            RconClient client = new RconClient();

            Assert.IsTrue(client.Connect("InvalidHost", Port, Password));
            Assert.IsTrue(client.IsConnected);

            client.Disconnect();
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception), AllowDerivedTypes = false)]
        public void ConnectTestWrongHost()
        {
            RconClient client = new RconClient();

            Assert.IsTrue(client.Connect("google.de", Port, Password));
            Assert.IsTrue(client.IsConnected);

            client.Disconnect();
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception), AllowDerivedTypes = false)]
        public void ConnectTestWrongPort()
        {
            RconClient client = new RconClient();

            Assert.IsTrue(client.Connect(Host, 1337, Password));
            Assert.IsTrue(client.IsConnected);

            client.Disconnect();
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception), AllowDerivedTypes = false)]
        public void ConnectTestWrongPassword()
        {
            RconClient client = new RconClient();

            Assert.IsTrue(client.Connect(Host, Port, "password"));
            Assert.IsTrue(client.IsConnected);

            client.Disconnect();
        }

        [TestMethod()]
        public void ConnectTest()
        {
            RconClient client = new RconClient();

            Assert.IsTrue(client.Connect(Host, Port, Password));
            Assert.IsTrue(client.IsConnected);

            client.Disconnect();
        }

        [TestMethod()]
        public void DisconnectTest()
        {
            RconClient client = new RconClient();

            Assert.IsTrue(client.Connect(Host, Port, Password));
            Assert.IsTrue(client.IsConnected);

            client.Disconnect();

            Assert.IsFalse(client.IsConnected);
        }

        [TestMethod()]
        public void ExecuteCommandAsyncTest()
        {
            RconClient client = new RconClient();

            Assert.IsTrue(client.Connect(Host, Port, Password));
            Assert.IsTrue(client.IsConnected);

            client.ExecuteCommandAsync(new Rcon.Commands.ListPlayers(), (s, e) => Assert.IsTrue(e.Successful));

            client.Disconnect();
        }

        [TestMethod()]
        public void ExecuteLowPrioCommandAsyncTest()
        {
            RconClient client = new RconClient();

            Assert.IsTrue(client.Connect(Host, Port, Password));
            Assert.IsTrue(client.IsConnected);

            client.ExecuteLowPrioCommandAsync(new Rcon.Commands.ListPlayers(), (s, e) => Assert.IsTrue(e.Successful));

            client.Disconnect();
        }
    }
}