namespace TfsTaskCreator.Object
{
    public class Task
    {
        public Task(int tfsId, string storyTitle)
        {
            this.StoryTitle = storyTitle;
            this.TfsId = tfsId;
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
    }
}
