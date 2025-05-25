using Lab2.Data;
using Lab2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab2.Controllers
{
    public class TeacherController : Controller
    {
        private readonly dbFirstLabContext context;

        public TeacherController(dbFirstLabContext context)
        {
            this.context = context;
        }
        // GET: TeacherController
        public ActionResult Index()
        {
            var ts = context.Teachers.ToList(); 
            return View(ts);
        }

        // GET: TeacherController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Teacher obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    context.Teachers.Add(obj);
                    context.SaveChanges();
                    TempData["msg"] = "تمت الاضافة بنجاح";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(obj);
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = "خطأ غير متوقع";
                return View(obj);
            }
        }

        // GET: StudentController/Edit/5
        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id == 0)
            {
                TempData["error"] = "يجب ادخال رقم الاستاذ";
                return RedirectToAction(nameof(Index));
            }
            var item = context.Teachers.Find(id);

            if (item == null)
            {
                TempData["error"] = " يجب ادخال رقم صحيح";
                return RedirectToAction(nameof(Index));
            }
            return View(item);
          
        }

        [HttpPost]
        public IActionResult Edit(Teacher obj)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var res = context.Teachers.Update(obj);
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

        // GET: TeacherController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TeacherController/Delete/5
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
