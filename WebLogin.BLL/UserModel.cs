using System;
using System.Collections.Generic;
using WebLogin.IBLL;
using WebLogin.IDAL;
using WebLogin.Objects;

namespace WebLogin.BLL
{
    public class UserModel : IUserModel
    {
        private readonly IUserDAL userDal;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userDal"></param>
        public UserModel(IUserDAL userDal)
        {
            this.userDal = userDal;
        }

        /// <summary>
        /// Obtain a list of all user names
        /// </summary>
        /// <returns></returns>
        public List<string> GetUserNames()
        {
            return userDal.GetUserNames();
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
