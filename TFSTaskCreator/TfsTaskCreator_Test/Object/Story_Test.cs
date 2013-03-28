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
        public void AddDoDTaskToStory_AddTask_ListShouldContain_1_Object()
        {
            this.story.AddDoDTaskToStory();
            Assert.AreEqual(1, this.story.TaskCount());
        }

        [TestMethod]
        public void AddSstTaskToStory_AddTask_ListShouldContain_1_Object()
        {
            this.story.AddSstTaskToStory();
            Assert.AreEqual(1, this.story.TaskCount());
        }

        [TestMethod]
        public void AddAccountingTaskToStory_AddTask_ListShouldContain_1_Object()
        {
            this.story.AddAccountingTaskToStory();
            Assert.AreEqual(1, this.story.TaskCount());
        }

        [TestMethod]
        public void AddUspTaskToStory_AddTask_ListShouldContain_1_Object()
        {
            this.story.AddUspTaskToStory();
            Assert.AreEqual(1, this.story.TaskCount());
        }

        [TestMethod]
        public void AddStabilizationSstTaskToStory_AddTask_ListShouldContain_1_Object()
        {
            this.story.AddStabilizationSstTaskToStory();
            Assert.AreEqual(1, this.story.TaskCount());
        }

        [TestMethod]
        public void AddBuildmasterTaskToStory_AddTask_ListShouldContain_1_Object()
        {
            this.story.AddBuildmasterTaskToStory();
            Assert.AreEqual(1, this.story.TaskCount());
        }

        [TestMethod]
        public void AddHPQCSstTaskToStory_AddTask_ListShouldContain_1_Object()
        {
            this.story.AddHPQCSstTaskToStory();
            Assert.AreEqual(1, this.story.TaskCount());
        }

        [TestMethod]
        public void AddHPQCDevTaskToStory_AddTask_ListShouldContain_1_Object()
        {
            this.story.AddHPQCDevTaskToStory();
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
        public void ToString_VerifyCorrectToStringOutput()
        {
            this.story.AddDoDTaskToStory();
            this.story.AddAccountingTaskToStory();
            Assert.AreEqual("12345: StoryTitle", this.story.ToString());
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
