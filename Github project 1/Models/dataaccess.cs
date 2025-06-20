using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace Github_project_1.Models
{
    public class dataaccess
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["info"].ConnectionString;

        public List<Entity> GetAllTasks()
        {
            List<Entity> tasks = new List<Entity>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetAllTasks", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    tasks.Add(new Entity
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        TaskName = reader["TaskName"].ToString(),
                        IsCompleted = Convert.ToBoolean(reader["IsCompleted"])
                    });
                }
            }
            return tasks;
        }

        public void AddTask(string taskName)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("AddTask", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TaskName", taskName);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void ToggleComplete(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("ToggleTaskCompletion", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteTask(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("DeleteTask", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}