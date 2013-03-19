using System.Linq;
using TfsTaskCreator.Object;
using System;

namespace TfsTaskCreator
{
    public class StoryPreparer
    {
        private readonly StoryRepository storyRepository;

        public StoryPreparer()
        {
            storyRepository = StoryRepository.Instance;
        }

        public void PrepareStories(string storiesInput)
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

                if (storyTitle != string.Empty)
                {
                    var story = new Story(id, storyTitle);
                    storyRepository.AddStoryToRepository(story);
                }
            }
        }

        private string GetStoryTitleById(int id)
        {
            var workItemCreator = new WorkItemCreator();
            var workItemById = workItemCreator.GetWorkItemById(id);

            return workItemById != null ? workItemById.Title : string.Empty;

        }

        public Story GetStoryById(int storyId)
        {
            var stories = storyRepository.Repository();
            return stories.FirstOrDefault(x => x.Id == storyId);
        }
    }
}