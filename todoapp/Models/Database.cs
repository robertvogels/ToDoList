using System.Collections.Generic;
using System.Linq;

namespace todoapp.Models
{
	/**
	 * This class represents the database that stores all data that is needed for the todoapp. Normally, a proper database
	 * would be used, but setting one up would be outside the scope of this assignment. So instead, we have this TodoDatabase
	 * class that is merely pretending to be a database.
	 */
	public class TodoDatabase
	{
		private List<TodoItem> AllTodoItems;

		public TodoDatabase(bool addDefaults = true)
		{
			if(addDefaults)
			{
				var id = 1;
				AllTodoItems = new List<TodoItem>()
				{
					new TodoItem(id++, "Implement the 'Completed' button"),
					new TodoItem(id++, "Implement the 'Delete' button"),
					new TodoItem(id++, "Make the counter at the top of the page show the correct value"),
					new TodoItem(id++, "Only show non-completed Todos in the overview"),
					new TodoItem(id++, "Add a class 'TodoCategory' to the database"),
					new TodoItem(id++, "Add some default categories to the database"),
					new TodoItem(id++, "For the to-do items in the overview, show which category they're in (if any)"),
					new TodoItem(id++, "Change the 'New to-do' feature to allow setting a category on the todo"),
					new TodoItem(id++, "Allow filtering the to-do list on a category"),
					new TodoItem(id++, "Implement the unit tests"),
				};
			}
			else
			{
				AllTodoItems = new List<TodoItem>();
			}
		}

		public List<TodoItem> GetTodos()
		{
            var list = AllTodoItems;

            switch (ViewConfigHelper.completedFilter)
            {
                case CompletedFilter.notCompleted:
                default:
                    list = list.Where(x => x.IsCompleted == false).ToList();
                    break;
                case CompletedFilter.all:
                    break;
                case CompletedFilter.completed:
                    list = list.Where(x => x.IsCompleted == true).ToList();
                    break;
            }

            switch (ViewConfigHelper.categoryFilter)
            {
                case Category.None:
                    break;
                default:
                    list = list.Where(x => x.ToDoCategory == ViewConfigHelper.categoryFilter).ToList();
                    break;
            }

            return list;
        }

		public TodoItem GetTodoById(int id)
		{
			return AllTodoItems.FirstOrDefault(x => x.TodoItemId == id);
		}

		public void AddTodo(TodoItem newTodo)
		{
			// normally, it is the job of the database to generate unique identifiers for each object.
			// but in this case we have to do it manually
			var id = 1;
			if(AllTodoItems.Any())
			{
				id = AllTodoItems.Max(x => x.TodoItemId) + 1;
			}
            
			newTodo.TodoItemId = id;
			AllTodoItems.Add(newTodo);
		}

        public void RemoveToDoItem(int id)
        {
            AllTodoItems.RemoveAll(r => r.TodoItemId == id);
        }

        public void MarkCompleted(int id)
        {
            AllTodoItems.FirstOrDefault(x => x.TodoItemId == id).IsCompleted = true;
        }
	}
}
