namespace TfsTaskCreator
{
    using System.Collections.Generic;

    public class Repository<T>
    {
        private readonly IList<T> repository = new List<T>();

        public void AddToRepository(T item)
        {
            this.repository.Add(item);
        }

        public int Count()
        {
            return this.repository.Count;
        }

        public IList<T> Repo()
        {
            return this.repository;
        }
    }
}
