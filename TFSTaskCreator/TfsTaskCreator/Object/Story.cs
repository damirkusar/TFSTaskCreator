namespace TfsTaskCreator.Object
{
    using System.Collections.Generic;

    public class Story
    {
        private readonly int id;
        private readonly string title;
        private readonly Repository<string> taskRepository;

        public Story(int id, string title)
        {
            this.id = id;
            this.title = title;
            this.taskRepository = new Repository<string>();
        }

        public int Id
        {
            get
            {
                return this.id;
            }
        }

        public string Title
        {
            get
            {
                return this.title;
            }
        }

        public void AddTaskToCreate(string task)
        {
            this.taskRepository.AddToRepository(task);
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

    }
}
