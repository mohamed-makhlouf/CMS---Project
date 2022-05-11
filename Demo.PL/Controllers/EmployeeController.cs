using AutoMapper;
using Demo.BL.Interface;
using Demo.BL.Models;
using Demo.DAL.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.IO;
using Demo.BL.Helper;
using Microsoft.AspNetCore.Authorization;

namespace Demo.PL.Controllers
{

    [Authorize]
    public class EmployeeController : Controller
    {


        #region Fields

        private readonly IEmployeeRep employee;
        private readonly IDepartment department;
        private readonly IMapper mapper;
        private readonly ICityRep city;
        private readonly IDistrictRep district;

        #endregion

        #region Ctor

        public EmployeeController(IEmployeeRep employee , IDepartment department , IMapper mapper , ICityRep city , IDistrictRep district)
        {
            this.employee = employee;
            this.department = department;
            this.mapper = mapper;
            this.city = city;
            this.district = district;
        }

        #endregion

        #region Actions

        public async Task<IActionResult> Index(string? SearchValue)
        {
            if(SearchValue == null)
            {
                var data = await employee.GetAsync(x => x.IsActive == true && x.IsDeleted == false);
                var result = mapper.Map<IEnumerable<EmployeeVM>>(data);
                return View(result);
            }
            else
            {
                var data = await employee.GetAsync(x => x.IsActive == true && x.IsDeleted == false && x.Name.Contains(SearchValue) || x.Email.Contains(SearchValue));
                var result = mapper.Map<IEnumerable<EmployeeVM>>(data);
                return View(result);
            }

        }

        public async Task<IActionResult> Details(int id)
        {
            var data = await employee.GetByIdAsync(x => x.IsActive == true && x.IsDeleted == false && x.Id == id);
            var result = mapper.Map<EmployeeVM>(data);
            var DepartmentData = mapper.Map<IEnumerable<DepartmentVM>>(await department.GetAsync());
            ViewBag.DepartmentList = new SelectList(DepartmentData, "Id", "Name", result.DepartmentId);
            return View(result);
        }

        public async Task<IActionResult> Create()
        {
            var data = mapper.Map<IEnumerable<DepartmentVM>>(await department.GetAsync());
            ViewBag.DepartmentList = new SelectList(data, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeVM obj)
        {

            var Dptdata = mapper.Map<IEnumerable<DepartmentVM>>(await department.GetAsync());
            ViewBag.DepartmentList = new SelectList(Dptdata, "Id", "Name");

            try
            {


                if (ModelState.IsValid)
                {

                    obj.ImageName = FileUploader.UploadFile(obj.Image,"Imgs");
                    obj.CvName = FileUploader.UploadFile(obj.Cv, "Docs");

                    var data = mapper.Map<Employee>(obj);
                    await employee.CreateAsync(data);
                    //ModelState.Clear();
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
            var data = await employee.GetByIdAsync(x => x.IsActive == true && x.IsDeleted == false && x.Id == id);
            var result = mapper.Map<EmployeeVM>(data);
            var DepartmentData = mapper.Map<IEnumerable<DepartmentVM>>(await department.GetAsync());
            ViewBag.DepartmentList = new SelectList(DepartmentData, "Id", "Name", result.DepartmentId);
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Update(EmployeeVM obj)
        {
            var Dptdata = mapper.Map<IEnumerable<DepartmentVM>>(await department.GetAsync());
            ViewBag.DepartmentList = new SelectList(Dptdata, "Id", "Name", obj.DepartmentId);

            try
            {
                if (ModelState.IsValid)
                {
                    var data = mapper.Map<Employee>(obj);
                    await employee.UpdateAsync(data);
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
            var data = await employee.GetByIdAsync(x => x.IsActive == true && x.IsDeleted == false && x.Id == id);
            var result = mapper.Map<EmployeeVM>(data);
            var Dptdata = mapper.Map<IEnumerable<DepartmentVM>>(await department.GetAsync());
            ViewBag.DepartmentList = new SelectList(Dptdata, "Id", "Name", result.DepartmentId);
            return View(result);
        }

        [HttpPost]
        //[ActionName("Delete")]
        public async Task<IActionResult> Delete(EmployeeVM obj)
        {


            FileUploader.RemoveFile("Imgs", obj.ImageName);
            FileUploader.RemoveFile("Docs", obj.CvName);

            var Dptdata = mapper.Map<IEnumerable<DepartmentVM>>(await department.GetAsync());
            ViewBag.DepartmentList = new SelectList(Dptdata, "Id", "Name", obj.DepartmentId);

            try
            {
                var data = mapper.Map<Employee>(obj);
                await employee.DeleteAsync(data.Id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Msg"] = ex.Message;
                return View(obj);
            }
        }

        #endregion

        #region Ajax Call


        [HttpPost]
        public async Task<JsonResult> GetCityDataByCountryId(int CntryId)
        {
            var data = await city.GetAsync(a => a.CountryId == CntryId);
            var result = mapper.Map<IEnumerable<CityVM>>(data);
            return Json(result);
        }


        [HttpPost]
        public async Task<JsonResult> GetDistrictDataByCityId(int CtyId)
        {
            var data = await district.GetAsync(a => a.CityId == CtyId);
            var result = mapper.Map<IEnumerable<DistrictVM>>(data);
            return Json(result);
        }

        #endregion

    }
}
