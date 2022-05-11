using AutoMapper;
using Demo.BL.Helper;
using Demo.BL.Interface;
using Demo.BL.Models;
using Demo.DAL.Entity;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {


        private readonly IEmployeeRep employee;
        private readonly IMapper mapper;

        public EmployeeController(IEmployeeRep employee , IMapper mapper)
        {
            this.employee = employee;
            this.mapper = mapper;
        }



        [HttpGet]
        [Route("~/api/employee/GetEmployee")]
        public async Task<IActionResult> GetEmployee()
        {

            try
            {
                var data = await employee.GetAsync(x => x.IsActive == true && x.IsDeleted == false);
                var result = mapper.Map<IEnumerable<EmployeeVM>>(data);
                return Ok(new ApiResponse<IEnumerable<EmployeeVM>>
                {
                    Code = "200" ,
                    Status = "Ok" ,
                    Message = "Data Found" ,
                    Data = result

                });
            }
            catch (Exception ex)
            {
                return NotFound(new ApiResponse<string>
                {
                    Code = "404",
                    Status = "Not Fount",
                    Message = "No Data Found",
                    Data = ex.Message

                });
            }

        }



        [HttpGet]
        [Route("~/api/employee/GetEmployeeById/{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {

            try
            {
                var data = await employee.GetByIdAsync(x => x.IsActive == true && x.IsDeleted == false && x.Id == id);
                var result = mapper.Map<EmployeeVM>(data);
                return Ok(new ApiResponse<EmployeeVM>
                {
                    Code = "200",
                    Status = "Ok",
                    Message = "Data Found",
                    Data = result

                });
            }
            catch (Exception ex)
            {
                return NotFound(new ApiResponse<string>
                {
                    Code = "404",
                    Status = "Not Fount",
                    Message = "No Data Found",
                    Data = ex.Message

                });
            }

        }


        //[DisableCors]
        //[EnableCors("https://localhost:7106/")]
        [HttpPost]
        [Route("~/api/employee/PostEmployee")]
        public async Task<IActionResult> PostEmployee(EmployeeVM obj)
        {

            try
            {
                var result = mapper.Map<Employee>(obj);
                var data = await employee.CreateAsync(result);

                return Ok(new ApiResponse<Employee>
                {
                    Code = "201",
                    Status = "Created",
                    Message = "Data Saved",
                    Data = result

                });
            }
            catch (Exception ex)
            {
                return NotFound(new ApiResponse<Exception>
                {
                    Code = "400",
                    Status = "Bad Request",
                    Message = "Data Not Created",
                    Data = ex.InnerException

                });
            }

        }



        [HttpPut]
        [Route("~/api/employee/PutEmployee")]
        public async Task<IActionResult> PutEmployee(EmployeeVM obj)
        {

            try
            {
                var result = mapper.Map<Employee>(obj);
                await employee.UpdateAsync(result);

                return Ok(new ApiResponse<string>
                {
                    Code = "202",
                    Status = "Accepted",
                    Message = "Data Updated",
                    Data = "Data Updated"

                });
            }
            catch (Exception ex)
            {
                return NotFound(new ApiResponse<Exception>
                {
                    Code = "400",
                    Status = "Bad Request",
                    Message = "Data Not Created",
                    Data = ex.InnerException

                });
            }

        }



        [HttpDelete]
        [Route("~/api/employee/DeleteEmployee/{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {

            try
            {
                await employee.DeleteAsync(id);

                return Ok(new ApiResponse<string>
                {
                    Code = "202",
                    Status = "Accepted",
                    Message = "Data Deleted",
                    Data = "Data Deleted"

                });
            }
            catch (Exception ex)
            {
                return NotFound(new ApiResponse<Exception>
                {
                    Code = "400",
                    Status = "Bad Request",
                    Message = "Data Not Created",
                    Data = ex.InnerException

                });
            }

        }





    }
}
