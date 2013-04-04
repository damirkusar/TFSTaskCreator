using System;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TfsTaskCreator_Test
{
    using Microsoft.TeamFoundation.WorkItemTracking.Client;

    using Moq;

    using TfsTaskCreator;
    using TfsTaskCreator.Object;

    [TestClass]
    [DeploymentItem("Server.xml")]
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
        public void GetWorkItemById_GetID123_TaskNotPresentInTFS_ShouldReturnNull()
        {
            WorkItem story = this.workItemCreator.GetWorkItemById(123);
            Assert.IsNull(story);
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

        [TestMethod]
        public void TaskTypes_VerifyThatItem_PBI_IsInList()
        {
            Assert.IsTrue(this.workItemCreator.TaskTypes().Contains("Product Backlog Item"), "Product Backlog Item: is not in List");
        }

        [TestMethod]
        public void TaskTypes_VerifyThatItem_Task_IsInList()
        {
            Assert.IsTrue(this.workItemCreator.TaskTypes().Contains("Task"), "TASK: is not in List");
        }

        [TestMethod]
        public void TaskTypes_VerifyThatItem_Impediment_IsInList()
        {
            Assert.IsTrue(this.workItemCreator.TaskTypes().Contains("Impediment"), "Impediment: is not in List");
        }

        [TestMethod]
        public void TaskTypes_VerifyThatItem_DevIT_IsInList()
        {
            Assert.IsTrue(this.workItemCreator.TaskTypes().Contains("DevIT"), "DevIT: is not in List");
        }

        [TestMethod]
        public void TaskTypes_VerifyThatItem_SwIT_IsInList()
        {
            Assert.IsTrue(this.workItemCreator.TaskTypes().Contains("SwIT"), "SwIT: is not in List");
        }

        [TestMethod]
        public void CreateValidString_SendStringWithCarriageReturn_ShouldBeRemoved()
        {
            const string InvalidString = "This is the input string \n Return \n should be removed.";
            const string ValidString = "This is the input string  Return  should be removed.";

            var withoutCarriageReturn = this.workItemCreator.CreateValidTaskTitle(InvalidString);

            Assert.AreEqual(ValidString, withoutCarriageReturn);
        }

        [TestMethod]
        public void CreateValidString_SendStringWithCarriageReturnAndQuote_ShouldBeRemoved()
        {
            const string InvalidString = "Event \"Whatever\", shall:\n remove the batch\n.";
            const string ValidString = "Event Whatever, shall: remove the batch.";

            var result = this.workItemCreator.CreateValidTaskTitle(InvalidString);

            Assert.AreEqual(ValidString, result);
        }

        [TestMethod]
        public void CreateValidString_SendInvalidString_ShouldBeValid()
        {
            const string InvalidString = "\rEvent \"Whatever\", shall:\n remove the \r\nbatch\n.";
            const string ValidString = "Event Whatever, shall: remove the batch.";

            var result = this.workItemCreator.CreateValidTaskTitle(InvalidString);

            Assert.AreEqual(ValidString, result);
        }

        [TestMethod]
        public void CreateValidString_SendInvalidStringWithLengthLargerThan10_ShouldBeValidAndLength100()
        {
            const string InvalidString = "[[ATRQ30963] If the validity check for a RMC fails before the start of the run, the Analytic System shall:\n- check the availability of a different RMC:\n  - allocate a different RMC if a valid RMC is available\n  - otherwise create a task with medium pri](166274)";
            const string ValidString = "[[ATRQ30963] If the validity check for a RMC fails before the start of the run, the Analyti](166274)";

            var result = this.workItemCreator.CreateValidTaskTitle(InvalidString);

            Assert.AreEqual(ValidString, result);
            Assert.AreEqual(100, result.Length);
        }
    }
}
