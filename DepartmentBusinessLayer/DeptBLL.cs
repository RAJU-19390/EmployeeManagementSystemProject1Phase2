using DepartmentDataAcessLayer;
using System.Collections.Generic;

namespace DepartmentBusinessLayer
{
    public class DeptBLL
    {
        DeptDAL dal = new DeptDAL();
        public List<DeptMasterB> GetDepartmentList()
        {
            List<DeptMasterB> deptlist = new List<DeptMasterB>();
            List<DepartmentDataAcessLayer.DeptMaster> ddal = new List<DepartmentDataAcessLayer.DeptMaster>();
            ddal = dal.GetDeptList();
            foreach(var item in ddal)
            {
                DeptMasterB dept = new DeptMasterB();
                dept.DeptId = item.DeptId;
                dept.DeptName = item.DeptName;
                dept.location = item.location;
                dept.EmpId = item.EmpId;
                deptlist.Add(dept);
            }
            return deptlist;
        }
        public bool AddDeptData(DeptMasterB dept)
        {
            DepartmentDataAcessLayer.DeptMaster data = new DeptMaster();
            data.DeptId = dept.DeptId;
            data.DeptName = dept.DeptName;
            data.location = dept.location;
            data.EmpId = dept.EmpId;
            bool status =dal.AddDept(data);
            return status;
        }

        public bool EditDeptData(DeptMasterB dept, int deptno)
        {
            DepartmentDataAcessLayer.DeptMaster data = new DeptMaster();
            data.DeptName = dept.DeptName;
            data.location = dept.location;
            data.EmpId = dept.EmpId;
            bool status = dal.EditDept(data,deptno);
            return status;
        }
        public bool RemoveDeptData(int deptno)
        {
            DeptMaster dept = new DeptMaster();
            dept.DeptId = deptno;
            bool status = dal.RemoveDept(dept.DeptId);
            return status;
        }
        public bool IsEmpIdUnique(int empno)
        {
            DeptMaster dept = new DeptMaster();
            dept.EmpId = empno;
            bool status = dal.EmpIdUnique(dept.EmpId);
            return status;
        }
        public DeptMasterB FindDeptById(int deptno)
        {
            DeptMasterB dept = new DeptMasterB();
            DepartmentDataAcessLayer.DeptMaster data = dal.FindDept(deptno);
            dept.DeptId = data.DeptId;
            dept.DeptName = data.DeptName;
            dept.location = data.location;
            dept.EmpId = data.EmpId;
            return dept;
           
        }
    }
}

