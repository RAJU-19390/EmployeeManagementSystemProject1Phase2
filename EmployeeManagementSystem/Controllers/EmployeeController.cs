using EmployeeBusinessLayer;
using EmployeeManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;

namespace EmployeeManagementSystem.Controllers
{
    public class EmployeeController : ApiController
    {
        private readonly EmployeeBLL bll = new EmployeeBLL();

        // GET api/Employee
        [HttpGet]
        public IHttpActionResult GetEmployees()
        {
            List<EmployeeModel> dlist = new List<EmployeeModel>();
            List<EmployeeB> blist = bll.GetEmployeeList();

            foreach (EmployeeB emp in blist)
            {
                dlist.Add(new EmployeeModel { EmpId = emp.EmpId, EmpName = emp.EmpName, EmpSalary = emp.EmpSalary, City = emp.City, Age = emp.Age });
            }

            return Ok(dlist);
        }

        // POST api/Employee
        [HttpPost]
        public IHttpActionResult PostEmployee(EmployeeModel model)
        {
            EmployeeBusinessLayer.EmployeeB emp = new EmployeeB
            {
                EmpId = model.EmpId,
                EmpName = model.EmpName,
                EmpSalary = model.EmpSalary,
                City = model.City,
                Age = model.Age
            };

            bll.AddEmployee(emp);

            return Created(Request.RequestUri, "Employee created successfully");
        }

        // GET api/Employee/{empno}
        [HttpGet]
        [Route("api/Employee/{empno}")]
        public IHttpActionResult GetEmployeeById(int empno)
        {
            EmployeeB emp = bll.FindEmployeeById(empno);

            if (emp.EmpId != 0 && emp.EmpName != null)
            {
                EmployeeModel model = new EmployeeModel
                {
                    EmpId = emp.EmpId,
                    EmpName = emp.EmpName,
                    EmpSalary = emp.EmpSalary,
                    City = emp.City,
                    Age = emp.Age
                };

                return Ok(model);
            }
            else
            {
                return NotFound();
            }
        }

        // DELETE api/Employee/{empno}
        [HttpDelete]
        [Route("api/Employee/{empno}")]
        public IHttpActionResult DeleteEmployeeById(int empno)
        {
            try
            {
                EmployeeB emp = bll.FindEmployeeById(empno);

                if (emp.EmpId != 0 && emp.EmpName != null)
                {
                    bool status = bll.RemoveEmployeeData(empno);
                    return Ok(status);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT api/Employee/{empno}
        [HttpPut]
        [Route("api/Employee/{empno}")]
        public IHttpActionResult PutEmployeeById(int empno, EmployeeModel model)
        {
            try
            {
                EmployeeB emp = bll.FindEmployeeById(empno);

                if (emp.EmpId != 0 && emp.EmpName != null)
                {
                    EmployeeBusinessLayer.EmployeeB Data = new EmployeeBusinessLayer.EmployeeB
                    {
                        EmpId = emp.EmpId,
                        EmpName = emp.EmpName,
                        EmpSalary = emp.EmpSalary,
                        City = emp.City,
                        Age = emp.Age
                    };

                    Data.EmpName = model.EmpName;
                    Data.EmpSalary = model.EmpSalary;
                    Data.City = model.City;
                    Data.Age = model.Age;

                    bool status = bll.EditEmployeeData(Data, empno);

                    if (status)
                    {
                        return Ok("Employee updated successfully");
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
