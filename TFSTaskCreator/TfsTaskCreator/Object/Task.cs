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
            this.tasks.Add(this.USP());
            this.tasks.Add(this.HPQCDev());
            this.tasks.Add(this.HPQCSst());
            this.tasks.Add(this.BuildMaster());
            this.tasks.Add(this.StabilizationSST());
        }

        public int TfsId { get; private set; }
        public string StoryTitle { get; private set; }

        public string Accounting()
        {
            return string.Format("[Acc][{0}]({1})", this.StoryTitle, this.TfsId);
        }

        public string DoD()
        {
            return "[DoD]";
        }


        public string SST()
        {
            return string.Format("[SST]({0})", this.TfsId);
        }

        public string USP()
        {
            return string.Format("[USP]({0})", this.TfsId);
        }

        public IEnumerable<string> AllTasks()
        {
            return this.tasks;
        }

        public string HPQCDev()
        {
            return "[HPQC Dev]";
        }

        public string HPQCSst()
        {
            return "[HPQC SST]";
        }

        public string BuildMaster()
        {
            return "[BuildMaster]";
        }

        public string StabilizationSST()
        {
            return "[Stabilization SST]";
        }
    }
}
