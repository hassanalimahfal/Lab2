using Lab2.Data;
using Lab2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab2.Controllers
{
    public class SubjectController : Controller
    {
        private readonly dbFirstLabContext context;

        public SubjectController(dbFirstLabContext context)
        {
            this.context = context;
        }
        // GET: SubjectController
        public ActionResult Index()
        {
            var ts = context.Subjects.Include(s=>s.Teacher).ToList();
            return View(ts);
        }

        // GET: SubjectController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            var ts = context.Teachers.ToList();
            ViewData["teachers"] = ts;

            return View();
        }

        [HttpPost]
        public IActionResult Create(Subject obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    context.Subjects.Add(obj);
                    context.SaveChanges();
                    TempData["msg"] = "تمت الاضافة بنجاح";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    var ts = context.Teachers.ToList();
                    ViewData["teachers"] = ts;
                    return View(obj);
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = "خطأ غير متوقع";
                var ts = context.Teachers.ToList();
                ViewData["teachers"] = ts;
                return View(obj);
            }
        }

        // GET: StudentController/Edit/5
        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id == 0)
            {
                TempData["error"] = "يجب ادخال الرقم";
                return RedirectToAction(nameof(Index));
            }
            var item = context.Subjects.Find(id);

            if (item == null)
            {
                TempData["error"] = " يجب ادخال رقم صحيح";
                return RedirectToAction(nameof(Index));
            }
            return View(item);

        }

        [HttpPost]
        public IActionResult Edit(Subject obj)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var res = context.Subjects.Update(obj);
                    if (res.State == EntityState.Modified)
                    {
                        context.SaveChanges();
                        TempData["msg"] = "تم التعديل بنجاح";
                        return RedirectToAction(nameof(Index));
                    }

                    TempData["msg"] = "لم يتم التعديل ";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(obj);
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return View(obj);
            }
        }

        // GET: SubjectController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SubjectController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
