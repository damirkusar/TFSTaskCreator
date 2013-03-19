using Microsoft.VisualStudio.TestTools.UnitTesting;
using TfsTaskCreator;
using TfsTaskCreator.Object;

namespace TfsTaskCreator_Test
{
    [TestClass]
    [DeploymentItem("Server.xml")]
    public class WorkflowController_Test
    {
        private WorkflowController workflowController;
        private WorkItemCreator workItemCreator;

        [TestInitialize]
        public void Setup()
        {
            this.workflowController = new WorkflowController();
            this.workItemCreator = new WorkItemCreator();
        }

        [TestMethod]
        public void TestWorkflow_Prepare_ListAllStories_AddTaskToStory_CreateTask()
        {
            const int Id1 = 230088;
            const int Id2 = 232804;

            string ids = string.Format("{0},{1}", Id1, Id2);

            this.workflowController.PrepareStories(ids);

            Story s1 = this.workflowController.GetStoryById(Id1);
            Assert.AreEqual(Id1, s1.Id);
            Assert.AreEqual("Integrate IC 0.77", s1.Title);
            Assert.AreEqual(0, s1.TaskCount());

            Story s2 = this.workflowController.GetStoryById(Id2);
            Assert.AreEqual(Id2, s2.Id);
            Assert.AreEqual("[Non-Project]", s2.Title);
            s2.AddDoDTaskToStory();
            Assert.AreEqual(1, s2.TaskCount());

            this.workflowController.CreateTasks();
            this.RemoveCreatedTasks();
        }

        private void RemoveCreatedTasks()
        {
            foreach (var createdWorkItem in this.workflowController.CreatedWorkItems)
            {
                this.workItemCreator.SetTaskToRemoved(createdWorkItem);
            }
        }
    }
}
