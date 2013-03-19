using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using TfsTaskCreator;
using TfsTaskCreator.Object;

namespace TfsTaskCreator_Test
{
    [TestClass]
    public class StoryRepository_Test
    {
        [TestMethod]
        public void AddSomeStoriesToRepo_SearchForSpecificStory_GetTheCorrectOutOfTheRepository()
        {
            StoryRepository sr = StoryRepository.Instance;
            const int Id1 = 12345;
            const int Id2 = 98765;
            const int Id3 = 75319;
            const string Title1 = "Title 1";
            const string Title2 = "Title 2";
            const string Title3 = "Title 3";

            sr.AddStoryToRepository(new Story(Id1, Title1));
            sr.AddStoryToRepository(new Story(Id2, Title2));
            sr.AddStoryToRepository(new Story(Id3, Title3));

            Story s1 = sr.GetStoryById(Id1);
            Assert.AreEqual(Id1, s1.Id);
            Assert.AreEqual(Title1, s1.Title);

            Story s3 = sr.GetStoryById(Id3);
            Assert.AreEqual(Id3, s3.Id);
            Assert.AreEqual(Title3, s3.Title);
        }
    }
}
