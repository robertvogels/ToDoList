using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using todoapp.Models;
using todoapp.ViewModels;
using System.Web;

namespace todoapp.Controllers
{
    public class HomeController : Controller
    {
        private readonly TodoDatabase db;

		public HomeController(TodoDatabase db)
		{
            this.db = db;
		}

        [HttpGet]
		public IActionResult Index()
        {
            var vmList = (from todo in db.GetTodos()
                        select new TodoVM()
                        {
                            Description = todo.Description,
                            TodoId = todo.TodoItemId,
                            ToDoCategory = todo.ToDoCategory.ToString()
                        }).ToList();

            var vm = new AllTodosVM()
            {
                Todos = vmList,
                Count = vmList.Count
            };

            return View(vm);
        }

        public IActionResult SetCompletedFilter(int id)
        {
            switch (id)
            {
                default:
                case 0:
                    ViewConfigHelper.completedFilter = CompletedFilter.notCompleted;
                    ViewConfigHelper.categoryFilter = Category.None;
                    break;
                case 1:
                    ViewConfigHelper.completedFilter = CompletedFilter.all;
                    break;
                case 2:
                    ViewConfigHelper.completedFilter = CompletedFilter.completed;
                    ViewConfigHelper.categoryFilter = Category.None;
                    break;
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult SetCategoryFilter(CategoryFilterRequest request)
        {
            ViewConfigHelper.categoryFilter = ViewConfigHelper.categoryStringToEnum[request.Selection];

            return RedirectToAction("Index");
        }

        [HttpPost]
		public IActionResult EditDescription(int id, TodoEditDescriptionRequest request)
		{
			var todo = db.GetTodoById(id);
			if (todo == null) return NotFound();

			todo.Description = request.Description;

			return RedirectToAction("Index");
		}

		[HttpPost]
		public IActionResult MarkAsCompleted(int id)
		{
            db.MarkCompleted(id);

            return RedirectToAction("Index");
		}

		[HttpPost]
		public IActionResult Delete(int id)
		{
            db.RemoveToDoItem(id);

            return RedirectToAction("Index");
        }

		[HttpPost]
		public IActionResult Create(TodoCreateRequest request)
		{
            var newTodo = new TodoItem()
            {
                Description = request.Description,
                ToDoCategory = ViewConfigHelper.categoryStringToEnum[request.ToDoCategory]
            };
            db.AddTodo(newTodo);

            return RedirectToAction("Index");
		}

	}
}
