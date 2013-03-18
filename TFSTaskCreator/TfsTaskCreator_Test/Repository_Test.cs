using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TfsTaskCreator_Test
{
    using TfsTaskCreator;

    [TestClass]
    public class Repository_Test
    {
        private Repository<string> stringRepository;

        [TestInitialize]
        public void Setup()
        {
            stringRepository = new Repository<string>();
        }

        [TestMethod]
        public void AddToRepository_AddAString_ShouldNotThrowException()
        {
            this.stringRepository.AddToRepository("test");
        }

        [TestMethod]
        public void Count_AddAString_ShouldBe_1()
        {
            this.stringRepository.AddToRepository("test");
            Assert.AreEqual(1, this.stringRepository.Count());
        }
    }
}
