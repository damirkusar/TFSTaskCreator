using Microsoft.VisualStudio.TestTools.UnitTesting;
using TfsTaskCreator.Object;

namespace TfsTaskCreator_Test.Object
{
    [TestClass]
    public class Task_Test
    {
        private Task task;

        [TestInitialize]
        public void Setup()
        {
            this.task = new Task(12345, "StoryName");
        }

        [TestMethod]
        public void Accounting_ReadTask_ShouldBe_StoryName_With_StoryID()
        {
            Assert.AreEqual("[StoryName](12345)", this.task.Accounting());
        }

        [TestMethod]
        public void DoD_ReadTask_ShouldBe_DoD()
        {
            Assert.AreEqual("[DoD]", this.task.DoD());
        }

        [TestMethod]
        public void SST_ReadTask_ShouldBe_SST_With_StoryId()
        {
            Assert.AreEqual("[SST](12345)", this.task.SST());
        }
    }
}
