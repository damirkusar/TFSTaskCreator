namespace TfsTaskCreator.Object
{
    using System.Collections.Generic;

    public class Task
    {
        private readonly IList<string> tasks;

        public Task(int tfsId, string storyTitle)
        {
            this.StoryTitle = storyTitle;
            this.TfsId = tfsId;

            tasks = new List<string>();
            this.CreateTaskList();
        }

        private void CreateTaskList()
        {
            this.tasks.Add(this.Accounting());
            this.tasks.Add(this.DoD());
            this.tasks.Add(this.SST());
        }

        public int TfsId { get; private set; }
        public string StoryTitle { get; private set; }

        public string Accounting()
        {
            return string.Format("[{0}]({1})", this.StoryTitle, this.TfsId);
        }

        public string DoD()
        {
            return "[DoD]";
        }

        public string SST()
        {
            return string.Format("[SST]({0})", this.TfsId);
        }

        public IEnumerable<string> AllTasks()
        {
            return this.tasks;
        }
    }
}
