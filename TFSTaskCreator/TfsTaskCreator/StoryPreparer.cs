using System.Linq;
using TfsTaskCreator.Object;
using System;

namespace TfsTaskCreator
{
    public class StoryPreparer
    {
        private readonly Repository<Story> storyRepository;

        public StoryPreparer()
        {
            this.storyRepository = new Repository<Story>();
        }

        public void IdSplitter(string storiesInput)
        {
            string stories = this.ReplaceCommasInStringsWithSpaces(storiesInput);

            var separators = new[] { ' ' };
            var storyIds = stories.Split(separators);
            foreach (var storyId in storyIds)
            {
                this.AddStoryToRepo(storyId);
            }
        }

        private int ParseStringToInt(string storyId)
        {
            int id;
            if (Int32.TryParse(storyId, out id))
            {
                return id;
            }
            return id;
        }

        public string ReplaceCommasInStringsWithSpaces(string input)
        {
            return input.Replace(",", " ");
        }

        private void AddStoryToRepo(string storyId)
        {
            int id = this.ParseStringToInt(storyId);

            if (id != 0)
            {
                string storyTitle = this.GetStoryTitleById(id);

                var story = new Story { Id = id, Title = storyTitle };
                this.storyRepository.AddToRepository(story);
            }
        }

        private string GetStoryTitleById(int id)
        {
            ServerConnector serverConnector = new ServerConnector();
            return serverConnector.GetWorkItemById(id).Title;
        }

        public Story GetStoryWithId(int storyId)
        {
            var stories = this.storyRepository.Repo();
            return stories.FirstOrDefault(x => x.Id == storyId);
        }
    }
}