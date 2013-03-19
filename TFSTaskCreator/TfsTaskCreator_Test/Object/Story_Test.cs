using Microsoft.VisualStudio.TestTools.UnitTesting;
using TfsTaskCreator.Object;

namespace TfsTaskCreator_Test.Object
{
    [TestClass]
    public class Story_Test
    {
        private Story story;

        [TestInitialize]
        public void Setup()
        {
            this.story = new Story(12345, "StoryTitle");
        }

        [TestMethod]
        public void AddTasksToCreate_Add1TaskObject_ListShouldContain_1_Object()
        {
            this.story.AddDoDTaskToStory();
            Assert.AreEqual(1, this.story.TaskCount());
        }

        [TestMethod]
        public void AddTasksToCreate_Add2TaskObject_ListShouldContain_2_Object()
        {
            this.story.AddDoDTaskToStory();
            this.story.AddAccountingTaskToStory();
            Assert.AreEqual(2, this.story.TaskCount());
        }

        [TestMethod]
        public void RepositoryContent_AddSomeTasks_VerifyIfCountCorrect_And_TaskCorrect()
        {
            this.story.AddSstTaskToStory();
            this.story.AddAccountingTaskToStory();
            Assert.AreEqual(2, this.story.TaskCount());

            var taskRepository = this.story.TaskRepository();

            foreach (var item in taskRepository)
            {
                Assert.IsTrue(item.Contains("12345"), "Item contains not ID 12345");
            }
        }
    }
}
