using DepartmentBusinessLayer;
using EmployeeManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;

namespace EmployeeManagementSystem.Controllers
{
    public class DepartmentController : ApiController
    {
        private readonly DeptBLL bll = new DeptBLL();

        // GET api/Department
        [HttpGet]
        public IHttpActionResult GetDepartments()
        {
            List<DepartmentModel> dlist = new List<DepartmentModel>();
            List<DeptMasterB> blist = bll.GetDepartmentList();

            foreach (DeptMasterB dept in blist)
            {
                dlist.Add(new DepartmentModel { DeptId = dept.DeptId, DeptName = dept.DeptName, location = dept.location, EmpId = dept.EmpId });
            }

            return Ok(dlist);
        }

        // GET api/Department/{deptno}
        [HttpGet]
        [Route("api/Department/{deptno}")]
        public IHttpActionResult GetDepartmentById(int deptno)
        {
            DeptMasterB dept = bll.FindDeptById(deptno);

            if (dept.DeptId != 0 && dept.DeptName != null)
            {
                DepartmentModel model = new DepartmentModel
                {
                    DeptId = dept.DeptId,
                    DeptName = dept.DeptName,
                    location = dept.location,
                    EmpId = dept.EmpId
                };

                return Ok(model);
            }
            else
            {
                return NotFound();
            }
        }

        // POST api/Department
        [HttpPost]
        public IHttpActionResult PostDepartment(DepartmentModel model)
        {
            DepartmentBusinessLayer.DeptMasterB dept = new DeptMasterB
            {
                DeptName = model.DeptName,
                location = model.location,
                EmpId = model.EmpId
            };

            bll.AddDeptData(dept);

            return Created(Request.RequestUri, "Department created successfully");
        }

        // DELETE api/Department/{deptno}
        [HttpDelete]
        [Route("api/Department/{deptno}")]
        public IHttpActionResult DeleteDepartmentById(int deptno)
        {
            try
            {
                DeptMasterB dept = bll.FindDeptById(deptno);

                if (dept.DeptId != 0 && dept.DeptName != null)
                {
                    bool status = bll.RemoveDeptData(deptno);
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

        // PUT api/Department/{deptno}
        [HttpPut]
        [Route("api/Department/{deptno}")]
        public IHttpActionResult PutDepartmentById(int deptno, DepartmentModel model)
        {
            try
            {
                DeptMasterB Dept = bll.FindDeptById(deptno);

                if (Dept.DeptId != 0 && Dept.DeptName != null)
                {
                    DepartmentBusinessLayer.DeptMasterB Data = new DepartmentBusinessLayer.DeptMasterB
                    {
                        DeptId = Dept.DeptId,
                        DeptName = Dept.DeptName,
                        location = Dept.location,
                        EmpId = Dept.EmpId
                    };

                    Data.DeptName = model.DeptName;
                    Data.location = model.location;
                    Data.EmpId = model.EmpId;

                    bool status = bll.EditDeptData(Data, deptno);

                    if (status)
                    {
                        return Ok("Department updated successfully");
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
