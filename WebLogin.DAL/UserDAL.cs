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
                connection.Open();
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
            var commandText = @"INSERT INTO Users(UserName, Password) VALUES(@USER_NAME, @PASSWORD)";
            using (SqlConnection connection = new SqlConnection(CONNECTION))
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        SqlCommand command = new SqlCommand(commandText, connection);
                        command.Parameters.AddWithValue("@USER_NAME", userName);
                        command.Parameters.AddWithValue("@PASSWORD", passwordHash);
                        command.ExecuteNonQuery();
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        return false;
                    }
                }

                return true;
            }
        }

        /// <summary>
        /// Change the user password
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passwordHash"></param>
        /// <returns></returns>
        public bool ChangeUserPassword(string userName, string passwordHash)
        {
            var commandText = @"UPDATE Users SET Password=@PASSWORD WHERE UserName=@USER_NAME";
            using (SqlConnection connection = new SqlConnection(CONNECTION))
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        SqlCommand command = new SqlCommand(commandText, connection);
                        command.Parameters.AddWithValue("@PASSWORD", passwordHash);
                        command.Parameters.AddWithValue("@USER_NAME", userName);
                        command.ExecuteNonQuery();
                        transaction.Commit();
                    }
                    catch(Exception)
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Set the roles for a user name
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        public bool SetUserRoles(string userName, List<Role> roles)
        {
            //Delete current roles
            using (SqlConnection connection = new SqlConnection(CONNECTION))
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        try
                        {
                            command.CommandText = @"DELETE FROM UserRoles WHERE UserName = @USER_NAME";
                            command.Transaction = transaction;
                            command.CommandType = CommandType.Text;
                            command.Parameters.AddWithValue("@USER_NAME", userName);
                            command.ExecuteNonQuery();
                            transaction.Commit();
                        }
                        catch (Exception)
                        {
                            transaction.Rollback();
                            return false;
                        }
                    }
                }
            }
            
            //Insert new roles
            using (SqlConnection connection = new SqlConnection(CONNECTION))
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.Transaction = transaction;
                        command.CommandType = CommandType.Text;
                        command.CommandText = "INSERT INTO UserRoles(UserName, Role) VALUES (@USER_NAME, @ROLE);";
                        command.Parameters.Add(new SqlParameter("@USER_NAME", SqlDbType.NVarChar));
                        command.Parameters.Add(new SqlParameter("@ROLE", SqlDbType.NVarChar));
                        try
                        {
                            foreach (var role in roles)
                            {
                                command.Parameters[0].Value = userName;
                                command.Parameters[1].Value = role;
                                if (command.ExecuteNonQuery() != 1)
                                {
                                    throw new InvalidProgramException();
                                }
                            }
                            transaction.Commit();
                        }
                        catch (Exception)
                        {
                            transaction.Rollback();
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Delete a user
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool DeleteUser(string userName)
        {
            using (SqlConnection connection = new SqlConnection(CONNECTION))
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand command = connection.CreateCommand())
                        {
                            //Delete from UserRoles
                            command.CommandText = @"DELETE FROM UserRoles WHERE UserName = @USER_NAME";
                            command.Transaction = transaction;
                            command.CommandType = CommandType.Text;
                            command.Parameters.AddWithValue("@USER_NAME", userName);
                            command.ExecuteNonQuery();
                        }
                        using (SqlCommand command = connection.CreateCommand())
                        {
                            //Delete from Users
                            command.CommandText = @"DELETE FROM Users WHERE UserName = @USER_NAME";
                            command.Transaction = transaction;
                            command.CommandType = CommandType.Text;
                            command.Parameters.AddWithValue("@USER_NAME", userName);
                            if (command.ExecuteNonQuery() != 1)
                            {
                                throw new InvalidProgramException();
                            }
                        }
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
            return true;
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
                command.Parameters.AddWithValue("@USER_NAME", userName);
                command.Parameters.AddWithValue("@PASSWORD_HASH", passwordHash);

                var da = new SqlDataAdapter(command);
                dt = new DataTable();
                da.Fill(dt);
                command.ExecuteReader();
            }

            return dt.Rows.Count > 0;
        }

        /// <summary>
        /// Check if user exist
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool UserExist(string userName)
        {
            if (string.IsNullOrEmpty(userName)) return false;

            DataTable dt;
            var commandText = @"select 1 from Users WHERE UserName = @USER_NAME";

            using (SqlConnection connection = new SqlConnection(CONNECTION))
            {
                connection.Open();
                var command = new SqlCommand(commandText, connection);
                var da = new SqlDataAdapter(command);
                command.Parameters.AddWithValue("@USER_NAME", userName);
                dt = new DataTable();
                da.Fill(dt);
                command.ExecuteReader();
            }

            return dt.Rows.Count > 0;
        }
    }
}
