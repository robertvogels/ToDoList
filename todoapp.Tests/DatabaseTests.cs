using NUnit.Framework;
using todoapp.Models;
using System.Linq;
using todoapp.Controllers;
using todoapp.ViewModels;

namespace Tests
{
    [TestFixture]
    public class DatabaseTests
	{
		private TodoDatabase db;
        private TodoDatabase dbFilled;

		[SetUp]
		public void Setup()
		{
			db = new TodoDatabase(addDefaults: false);
            dbFilled = new TodoDatabase(addDefaults: true);
        }

		[Test]
		public void Test_GetAll()
		{
			// the database should start out empty
			Assert.IsEmpty(db.GetTodos());
		}

        [Test]
        public void Test_GetAllFilled()
        {
            // this database should at least have 1 record
            Assert.IsTrue(dbFilled.GetTodos().Count > 0);
        }

        [Test]
        public void Test_CreateItem()
        {
            var controller = new HomeController(db);
            var newTodoRequest = new TodoCreateRequest()
            {
                Description = "TestTest",
                ToDoCategory = "Private",
            };

            controller.Create(newTodoRequest);

            Assert.IsTrue(db.GetTodos().Count > 0);
        }

        [Test]
        public void Test_GetTodoById()
        { 
            var item = dbFilled.GetTodoById(1);
            Assert.IsInstanceOf<TodoItem>(item);
        }

        [Test]
        public void Test_MarkAsCompleted()
        {
            var firstState = dbFilled.GetTodoById(1).IsCompleted;
            dbFilled.MarkCompleted(1);

            Assert.AreNotEqual(dbFilled.GetTodoById(1).IsCompleted, firstState);
        }

        [Test]
        public void Test_RemoveToDoItem()
        {
            dbFilled.RemoveToDoItem(1);
            var item = dbFilled.GetTodoById(1);
            Assert.IsNull(item);
        }
	}
}
