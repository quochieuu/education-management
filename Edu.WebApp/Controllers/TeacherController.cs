using Edu.Data.EF;
using Edu.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Edu.WebApp.Controllers
{
    [Authorize]
    public class TeacherController : Controller
    {
        private readonly DataDbContext _context;
        public TeacherController(DataDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string search)
        {
            var entity = _context.Teachers.ToList();

            if (!String.IsNullOrEmpty(search))
            {
                entity = _context.Teachers.Where(s => s.Name.Contains(search)).ToList();
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
        public ActionResult Create(Teacher model)
        {
            if (ModelState.IsValid)
            {
                _context.Teachers.Add(model);
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
            Teacher model = _context.Teachers.Find(id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Teacher model)
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
            var student = _context.Teachers.FirstOrDefault(x => x.Id == id);
            _context.Teachers.Remove(student);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
