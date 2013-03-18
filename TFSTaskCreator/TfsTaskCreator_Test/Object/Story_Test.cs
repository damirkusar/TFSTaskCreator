namespace TfsTaskCreator_Test.Object
{
    using System.Collections;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using TfsTaskCreator.Object;

    [TestClass]
    public class Story_Test
    {
        private Story story;
        private Task task;


        [TestInitialize]
        public void Setup()
        {
            this.story = new Story { Id = 12345, Title = "StoryTitle" };
            this.task = new Task(this.story.Id, this.story.Title);
        }

        [TestMethod]
        public void AddTasksToCreate_Add1TaskObject_ListShouldContain_1_Object()
        {
            this.story.AddTaskToCreate(this.task.DoD());
            Assert.AreEqual(1, this.story.TaskCount());
        }

        [TestMethod]
        public void AddTasksToCreate_Add2TaskObject_ListShouldContain_2_Object()
        {
            this.story.AddTaskToCreate(this.task.DoD());
            this.story.AddTaskToCreate(this.task.Accounting());
            Assert.AreEqual(2, this.story.TaskCount());
        }

        [TestMethod]
        public void RepositoryContent_AddSomeTasks_VerifyIfCountCorrect_And_TaskCorrect()
        {
            this.story.AddTaskToCreate(this.task.SST());
            this.story.AddTaskToCreate(this.task.Accounting());
            Assert.AreEqual(2, this.story.TaskCount());

            var taskRepository = this.story.TaskRepository();

            foreach (var item in taskRepository)
            {
                Assert.IsTrue(item.Contains("12345"), "Item contains not ID 12345");
            }
        }
    }
}
