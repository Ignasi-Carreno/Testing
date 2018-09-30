using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebLogin.IBLL;

namespace WebLogin.Site.Controllers.API
{
    [Authorize]
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
        public IEnumerable<string> Get()
        {
            return userModel.GetUserNames();
        }

        // GET: api/User/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/User
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/User/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/User/5
        public void Delete(int id)
        {
        }
    }
}
