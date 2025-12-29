using EmployeeManagement.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace EmployeeManagement.DAL
{
    public class EmployeeDAL : IEmployeeDAL
    {
        private readonly string conStr = ConfigurationManager.ConnectionStrings["EmployeeDB"].ConnectionString;

        // CREATE – Add employee
        public void AddEmployee(Employee emp)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            using (SqlCommand cmd = new SqlCommand("sp_AddEmployee", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@FullName", emp.FullName);
                cmd.Parameters.AddWithValue("@Email", emp.Email);
                cmd.Parameters.AddWithValue("@Department", emp.Department);
                cmd.Parameters.AddWithValue("@Salary", emp.Salary);
                cmd.Parameters.AddWithValue("@StateId", emp.StateId);
                cmd.Parameters.AddWithValue("@CityId", emp.CityId);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteEmployee(int id)
        {
            using(SqlConnection con = new SqlConnection(conStr))
            using(SqlCommand cmd = new SqlCommand("sp_DeleteEmployee", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeId", id);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // READ – Get all employees
        public List<Employee> GetEmployees()
        {
            List<Employee> employees = new List<Employee>();
            using (SqlConnection con = new SqlConnection(conStr))
            using (SqlCommand cmd = new SqlCommand("sp_GetEmployees", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        employees.Add(new Employee
                        {
                            EmployeeId = (int)dr["EmployeeId"],
                            FullName = dr["FullName"].ToString(),
                            Email = dr["Email"].ToString(),
                            Department = dr["Department"].ToString(),
                            Salary = (decimal)dr["Salary"],
                            StateId = (int)dr["StateId"],
                            CityId = (int)dr["CityId"],
                            StateName = dr["StateName"].ToString(),
                            CityName = dr["CityName"].ToString()
                        });
                    }
                }
            }
            return employees;
        }

        public void UpdateEmployee(Employee emp)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            using (SqlCommand cmd = new SqlCommand("sp_UpdateEmployee", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EmployeeId", emp.EmployeeId);
                cmd.Parameters.AddWithValue("@FullName", emp.FullName);
                cmd.Parameters.AddWithValue("@Email", emp.Email);
                cmd.Parameters.AddWithValue("@Department", emp.Department);
                cmd.Parameters.AddWithValue("@Salary", emp.Salary);
                cmd.Parameters.AddWithValue("@StateId", emp.StateId);
                cmd.Parameters.AddWithValue("@CityId", emp.CityId);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}