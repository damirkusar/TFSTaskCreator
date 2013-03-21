using System.Collections.Generic;
using System.Threading;

using Microsoft.TeamFoundation.WorkItemTracking.Client;
using TfsTaskCreator.Object;

namespace TfsTaskCreator
{
    public class WorkflowController
    {
        private readonly StoryPreparer storyPreparer;
        private readonly StoryRepository storyRepository;
        private readonly WorkItemCreator workItemCreator;

        // I need this to remove all of them after Tests Run.
        private readonly IList<WorkItem> createdWorkItems;

        public WorkflowController()
        {
            this.storyPreparer = new StoryPreparer();
            this.storyRepository = StoryRepository.Instance;
            this.workItemCreator = new WorkItemCreator();
            this.createdWorkItems = new List<WorkItem>();
        }

        public IList<WorkItem> CreatedWorkItems
        {
            get
            {
                return this.createdWorkItems;
            }
        }

        public void PrepareStories(string inputOfStories)
        {
            this.Clear();
            this.storyPreparer.PrepareStories(inputOfStories);
        }

        private void Clear()
        {
            this.createdWorkItems.Clear();
            this.storyRepository.Clear();
        }

        public IEnumerable<Story> GetAllStories()
        {
            return this.storyRepository.Repository();
        }

        public Story GetStoryById(int id)
        {
            return this.storyRepository.GetStoryById(id);
        }

        public void CreateTasks()
        {
            this.CreateNewTasks();
            //var thread = new Thread(this.CreateNewTasks);
            //thread.Start();
        }

        private void CreateNewTasks()
        {
            foreach (var story in this.GetAllStories())
            {
                foreach (var task in story.AllTasks())
                {
                    var workitem = this.workItemCreator.CreateNewTask(story.Id, task);
                    this.CreatedWorkItems.Add(workitem);
                }
            }
        }
    }
}
