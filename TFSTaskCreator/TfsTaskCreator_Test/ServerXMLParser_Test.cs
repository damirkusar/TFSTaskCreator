using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TfsTaskCreator_Test
{
    using TfsTaskCreator;

    [TestClass]
    [DeploymentItem("Settings/Server.xml")]
    public class ServerXMLParser_Test
    {
        private ServerXMLParser serverServerXmlParser;

        [TestInitialize]
        public void Setup()
        {
            this.serverServerXmlParser = new ServerXMLParser();
        }

        [TestMethod]
        public void GetTfsServer_ReadFromXmlFile_ShouldContain_defaultcollection()
        {
            string server = this.serverServerXmlParser.GetTfsServer();
            Assert.IsTrue(server.Contains("8080/tfs/defaultcollection"), "TFS Server Not Correct. Because of Security Reasons not full Server listed.");
        }

        [TestMethod]
        public void GetTeamProject_ReadFromXmlFile_ShouldContain_defaultcollection()
        {
            string server = this.serverServerXmlParser.GetTeamProject();
            Assert.IsTrue(server.Contains("e.DP.N"), "TeamProject Not Correct. Because of Security Reasons not full Server listed.");
        }
    }
}
