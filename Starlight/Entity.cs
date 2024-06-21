namespace Starlight
{
	public abstract class Entity
	{

		public Entity? Parent { get; private set; }

		public virtual void Update() { }

		public virtual void LateUpdate() { }

		private List<Entity> Children = new();

		public void AddChild(Entity entity)
		{
			
			ArgumentNullException.ThrowIfNull(nameof(entity));

			if (entity.Parent != null)
			{
				throw new InvalidOperationException("The entity already has a parent.");
			}

			entity.Parent = this;
			Children.Add(entity);
		}

		public List<Entity> GetChildren() { return new List<Entity>(Children); }

		public static async void MegaUpdate(List<Entity> ents)
		{
			List<Task> updateTasks = new();
			foreach (Entity entity in ents)
			{
				updateTasks.Add(Task.Run(() => UpdateChildren(entity)));
			}

			await Task.WhenAll(updateTasks);

			foreach (Entity entity in ents)
			{
				updateTasks.Add(Task.Run(() => UpdateChildren(entity, true)));
			}

			await Task.WhenAll(updateTasks);

		}

		private static async void UpdateChildren(Entity entity, bool late = false)
		{
			if (!late) entity.Update();
			else entity.LateUpdate();

			List<Entity> children = entity.GetChildren();
			List<Task> childUpdateTasks = new();

			foreach (Entity child in children)
			{
				childUpdateTasks.Add(Task.Run(() => UpdateChildren(child, late)));
			}

			await Task.WhenAll(childUpdateTasks.ToArray());
		}

	}
}
