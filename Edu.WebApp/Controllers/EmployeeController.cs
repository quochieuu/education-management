using Edu.Data.EF;
using Edu.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Edu.WebApp.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly DataDbContext _context;
        public EmployeeController(DataDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string search)
        {
            var entity = _context.Employees.ToList();

            if (!String.IsNullOrEmpty(search))
            {
                entity = _context.Employees.Where(s => s.Name.Contains(search)).ToList();
            }

            return View(entity);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee model)
        {
            if (ModelState.IsValid)
            {
                _context.Employees.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Employee model = _context.Employees.Find(id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Employee model)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(model).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            var student = _context.Employees.FirstOrDefault(x => x.Id == id);
            _context.Employees.Remove(student);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
