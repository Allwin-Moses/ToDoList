using Microsoft.AspNetCore.Mvc;
using Github_project_1.Models;

namespace Github_project_1.Controllers
{
    public class TodoController : Controller
    {


        private readonly DataAccess _dataAccess;

        public TodoController(DataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public IActionResult View1()
        {
            return View(_dataAccess.GetAllTasks());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add([FromForm] string? taskName)
        {
            if (string.IsNullOrWhiteSpace(taskName))
            {
                ModelState.AddModelError(nameof(taskName), "Task name is required.");
                return RedirectToAction(nameof(View1));
            }

            _dataAccess.AddTask(taskName);
            return RedirectToAction("View1");
        }

        public IActionResult ToggleComplete(int id)
        {
            _dataAccess.ToggleTaskCompletion(id);
            return RedirectToAction("View1");
        }

        public IActionResult Delete(int id)
        {
            _dataAccess.DeleteTask(id);
            return RedirectToAction("View1");
        }

    }
}
