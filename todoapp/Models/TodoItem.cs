namespace todoapp.Models
{
	public class TodoItem
	{
		public int TodoItemId { get; set; }

		public string Description { get; set; }
		public bool IsCompleted { get; set; }
        public Category ToDoCategory { get; set; }

		public TodoItem() { }

        public TodoItem(int id, string description, Category category = Category.Work)
		{
			this.TodoItemId = id;
			this.Description = description;
            this.ToDoCategory = category;
		}
	}
}
