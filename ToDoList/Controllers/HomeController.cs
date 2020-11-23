using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class HomeController : Controller
    {
        ToDoListContext _db;
        UserManager<IdentityUser> _signInManager;

        public HomeController(ToDoListContext db, UserManager<IdentityUser> signInManager)
        {
            _db = db;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Welcome");
            }
            else
            {
            return View();
            }
        }
        public IActionResult Welcome()
        {
            var userId = _signInManager.GetUserId(User);
            var taskLists = _db.TaskList.ToList().Where(task => task.UserId == userId);
            return View(taskLists.ToList());
        }
        
        public IActionResult CompleteTask(int Id, bool isComplete)
        {
            TaskList t = _db.TaskList.Find(Id);
            t.Complete = isComplete;

            _db.TaskList.Update(t);
            _db.SaveChanges();
            return RedirectToAction("Welcome");
        }

        public IActionResult SaveTask(TaskList newTask)
        {
            _db.TaskList.Add(newTask);
            _db.SaveChanges();
            return View("AddTask");
        }
        public IActionResult AddTask()
        {
            return View();
        }
        public IActionResult Delete(int Id)
        {
            var task = _db.TaskList.Find(Id);
            _db.TaskList.Remove(task);
            _db.SaveChanges();
            return RedirectToAction("Welcome");
        }
        public IActionResult Search(string searchWord)
        {
            var result = _db.TaskList.Where(r => r.ItemDescription.Contains(searchWord));
            return View(result);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
