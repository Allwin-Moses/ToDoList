using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Github_project_1.Models;

namespace Github_project_1.Controllers
{
    public class TodoController : Controller
    {


        dataaccess ado = new dataaccess();

        public ActionResult View1()
        {
            List<Entity> tasks = ado.GetAllTasks();
            return View(tasks);
        }

        [HttpPost]
        public ActionResult Add(string TaskName)
        {
            ado.AddTask(TaskName);
            return RedirectToAction("View1");
        }

        public ActionResult ToggleComplete(int id)
        {
            ado.ToggleComplete(id);
            return RedirectToAction("View1");
        }

        public ActionResult Delete(int id)
        {
            ado.DeleteTask(id);
            return RedirectToAction("View1");
        }

    }
}
