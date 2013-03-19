using System.Linq;

namespace TfsTaskCreator
{
    using System.Collections.Generic;

    using TfsTaskCreator.Object;

    public class StoryRepository
    {
        private static StoryRepository instance;
        private readonly Repository<Story> repo;

        private StoryRepository()
        {
            this.repo = new Repository<Story>();
        }

        public static StoryRepository Instance
        {
            get
            {
                return instance ?? (instance = new StoryRepository());
            }
        }

        public void AddStoryToRepository(Story story)
        {
            this.repo.AddToRepository(story);
        }

        public IEnumerable<Story> Repository()
        {
            return this.repo.Repo();
        }

        public Story GetStoryById(int id)
        {
            return this.repo.Repo().First(x => x.Id == id);
        }

        public void Clear()
        {
            this.repo.Repo().Clear();
        }
    }
}