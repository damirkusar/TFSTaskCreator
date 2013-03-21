using Microsoft.VisualStudio.TestTools.UnitTesting;
using TfsTaskCreator.Object;

namespace TfsTaskCreator_Test.Object
{
    using System.Collections.Generic;
    using System.Linq;

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

        [TestMethod]
        public void USP_ReadTask_ShouldBe_SST_With_StoryId()
        {
            Assert.AreEqual("[USP](12345)", this.task.USP());
        }

        [TestMethod]
        public void AllTask_GetAllTask_ShouldContain_SST_DoD_Accounting()
        {
            IEnumerable<string> allTasks = this.task.AllTasks();

            Assert.IsTrue(allTasks.Any(x => x.Contains("[DoD]")));
            Assert.IsTrue(allTasks.Any(x => x.Contains("[SST](12345)")));
            Assert.IsTrue(allTasks.Any(x => x.Contains("[USP](12345)")));
            Assert.IsTrue(allTasks.Any(x => x.Contains("[StoryName](12345)")));
        }
    }
}
