using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TfsTaskCreator_Test
{
    using Microsoft.TeamFoundation.WorkItemTracking.Client;

    using TfsTaskCreator;

    [TestClass]
    [DeploymentItem("Settings/Server.xml")]
    public class ServerConnector_Test
    {
        private ServerConnector serverConnector;

        [TestInitialize]
        public void Setup()
        {
            serverConnector = new ServerConnector();
        }

        [TestMethod]
        public void GetWorkItemById_GetID230088_TitleShouldBe_IntegrateIC077()
        {
            WorkItem story = serverConnector.GetWorkItemById(230088);
            Assert.AreEqual("Integrate IC 0.77", story.Title);
        }
    }
}
