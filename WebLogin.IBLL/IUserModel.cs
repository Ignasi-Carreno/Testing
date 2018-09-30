using System.Collections.Generic;
using WebLogin.Objects;

namespace WebLogin.IBLL
{
    public interface IUserModel
    {
        /// <summary>
        /// Obtain a list of all user names
        /// </summary>
        /// <returns></returns>
        List<User> GetUsers();

        /// <summary>
        /// Obtain a user
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        User GetUser(string userName);

        /// <summary>
        /// Check if user exist
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        bool UserExist(string userName);

        /// <summary>
        /// Creates a new user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        bool CreateUser(User user);

        /// <summary>
        /// Update user information
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        bool UpdateUser(string userName, User user);

        /// <summary>
        /// Delete a user
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        bool DeleteUser(string userName);

        /// <summary>
        /// Indicates if user name and password are correct
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        bool IsValidUser(string userName, string password);
    }
}
