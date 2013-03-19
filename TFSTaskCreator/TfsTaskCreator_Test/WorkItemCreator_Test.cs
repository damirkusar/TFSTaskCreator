﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TfsTaskCreator_Test
{
    using Microsoft.TeamFoundation.WorkItemTracking.Client;

    using Moq;

    using TfsTaskCreator;
    using TfsTaskCreator.Object;

    [TestClass]
    [DeploymentItem("Settings/Server.xml")]
    public class WorkItemCreator_Test
    {
        private WorkItemCreator workItemCreator;
        private Mock<Task> taskMock;

        [TestInitialize]
        public void Setup()
        {
            this.workItemCreator = new WorkItemCreator();
            taskMock = new Mock<Task>(232804, "[Non-Project]");

        }

        [TestMethod]
        public void GetWorkItemById_GetID230088_TitleShouldBe_IntegrateIC077()
        {
            WorkItem story = this.workItemCreator.GetWorkItemById(230088);
            Assert.AreEqual("Integrate IC 0.77", story.Title);
        }

        [TestMethod]
        public void CreateNewTask_CreateAccountingTaskForStory_232804_VerifyIfTaskExistsAndCleanupTask()
        {
            var taskName = taskMock.Object.Accounting();
            var storyId = taskMock.Object.TfsId;
            var storyTitle = taskMock.Object.StoryTitle;

            WorkItem item = this.workItemCreator.CreateNewTask(storyId, taskName);
            Assert.AreEqual(string.Format("[{0}]({1})", storyTitle, storyId), item.Title);
            this.workItemCreator.SetTaskToRemoved(item);
        }

        [TestMethod]
        public void CreateNewTask_CreateSSTTaskForStory_232804_VerifyIfTaskExistsAndCleanupTask()
        {
            var taskName = taskMock.Object.SST();
            var storyId = taskMock.Object.TfsId;

            WorkItem item = this.workItemCreator.CreateNewTask(storyId, taskName);
            Assert.AreEqual(string.Format("[SST]({0})", storyId), item.Title);
            this.workItemCreator.SetTaskToRemoved(item);
        }
    }
}