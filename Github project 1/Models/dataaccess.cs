using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;


namespace Github_project_1.Models
{
    public sealed class DataAccess
    {
        private readonly string _connectionString;
        private readonly ILogger<DataAccess> _logger;

        public DataAccess(IConfiguration configuration, ILogger<DataAccess> logger)
        {
            _connectionString = configuration.GetConnectionString("info")
                ?? throw new InvalidOperationException("Connection string 'info' is not configured.");
            _logger = logger;
        }

        public List<Entity> GetAllTasks()
        {
            var tasks = new List<Entity>();
            using var con = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("GetAllTasks", con) { CommandType = CommandType.StoredProcedure };
            try
            {
                con.Open();
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    tasks.Add(new Entity
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        TaskName = reader["TaskName"] == DBNull.Value ? string.Empty : Convert.ToString(reader["TaskName"]) ?? string.Empty,
                        IsCompleted = Convert.ToBoolean(reader["IsCompleted"])
                    });
                }
            }
            catch (SqlException exception)
            {
                _logger.LogError(exception, "Unable to retrieve tasks.");
                throw;
            }
            return tasks;
        }

        public void AddTask(string taskName)
        {
            if (string.IsNullOrWhiteSpace(taskName))
            {
                throw new ArgumentException("Task name is required.", nameof(taskName));
            }

            using var con = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("AddTask", con) { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.Add("@TaskName", SqlDbType.NVarChar, 200).Value = taskName.Trim();
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException exception)
            {
                _logger.LogError(exception, "Unable to add task.");
                throw;
            }
        }

        public void ToggleTaskCompletion(int id)
        {
            ExecuteById("ToggleTaskCompletion", id);
        }

        public void DeleteTask(int id)
        {
            ExecuteById("DeleteTask", id);
        }

        private void ExecuteById(string procedureName, int id)
        {
            using var con = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(procedureName, con) { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException exception)
            {
                _logger.LogError(exception, "Unable to execute stored procedure {ProcedureName} for task {TaskId}.", procedureName, id);
                throw;
            }
        }
    }
}