using AutoMapper;
using Demo.BL.Interface;
using Demo.BL.Models;
using Demo.BL.Repository;
using Demo.DAL.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{

    [Authorize]
    public class DepartmentController : Controller
    {

        // tightly coupled Vs Loosly coupled 
        private readonly IDepartment department;
        private readonly IMapper mapper;

        public DepartmentController(IDepartment department , IMapper mapper)
        {
            this.department = department;
            this.mapper = mapper;
        }
       
        public async Task<IActionResult> Index()
        {
            var data = await department.GetAsync();
            var result = mapper.Map<IEnumerable<DepartmentVM>>(data);
            return View(result);
        }

        public async Task<IActionResult> Details(int id)
        {
            var data = await department.GetByIdAsync(id);
            var result = mapper.Map<DepartmentVM>(data);
            return View(result);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DepartmentVM obj)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var data = mapper.Map<Department>(obj);
                    await department.CreateAsync(data);
                    return RedirectToAction("Index");
                }

                TempData["Msg"] = "Validation Error";
                return View(obj);
            }
            catch (Exception ex)
            {
                TempData["Msg"] = ex.Message;
                return View(obj);
            }
        }

        public async Task<IActionResult> Update(int id)
        {
            var data = await department.GetByIdAsync(id);
            var result = mapper.Map<DepartmentVM>(data);
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Update(DepartmentVM obj)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var data = mapper.Map<Department>(obj);
                    await department.UpdateAsync(data);
                    return RedirectToAction("Index");
                }

                TempData["Msg"] = "Validation Error";
                return View(obj);
            }
            catch (Exception ex)
            {
                TempData["Msg"] = ex.Message;
                return View(obj);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var data = await department.GetByIdAsync(id);
            var result = mapper.Map<DepartmentVM>(data);
            return View(result);
        }

        [HttpPost]
        //[ActionName("Delete")]
        public async Task<IActionResult> Delete(DepartmentVM obj)
        {
            try
            {
                var data = mapper.Map<Department>(obj);
                await department.DeleteAsync(data.Id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Msg"] = ex.Message;
                return View(obj);
            }
        }

    }
}
