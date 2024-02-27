using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace EmployeeDataAccessLayer
{
    public class EmployeeDAL
    {
        public string str = ConfigurationManager.ConnectionStrings["EmployeeManagementDB"].ConnectionString;
        public bool AddEmployeeData(EmployeeProp employee)
        {
            bool status = false;
            SqlConnection con = new SqlConnection(str);
            SqlCommand cmd = new SqlCommand("[dbo].sp_InsertEmployeeData", con);
            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@p_EmpName", employee.EmpName);
                cmd.Parameters.AddWithValue("@p_EmpSalary", employee.EmpSalary);
                cmd.Parameters.AddWithValue("@p_City", employee.City);
                cmd.Parameters.AddWithValue("@p_Age", employee.Age);
                con.Open();
                cmd.ExecuteNonQuery();
                status = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                con.Close();
                con.Dispose();
            }
            return status;
        }

        public bool EditEmployee(EmployeeProp employee, int EmpId)
        {
            bool status = false;
            SqlConnection cn = new SqlConnection(str);
            SqlCommand cmd = new SqlCommand("[dbo].[sp_UpdateEmployeeData]", cn);
            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@p_EmpId", EmpId);
                cmd.Parameters.AddWithValue("@p_EmpName", employee.EmpName);
                cmd.Parameters.AddWithValue("@p_EmpSalary", employee.EmpSalary);
                cmd.Parameters.AddWithValue("@p_City", employee.City);
                cmd.Parameters.AddWithValue("@p_Age", employee.Age);
                cn.Open();
                cmd.ExecuteNonQuery();
                status = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            return status;
        }
        public bool RemoveEmployee(int EmpId)
        {
            bool status = false;
            SqlConnection cn = new SqlConnection(str);
            SqlCommand cmd = new SqlCommand("DELETE FROM Employee WHERE EmpId = @EmpId", cn);
            cmd.Parameters.AddWithValue("@EmpId", EmpId);
            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                status = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                cn.Close();
                cn.Dispose();
            }
            return status;
        }


        public List<EmployeeProp> GetEmployeeDataList()
        {
            List<EmployeeProp> emplist = new List<EmployeeProp>();
            SqlConnection con = new SqlConnection(str);
            SqlCommand cmd = new SqlCommand("select * from Employee", con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                EmployeeProp emp = new EmployeeProp();
                emp.EmpId = Convert.ToInt32(dr["EmpId"]);
                emp.EmpName = dr["EmpName"].ToString();
                emp.EmpSalary = Convert.ToSingle(dr["EmpSalary"]);
                emp.City = dr["City"].ToString();
                emp.Age = Convert.ToInt32(dr["Age"]);
                emplist.Add(emp);
            }
            con.Close();
            con.Dispose();
            return emplist;
        }
        public EmployeeProp FindEmployee(int EmpId)
        {
            EmployeeProp emp = new EmployeeProp();
            SqlConnection cn = new SqlConnection(str);
            SqlCommand cmd = new SqlCommand("select * from Employee where EmpId = " + EmpId, cn);
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                emp.EmpId = Convert.ToInt32(dr["EmpId"]);
                emp.EmpName = dr["EmpName"].ToString();
                emp.EmpSalary = Convert.ToSingle(dr["EmpSalary"]);
                emp.City = dr["City"].ToString();
                emp.Age = Convert.ToInt32(dr["Age"]);
            }
            cn.Close();
            cn.Dispose();
            return emp;
        }
    }
}
