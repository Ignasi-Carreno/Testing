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
            var conn = new SqlConnection(CONNECTION);
            conn.Open();
            var cmd = new SqlCommand(@"select UserName from Users", conn);
            var da = new SqlDataAdapter(cmd);
            var dt = new DataTable();
            da.Fill(dt);
            cmd.ExecuteReader();
            conn.Close();

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
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a new user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool CreateUser(User user)
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
        /// <param name="user"></param>
        /// <returns></returns>
        public bool IsValidUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
