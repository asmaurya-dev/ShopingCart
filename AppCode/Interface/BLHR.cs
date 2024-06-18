using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Drawing;
using ShopingCart.Models;

namespace RP_task.AppCode.BusinessLayer
{
    public class BLHR
    {

        SqlConnection _connection;
        public IConfiguration Configuration { get; }
        public BLHR(IConfiguration configuration)
        {
            Configuration=configuration;
            _connection = new SqlConnection(GetConnectionString());
        }
        private string GetConnectionString()
        {
            return Configuration.GetConnectionString("DefaultConnection");
        }
        public int ExecuteDML(string procname, SqlParameter[] parameters)
        {
            using (SqlCommand command = new SqlCommand(procname,_connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                foreach (SqlParameter param in parameters)
                {
                    if (param.Value != null)
                    {
                        command.Parameters.Add(param);
                    }
                }

                if (_connection.State == ConnectionState.Closed)
                    _connection.Open();

                int result = command.ExecuteNonQuery();
                _connection.Close();
                return result;
            }
        }

        public DataTable ExecuteSelectWithParameters(string procname, SqlParameter[] parameters)
        {
            using (SqlCommand command = new SqlCommand(procname, _connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                foreach (SqlParameter param in parameters)
                {
                    if (param.Value != null)
                    {
                        command.Parameters.Add(param);
                    }
                }

                SqlDataAdapter sda = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                return dt;
            }
        }

        public DataTable ExecuteSelect(string procname)
        {
            using (SqlCommand command = new SqlCommand(procname, _connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter sda = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                return dt;
            }
        }

        public object ExecuteScalar(string procname, SqlParameter[] parameters)
        {
            using (SqlCommand command = new SqlCommand(procname, _connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                foreach (SqlParameter param in parameters)
                {
                    if (param.Value != null)
                    {
                        command.Parameters.Add(param);
                    }
                }

                if (_connection.State == ConnectionState.Closed)
                    _connection.Open();

                object result = command.ExecuteNonQuery();
                _connection.Close();
                return result;
            } 
         }
        public object ExecuteScalarwithparamete(string procname, SqlParameter[] parameters)
        {
            using (SqlCommand command = new SqlCommand(procname, _connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                foreach (SqlParameter param in parameters)
                {
                    if (param.Value != null)
                    {
                        command.Parameters.Add(param);
                    }
                }

                if (_connection.State == ConnectionState.Closed)
                    _connection.Open();

                object result = command.ExecuteScalar();
                _connection.Close();
                return result;
            }
        }

        public object ExecuteScalar(string procname)
        {
            using (SqlCommand command = new SqlCommand(procname, _connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                if (_connection.State == ConnectionState.Closed)
                    _connection.Open();

                object result = command.ExecuteScalar();
                _connection.Close();
                return result;
            }
        }
        public Response Execute(string proc, SqlParameter[] parameters)
        {
            Response response = new Response();

            using (SqlCommand cmd = new SqlCommand(proc, _connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                foreach (SqlParameter param in parameters)
                {
                    if (param.Value != null)
                        cmd.Parameters.Add(param);
                }

                _connection.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        // Read the first row (assuming the stored procedure returns one row)
                        reader.Read();

                        // Retrieve status code and message columns
                        int statusCodeIndex = reader.GetOrdinal("StatusCode");
                        int messageIndex = reader.GetOrdinal("Message");

                        // Retrieve values
                        int statusCode = reader.GetInt32(statusCodeIndex);
                        string message = reader.GetString(messageIndex);

                        // Set the values in the response object
                        response.Status = statusCode;
                        response.Message = message;
                    }
                }

                _connection.Close();
            }

            return response;
        }
        public Response Executee(string proc, SqlParameter[] parameters)
        {
            Response response = new Response();

            using (SqlCommand cmd = new SqlCommand(proc, _connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                foreach (SqlParameter param in parameters)
                {
                    if (param.Value != null)
                        cmd.Parameters.Add(param);
                }

                _connection.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        // Read the first row (assuming the stored procedure returns one row)
                        reader.Read();

                        // Retrieve status code and message columns
                        int statusCodeIndex = reader.GetOrdinal("StatusCode");
                        int messageIndex = reader.GetOrdinal("Message");
                        int NameIndex = reader.GetOrdinal("Name");

                        // Retrieve values
                        int statusCode = reader.GetInt32(statusCodeIndex);
                        string message = reader.GetString(messageIndex);
                        string Name = reader.GetString(NameIndex);

                        // Set the values in the response object
                        response.Status = statusCode;
                        response.Message = message;
                        response.Name = Name;
                    }
                }

                _connection.Close();
            }

            return response;
        }
    }
}
