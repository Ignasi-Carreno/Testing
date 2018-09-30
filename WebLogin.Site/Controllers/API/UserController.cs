using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebLogin.IBLL;
using WebLogin.Site.Models;

namespace WebLogin.Site.Controllers.API
{
    [Authorize()]
    public class UserController : ApiController
    {
        private readonly IUserModel userModel;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userModel"></param>
        public UserController(IUserModel userModel)
        {
            this.userModel = userModel;
        }

        // GET: api/User
        public IEnumerable<User> Get()
        {
            return userModel.GetUsers().Select(user => AutoMapper.Mapper.Map<User>(user));
        }

        // GET: api/User/UserName
        public User Get(string userName)
        {
            return AutoMapper.Mapper.Map<User>(userModel.GetUser(userName));
        }

        // POST: api/User
        [Authorize(Roles = "ADMIN")]
        public void Post([FromBody]User user)
        {
            userModel.CreateUser(AutoMapper.Mapper.Map<Objects.User>(user));
        }

        // PUT: api/User/5
        [Authorize(Roles = "ADMIN")]
        public void Put(string userName, [FromBody]User user)
        {
            userModel.UpdateUser(userName, AutoMapper.Mapper.Map<Objects.User>(user));
        }

        // DELETE: api/User/5
        [Authorize(Roles = "ADMIN")]
        public void Delete(string userName)
        {
            userModel.DeleteUser(userName);
        }
    }
}
