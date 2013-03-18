namespace TfsTaskCreator.Object
{
    public class Task
    {
        private readonly string storyTitle;
        private readonly int tfsId;

        public Task(int tfsId, string storyTitle)
        {
            this.storyTitle = storyTitle;
            this.tfsId = tfsId;
        }

        public string Accounting()
        {
            return string.Format("[{0}]({1})", this.storyTitle, this.tfsId);
        }

        public string DoD()
        {
            return "[DoD]";
        }

        public string SST()
        {
            return string.Format("[SST]({0})", this.tfsId);
        }
    }
}
