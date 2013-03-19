using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TfsTaskCreator_Test
{
    using System.Globalization;

    using TfsTaskCreator;
    using TfsTaskCreator.Object;

    [TestClass]
    [DeploymentItem("Server.xml")]
    public class StoryPreparer_Test
    {
        private StoryPreparer storyPreparer;

        [TestInitialize]
        public void Setup()
        {
            this.storyPreparer = new StoryPreparer();
        }

        [TestMethod]
        public void PrepareStories_1ID_RepositoryShouldContainTheCorrectIDAndStoryName()
        {
            const int Id1 = 230088;
            this.storyPreparer.PrepareStories(Id1.ToString(CultureInfo.InvariantCulture));
            Story s = this.storyPreparer.GetStoryById(Id1);
            Assert.AreEqual(s.Id, Id1);
            Assert.AreEqual("Integrate IC 0.77", s.Title);
        }

        [TestMethod]
        public void PrepareStories_1ID_RepositoryShouldContainTheCorrectID()
        {
            const int Id1 = 230088;
            this.storyPreparer.PrepareStories(Id1.ToString(CultureInfo.InvariantCulture));
            Story s = this.storyPreparer.GetStoryById(Id1);
            Assert.AreEqual(s.Id, Id1);
        }

        [TestMethod]
        public void PrepareStories_2IDsSeparatedWithSpace_RepositoryShouldContainTheCorrectIDs()
        {
            const int Id1 = 230088;
            const int Id2 = 230089;

            string ids = string.Format("{0} {1}", Id1, Id2);

            this.storyPreparer.PrepareStories(ids);

            Story s1 = this.storyPreparer.GetStoryById(Id1);
            Assert.AreEqual(s1.Id, Id1);

            Story s2 = this.storyPreparer.GetStoryById(Id2);
            Assert.AreEqual(s2.Id, Id2);
        }

        [TestMethod]
        public void PrepareStories_2IDsSeparatedWithComma_RepositoryShouldContainTheCorrectIDs()
        {
            const int Id1 = 230088;
            const int Id2 = 230089;

            string ids = string.Format("{0},{1}", Id1, Id2);

            this.storyPreparer.PrepareStories(ids);

            Story s1 = this.storyPreparer.GetStoryById(Id1);
            Assert.AreEqual(s1.Id, Id1);

            Story s2 = this.storyPreparer.GetStoryById(Id2);
            Assert.AreEqual(s2.Id, Id2);
        }

        [TestMethod]
        public void PrepareStories_2IDsSeparatedWithCommaAndSpaces_RepositoryShouldContainTheCorrectIDs()
        {
            const int Id1 = 230088;
            const int Id2 = 230089;

            string ids = string.Format("{0}, {1}", Id1, Id2);

            this.storyPreparer.PrepareStories(ids);

            Story s1 = this.storyPreparer.GetStoryById(Id1);
            Assert.AreEqual(s1.Id, Id1);

            Story s2 = this.storyPreparer.GetStoryById(Id2);
            Assert.AreEqual(s2.Id, Id2);
        }

        [TestMethod]
        public void PrepareStories_2IDsSeparatedWithCommaAndMoreSpaces_RepositoryShouldContainTheCorrectIDs()
        {
            const int Id1 = 230088;
            const int Id2 = 230089;

            string ids = string.Format("{0}  ,  {1}", Id1, Id2);

            this.storyPreparer.PrepareStories(ids);

            Story s1 = this.storyPreparer.GetStoryById(Id1);
            Assert.AreEqual(s1.Id, Id1);

            Story s2 = this.storyPreparer.GetStoryById(Id2);
            Assert.AreEqual(s2.Id, Id2);
        }


        [TestMethod]
        public void PrepareStories_4IDsSeparatedWithCommaAndSpaces_RepositoryShouldContainTheCorrectIDs()
        {
            const int Id1 = 230088;
            const int Id2 = 230089;
            const int Id3 = 230090;
            const int Id4 = 136442;

            string ids = string.Format("{0} , {1}, {2}    {3}", Id1, Id2, Id3, Id4);

            this.storyPreparer.PrepareStories(ids);

            Story s1 = this.storyPreparer.GetStoryById(Id1);
            Assert.AreEqual(s1.Id, Id1);

            Story s2 = this.storyPreparer.GetStoryById(Id2);
            Assert.AreEqual(s2.Id, Id2);

            Story s3 = this.storyPreparer.GetStoryById(Id3);
            Assert.AreEqual(s3.Id, Id3);

            Story s4 = this.storyPreparer.GetStoryById(Id4);
            Assert.AreEqual(s4.Id, Id4);
        }

        [TestMethod]
        public void ReplaceCommasInStrings_2IDsSeparatedWithSpaces_IdsShouldBeSeparatedByComma()
        {
            const int Id1 = 230088;
            const int Id2 = 230089;

            string input = string.Format("{0},{1}", Id1, Id2);
            string output = this.storyPreparer.ReplaceCommasInStringsWithSpaces(input);

            Assert.AreEqual(string.Format("{0} {1}", Id1, Id2), output);
        }
    }
}
