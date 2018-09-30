using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using WebLogin.IDAL;
using WebLogin.Objects;

namespace WebLogin.DAL
{
    public class UserDAL : IUserDAL
    {
        private const string CONNECTION = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ignas\Documents\Repos\WebLogin\WebLogin.DAL\Database.mdf;Integrated Security=True";

        /// <summary>
        /// Obtain a list of all user names
        /// </summary>
        /// <returns></returns>
        public List<string> GetUserNames()
        {
            DataTable dt;
            var commandText = @"select UserName from Users";

            using (SqlConnection connection = new SqlConnection(CONNECTION))
            {
                var command = new SqlCommand(commandText, connection);
                var da = new SqlDataAdapter(command);
                dt = new DataTable();
                da.Fill(dt);
                command.ExecuteReader();
            }

            var users = new List<string>();

            foreach(DataRow row in dt.Rows)
            {
                users.Add(row["UserName"].ToString());
            }

            return users;
        }

        /// <summary>
        /// Obtain roles for a user name
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public List<Role> GetUserRoles(string userName)
        {
            DataTable dt;
            var commandText = @"select Role from UserRoles where UserName = @USER_NAME";

            using (SqlConnection connection = new SqlConnection(CONNECTION))
            {
                connection.Open();
                var command = new SqlCommand(commandText, connection);
                command.Parameters.Add("@USER_NAME", SqlDbType.NVarChar);
                command.Parameters["@USER_NAME"].Value = userName;

                var da = new SqlDataAdapter(command);
                dt = new DataTable();
                da.Fill(dt);
                command.ExecuteReader();
            }

            var roles = new List<Role>();

            foreach (DataRow row in dt.Rows)
            {
                Role role;
                var dbString = row["Role"].ToString();

                if (Enum.TryParse(dbString, out role))
                    roles.Add(role);
            }

            return roles;
        }

        /// <summary>
        /// Creates a new user
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passwordHash"></param>
        /// <returns></returns>
        public bool CreateUser(string userName, string passwordHash)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Set the roles for a user name
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        public bool SetUserRoles(string userName, List<Role> roles)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Delete a user
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool DeleteUser(string userName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Indicates if user name and password are correct
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passwordHash"</param>
        /// <returns></returns>
        public bool IsValidUser(string userName, string passwordHash)
        {
            DataTable dt;
            var commandText = @"select 1 from Users where UserName = @USER_NAME and Password = @PASSWORD_HASH";

            using (SqlConnection connection = new SqlConnection(CONNECTION))
            {
                connection.Open();
                var command = new SqlCommand(commandText, connection);
                command.Parameters.Add("@USER_NAME", SqlDbType.NVarChar);
                command.Parameters["@USER_NAME"].Value = userName;
                command.Parameters.Add("@PASSWORD_HASH", SqlDbType.NVarChar);
                command.Parameters["@PASSWORD_HASH"].Value = passwordHash;

                var da = new SqlDataAdapter(command);
                dt = new DataTable();
                da.Fill(dt);
                command.ExecuteReader();
            }

            return dt.Rows.Count > 0;
        }
    }
}
