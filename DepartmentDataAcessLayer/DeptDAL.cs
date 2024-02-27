using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
namespace DepartmentDataAcessLayer
{
    public class DeptDAL
    {
        public string str;
        public DeptDAL()
        {
            str = ConfigurationManager.ConnectionStrings["EmployeeManagementDB"].ConnectionString;
        }
        public bool AddDept(DeptMaster dept)
        {
            bool status = false;
            SqlConnection con = new SqlConnection(str);
            SqlCommand cmd = new SqlCommand("[dbo].sp_InsertDept", con);
            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@p_DeptName", dept.DeptName);
                cmd.Parameters.AddWithValue("@p_location", dept.location);
                cmd.Parameters.AddWithValue("@p_EmpId", dept.EmpId);
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
        public bool EditDept(DeptMaster dept, int deptno)
        {
            bool status = false;
            SqlConnection cn = new SqlConnection(str);
            SqlCommand cmd = new SqlCommand("[dbo].[sp_UpdateDept]", cn);
            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@p_DeptId", deptno);
                cmd.Parameters.AddWithValue("@p_DeptName", dept.DeptName);
                cmd.Parameters.AddWithValue("@p_location", dept.location);
                cmd.Parameters.AddWithValue("@p_EmpId", dept.EmpId);
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
        public bool RemoveDept(int deptno)
        {
            bool status = false;
            SqlConnection cn = new SqlConnection(str);
            SqlCommand cmd = new SqlCommand("delete  from Department where DeptId = " + deptno, cn);
            cn.Open();
            cmd.ExecuteNonQuery();
            status = true;
            cn.Close();
            cn.Dispose();
            return status;
        }

        public List<DeptMaster> GetDeptList()
        {
            List<DeptMaster> deptlist = new List<DeptMaster>();
            string str = ConfigurationManager.ConnectionStrings["EmployeeManagementDB"].ConnectionString;
            SqlConnection con = new SqlConnection(str);
            SqlCommand cmd = new SqlCommand("select * from Department", con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                DeptMaster dm = new DeptMaster();
                dm.DeptId = Convert.ToInt32(dr["DeptId"]);
                dm.DeptName = dr["DeptName"].ToString();
                dm.location = dr["location"].ToString();
                dm.EmpId = Convert.ToInt32(dr["EmpId"]);
                deptlist.Add(dm);
            }
            con.Close();
            con.Dispose();
            return deptlist;

        }
        public bool EmpIdUnique(int empno)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["EmployeeManagementDB"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Department WHERE EmpId = @EmpId", connection))
                {
                    command.Parameters.AddWithValue("@EmpId", empno);
                    connection.Open();
                    int count = (int)command.ExecuteScalar();
                    return count == 0;
                }
            }
        }
        public DeptMaster FindDept(int deptno)
        {
            DeptMaster dept = new DeptMaster();
            SqlConnection cn = new SqlConnection(str);
            SqlCommand cmd = new SqlCommand("select * from Department where DeptId = " + deptno, cn);
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                dept.DeptId = Convert.ToInt32(dr["DeptId"]);
                dept.DeptName = dr["DeptName"].ToString();
                dept.location = dr["location"].ToString();
                dept.EmpId = Convert.ToInt32(dr["EmpId"]);
            }
            cn.Close();
            cn.Dispose();
            return dept;
        }
    }
}
