using System.Collections.Generic;
using WebLogin.Objects;

namespace WebLogin.IDAL
{
    public interface IUserDAL
    {
        /// <summary>
        /// Obtain a list of all user names
        /// </summary>
        /// <returns></returns>
        List<string> GetUserNames();

        /// <summary>
        /// Obtain roles for a user name
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        List<Role> GetUserRoles(string userName);

        /// <summary>
        /// Creates a new user
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passwordHash"></param>
        /// <returns></returns>
        bool CreateUser(string userName, string passwordHash);

        /// <summary>
        /// Set the roles for a user name
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        bool SetUserRoles(string userName, List<Role> roles);

        /// <summary>
        /// Change the user password
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passwordHash"></param>
        /// <returns></returns>
        bool ChangeUserPassword(string userName, string passwordHash);

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
        /// <param name="passwordHash"></param>
        /// <returns></returns>
        bool IsValidUser(string userName, string passwordHash);

        /// <summary>
        /// Check if user exist
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        bool UserExist(string userName);

    }
}
