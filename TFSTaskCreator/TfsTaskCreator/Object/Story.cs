namespace TfsTaskCreator.Object
{
    using System.Collections.Generic;

    public class Story
    {
        private readonly Repository<string> taskRepository;
        private readonly Task task;

        public Story(int id, string title)
        {
            this.Id = id;
            this.Title = title;

            this.taskRepository = new Repository<string>();
            this.task = new Task(this.Id, this.Title);
        }

        public int Id { get; set; }
        public string Title { get; set; }

        public IEnumerable<string> AllTasks()
        {
            return this.taskRepository.Repo();
        }

        public int TaskCount()
        {
            return this.taskRepository.Count();
        }

        public IEnumerable<string> TaskRepository()
        {
            IEnumerable<string> repo = this.taskRepository.Repo();
            return repo;
        }

        public void AddAccountingTaskToStory()
        {
            this.taskRepository.AddToRepository(this.task.Accounting());
        }

        public void AddDoDTaskToStory()
        {
            this.taskRepository.AddToRepository(this.task.DoD());
        }

        public void AddSstTaskToStory()
        {
            this.taskRepository.AddToRepository(this.task.SST());
        }

        public void AddUspTaskToStory()
        {
            this.taskRepository.AddToRepository(this.task.USP());
        }

        public override string ToString()
        {
            return string.Format("{0}: {1}", this.Id, this.Title);
        }
    }
}
