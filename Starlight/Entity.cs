namespace Starlight
{
    public abstract class Entity
    {
        public virtual void Update() { }

        public virtual void LateUpdate() { }

        private List<Entity> Children = new();

        public void AddChild(Entity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            Children.Add(entity);
        }

        public List<Entity> GetChildren() { return new List<Entity>(Children); }

        public static async void MegaUpdate(List<Entity> ents)
        {
            List<Task> updateTasks = new List<Task>();
            foreach (Entity entity in ents)
            {
                updateTasks.Add(Task.Run(() => UpdateChildren(entity)));
            }

            await Task.WhenAll(updateTasks);
        }

        private static async void UpdateChildren(Entity entity)
        {
            entity.Update();

            List<Entity> children = entity.GetChildren();
            List<Task> childUpdateTasks = new List<Task>();

            foreach (Entity child in children)
            {
                childUpdateTasks.Add(Task.Run(() => UpdateChildren(child)));
            }

            await Task.WhenAll(childUpdateTasks.ToArray());
        }

    }
}
