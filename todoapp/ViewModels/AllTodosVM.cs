using System.Collections.Generic;

namespace todoapp.ViewModels
{
	public class AllTodosVM
	{
		public List<TodoVM> Todos { get; set; }
		public int Count { get; set; }
	}
}
