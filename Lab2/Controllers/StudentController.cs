using Lab2.Data;
using Lab2.Models;
using Lab2.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab2.Controllers
{

    // [Authorize(Roles = "admin")]
 

    public class StudentController : Controller
    {
        private readonly IRepositoryManager repository;

        public StudentController(IRepositoryManager repository)
        {
            this.repository = repository;
        }

        // GET: StudentController
        [AllowAnonymous] 
        public IActionResult Index()
        {
            //var sList = context.Students;
            //  return NotFound();
            return View(repository.StudentRepository.GetAll());
        }

        public async Task<IActionResult> allStudent()
        {
            return View( repository.StudentRepository.GetAll());
        }

        // GET: Transaction/AddOrEdit(Insert)
        // GET: Transaction/AddOrEdit/5(Update)
        
        public async Task<IActionResult> AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new Student());
            else
            {
                var transactionModel =  repository.StudentRepository.GetById(id);
                if (transactionModel == null)
                {
                    return NotFound();
                }
                return View(transactionModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, [Bind("Id,Name,Age")] Student transactionModel)
        {
            if (ModelState.IsValid)
            {
                //Insert
                if (id == 0)
                {
                    var student = new Student {
                        Age = transactionModel.Age,
                        Id = transactionModel.Id,
                        Name = transactionModel.Name,
                        IsActive= true,
                        
                    //aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
                    
                    };
                    repository.StudentRepository.Add(student);
                    repository.UnitOfWork.SaveChanges();
                    TempData["msg"] = "تمت الاضافة بنجاح";
                }
                //Update
                else
                {
                    try
                    {//asssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssss
                        repository.StudentRepository.Update(transactionModel);
                        repository.UnitOfWork.SaveChanges();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        throw;
                    }
                }
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", repository.StudentRepository.GetAll()) });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", transactionModel) });
        }
      

        // GET: StudentController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: StudentController/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Student obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    repository.StudentRepository.Add(obj);
                    repository.UnitOfWork.SaveChanges();
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
                TempData["error"] = "يجب ادخال رقم الطالب";
                return RedirectToAction(nameof(Index));
            }
            var item = repository.StudentRepository.GetById(id);

            if (item == null)
            {
                TempData["error"] = " يجب ادخال رقم طالب صحيح";
                return RedirectToAction(nameof(Index));
            }
            return View(item);
            //return View(new StudentEditVM { Name = student.Name, Age= student.Age});
        }

        [HttpPost]
        public IActionResult Edit(Student obj)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var res = repository.StudentRepository.Update(obj);
                    if (res != null)
                    {
                        repository.UnitOfWork.SaveChanges();
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

        // GET: StudentController/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == 0)
            {
                TempData["error"] = "يجب ادخال رقم الطالب";
                return RedirectToAction(nameof(Index));
            }
            var item = repository.StudentRepository.GetById(id);

            if (item == null)
            {
                TempData["error"] = " يجب ادخال رقم طالب صحيح";
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        // POST: StudentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Student obj)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var res = repository.StudentRepository.Delete(obj);
                    if (res)
                    {
                        repository.UnitOfWork.SaveChanges();
                        TempData["msg"] = "تم الحذف بنجاح";
                        return RedirectToAction(nameof(Index));
                    }

                    TempData["msg"] = "لم يتم الحذف ";
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

        public IActionResult ChangeState(int id, int state)
        {
            try
            {
                if (id == 0)
                {
                    TempData["error"] = "يجب ادخال رقم الطالب";
                    return RedirectToAction(nameof(Index));
                }
                var item = repository.StudentRepository.GetById(id);

                if (item == null)
                {
                    TempData["error"] = " يجب ادخال رقم طالب صحيح";
                    return RedirectToAction(nameof(Index));
                }
                item.IsActive = state == 1 ? true : false;
                repository.UnitOfWork.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                TempData["error"] = "خطأ غير متوقع";
                return RedirectToAction(nameof(Index)); 
            }
            
        }
    }
}

