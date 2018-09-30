using System;
using System.Collections.Generic;
using System.Linq;
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
        public List<User> GetUsers()
        {
            var users = userDal.GetUserNames().Select(userName => new User()
            {
                UserName = userName,
                Roles = userDal.GetUserRoles(userName)
            });

            return users.ToList();
        }

        /// <summary>
        /// Obtain a user
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public User GetUser(string userName)
        {
            var user = new User()
            {
                UserName = userName,
                Roles = userDal.GetUserRoles(userName)
            };

            return user;
        }

        /// <summary>
        /// Creates a new user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool CreateUser(User user)
        {
            if(userDal.CreateUser(user.UserName, EncodeSHA1(user.Password)))
            {
                return userDal.SetUserRoles(user.UserName, user.Roles);
            }

            return false;
        }

        /// <summary>
        /// Update user information
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool UpdateUser(string userName, User user)
        {
            bool operationFail = false;
            if (!string.IsNullOrEmpty(user.Password))
            {
                operationFail = !userDal.ChangeUserPassword(user.UserName, EncodeSHA1(user.Password));
            }
            
            if(!operationFail && user.Roles != null)
            {
                operationFail = !userDal.SetUserRoles(user.UserName, user.Roles);
            }

            return !operationFail;
        }

        /// <summary>
        /// Delete a user
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool DeleteUser(string userName)
        {
            return userDal.DeleteUser(userName);
        }

        /// <summary>
        /// Indicates if user name and password are correct
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool IsValidUser(string userName, string password)
        {
            return userDal.IsValidUser(userName, EncodeSHA1(password));
        }

        /// <summary>
        /// Encode given string using SHA1 algorithm
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private string EncodeSHA1(string value)
        {
            var hash = System.Security.Cryptography.SHA1.Create();
            var encoder = new System.Text.ASCIIEncoding();
            var combined = encoder.GetBytes(value ?? "");
            return BitConverter.ToString(hash.ComputeHash(combined)).ToLower().Replace("-", "");
        }
    }
}
