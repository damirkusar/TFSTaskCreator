namespace TfsTaskCreator.Object
{
    using System.Collections.Generic;

    public class Story
    {
        private int id;
        private string title;
        private readonly Repository<string> taskRepository;

        public Story()
        {
            this.taskRepository = new Repository<string>();
        }

        public int Id
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }

        public string Title
        {
            get
            {
                return this.title;
            }
            set
            {
                this.title = value;
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
