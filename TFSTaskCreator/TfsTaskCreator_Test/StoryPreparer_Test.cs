using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TfsTaskCreator_Test
{
    using System.Globalization;

    using TfsTaskCreator;
    using TfsTaskCreator.Object;

    [TestClass]
    public class StoryPreparer_Test
    {
        private StoryPreparer storyPreparer;

        [TestInitialize]
        public void Setup()
        {
            this.storyPreparer = new StoryPreparer();
        }

        [TestMethod]
        public void IdSplitter_1ID_RepositoryShouldContainTheCorrectID()
        {
            const int Id1 = 12345;
            this.storyPreparer.IdSplitter(Id1.ToString(CultureInfo.InvariantCulture));
            Story s = this.storyPreparer.GetStoryWithId(Id1);
            Assert.AreEqual(s.Id, Id1);
        }

        [TestMethod]
        public void IdSplitter_2IDsSeparatedWithSpace_RepositoryShouldContainTheCorrectIDs()
        {
            const int Id1 = 12345;
            const int Id2 = 23456;

            string ids = string.Format("{0} {1}", Id1, Id2);

            this.storyPreparer.IdSplitter(ids);

            Story s1 = this.storyPreparer.GetStoryWithId(Id1);
            Assert.AreEqual(s1.Id, Id1);

            Story s2 = this.storyPreparer.GetStoryWithId(Id2);
            Assert.AreEqual(s2.Id, Id2);
        }

        [TestMethod]
        public void IdSplitter_2IDsSeparatedWithComma_RepositoryShouldContainTheCorrectIDs()
        {
            const int Id1 = 12345;
            const int Id2 = 23456;

            string ids = string.Format("{0},{1}", Id1, Id2);

            this.storyPreparer.IdSplitter(ids);

            Story s1 = this.storyPreparer.GetStoryWithId(Id1);
            Assert.AreEqual(s1.Id, Id1);

            Story s2 = this.storyPreparer.GetStoryWithId(Id2);
            Assert.AreEqual(s2.Id, Id2);
        }

        [TestMethod]
        public void IdSplitter_2IDsSeparatedWithCommaAndSpaces_RepositoryShouldContainTheCorrectIDs()
        {
            const int Id1 = 12345;
            const int Id2 = 23456;

            string ids = string.Format("{0}, {1}", Id1, Id2);

            this.storyPreparer.IdSplitter(ids);

            Story s1 = this.storyPreparer.GetStoryWithId(Id1);
            Assert.AreEqual(s1.Id, Id1);

            Story s2 = this.storyPreparer.GetStoryWithId(Id2);
            Assert.AreEqual(s2.Id, Id2);
        }

        [TestMethod]
        public void IdSplitter_2IDsSeparatedWithCommaAndMoreSpaces_RepositoryShouldContainTheCorrectIDs()
        {
            const int Id1 = 12345;
            const int Id2 = 23456;

            string ids = string.Format("{0}  ,  {1}", Id1, Id2);

            this.storyPreparer.IdSplitter(ids);

            Story s1 = this.storyPreparer.GetStoryWithId(Id1);
            Assert.AreEqual(s1.Id, Id1);

            Story s2 = this.storyPreparer.GetStoryWithId(Id2);
            Assert.AreEqual(s2.Id, Id2);
        }

        [TestMethod]
        public void IdSplitter_4IDsSeparatedWithCommaAndSpaces_RepositoryShouldContainTheCorrectIDs()
        {
            const int Id1 = 12345;
            const int Id2 = 23456;
            const int Id3 = 25558;
            const int Id4 = 78889;

            string ids = string.Format("{0} , {1}, {2}    {3}", Id1, Id2, Id3, Id4);

            this.storyPreparer.IdSplitter(ids);

            Story s1 = this.storyPreparer.GetStoryWithId(Id1);
            Assert.AreEqual(s1.Id, Id1);

            Story s2 = this.storyPreparer.GetStoryWithId(Id2);
            Assert.AreEqual(s2.Id, Id2);

            Story s3 = this.storyPreparer.GetStoryWithId(Id3);
            Assert.AreEqual(s3.Id, Id3);

            Story s4 = this.storyPreparer.GetStoryWithId(Id4);
            Assert.AreEqual(s4.Id, Id4);
        }

        [TestMethod]
        public void ReplaceCommasInStrings_2IDsSeparatedWithSpaces_IdsShouldBeSeparatedByComma()
        {
            const int Id1 = 12345;
            const int Id2 = 23456;

            string input = string.Format("{0},{1}", Id1, Id2);
            string output = this.storyPreparer.ReplaceCommasInStringsWithSpaces(input);

            Assert.AreEqual(string.Format("{0} {1}", Id1, Id2), output);
        }
    }
}
