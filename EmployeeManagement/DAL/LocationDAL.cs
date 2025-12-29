using EmployeeManagement.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace EmployeeManagement.DAL
{
    public class LocationDAL : ILocationDAL
    {
        private readonly string conStr =
        ConfigurationManager.ConnectionStrings["EmployeeDB"].ConnectionString;

        public List<City> GetCitiesByState(int stateId)
        {
            List<City> cities = new List<City>();

            using (SqlConnection con = new SqlConnection(conStr))
            using (SqlCommand cmd = new SqlCommand("sp_GetCitiesByState", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StateId", stateId);

                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        cities.Add(new City
                        {
                            CityId = (int)dr["CityId"],
                            CityName = dr["CityName"].ToString()
                        });
                    }
                }
            }
            return cities;
        }

        public List<State> GetStates()
        {
            List<State> states = new List<State>();

            using (SqlConnection con = new SqlConnection(conStr))
            using (SqlCommand cmd = new SqlCommand("sp_GetStates", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while(dr.Read())
                    {
                        states.Add(new State
                        {
                            StateId = (int)dr["StateId"],
                            StateName = dr["StateName"].ToString()
                        });
                    }
                }
            }

            return states;
        }
    }
}