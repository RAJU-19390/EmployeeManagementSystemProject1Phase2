using EmployeeDataAcessLayer;
using System.Collections.Generic;
namespace EmployeeBusinessLayer
{
    public class EmployeeBLL
    {
        EmployeeDAL dal = new EmployeeDAL();
        public List<EmployeeB> GetEmployeeList()
        {
            List<EmployeeB> emplist = new List<EmployeeB>();
            List<EmployeeDataAcessLayer.EmployeeProp> ddal = new List<EmployeeDataAcessLayer.EmployeeProp>();
            ddal = dal.GetEmployeeDataList();
            foreach (var item in ddal)
            {
                EmployeeB emp = new EmployeeB();
                emp.EmpId = item.EmpId;
                emp.EmpName = item.EmpName;
                emp.EmpSalary = item.EmpSalary;
                emp.City = item.City;
                emp.Age = item.Age;
                emplist.Add(emp);
            }
            return emplist;
        }
        public bool AddEmployee(EmployeeB emp)
        {
            EmployeeDataAcessLayer.EmployeeProp data = new EmployeeProp();
            data.EmpId = emp.EmpId;
            data.EmpName = emp.EmpName;
            data.EmpSalary = emp.EmpSalary;
            data.City = emp.City;
            data.Age = emp.Age;
            bool status = dal.AddEmployeeData(data);
            return status;
        }
        public bool EditEmployeeData(EmployeeB emp, int empno)
        {
            EmployeeDataAcessLayer.EmployeeProp data = new EmployeeProp();
            data.EmpName = emp.EmpName;
            data.EmpSalary = emp.EmpSalary;
            data.City = emp.City;
            data.Age = emp.Age;
            bool status = dal.EditEmployee(data, empno);
            return status;
        }
        public bool RemoveEmployeeData(int empno)
        {
            EmployeeB emp = new EmployeeB();
            emp.EmpId = empno;
            bool status = dal.RemoveEmployee(emp.EmpId);
            return status;
        }
        public EmployeeB FindEmployeeById(int empno)
        {
            EmployeeB emp = new EmployeeB();
            emp.EmpId = empno;
            EmployeeDataAcessLayer.EmployeeProp data = dal.FindEmployee(emp.EmpId);
            emp.EmpName = data.EmpName;
            emp.EmpSalary = data.EmpSalary;
            emp.City = data.City;
            emp.Age = data.Age;
            return emp;
        }
    }
}
