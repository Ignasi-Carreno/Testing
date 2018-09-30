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
        public IHttpActionResult Get()
        {
            var users = userModel.GetUsers().Select(user => AutoMapper.Mapper.Map<User>(user)).ToList();

            return Ok(users);
        }

        // GET: api/User/UserName
        public IHttpActionResult Get(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var user = AutoMapper.Mapper.Map<User>(userModel.GetUser(id));

            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // POST: api/User
        [Authorize(Roles = "ADMIN")]
        public IHttpActionResult Post([FromBody]User user)
        {
            if (user == null)
                return BadRequest("User can't be empty");

            if (userModel.UserExist(user.UserName))
                return BadRequest("The user already exist");

            var objectUser = AutoMapper.Mapper.Map<Objects.User>(user);
            if (userModel.CreateUser(objectUser))
                return CreatedAtRoute("DefaultApi", new { id = user.UserName }, user);
            else
                return InternalServerError();
        }

        // PUT: api/User/UserName
        [Authorize(Roles = "ADMIN")]
        public IHttpActionResult Put(string id, [FromBody]User user)
        {
            if (string.IsNullOrEmpty(id) || !userModel.UserExist(id))
                return NotFound();

            if (userModel.UpdateUser(id, AutoMapper.Mapper.Map<Objects.User>(user)))
                return Content(HttpStatusCode.Accepted, user);
            else
                return InternalServerError();
        }

        // DELETE: api/User/5
        [Authorize(Roles = "ADMIN")]
        public IHttpActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id) || !userModel.UserExist(id))
                return NotFound();

            if (userModel.DeleteUser(id))
                return Ok();
            else
                return InternalServerError();
        }
    }
}
